using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class FirebaseManager02 : MonoBehaviour
{
    private FirebaseAuth auth;                              // 로그인, 유저정보 등에 사용
    private FirebaseUser user;                              // 인증이 완료된 유저 정보, 로그인된 유저 정보
    private DatabaseReference DBRefernce;                   // 데이터 베이스에 사용

    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginFied;
    public TMP_Text warninLoginText;
    public TMP_Text confirmLoginText;

    [Header("Register")]
    public TMP_InputField usernamedRegisterFied;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterFied;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warninRegisterText;

    [Header("UserData")]
    public TMP_InputField usernameField;
    public TMP_InputField xpField;
    public TMP_InputField killsField;
    public TMP_InputField deathsField;
    public GameObject scoreElement;
    public Transform scoreboardContent;

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStaus = task.Result;
            if(dependencyStaus == DependencyStatus.Available)
            {
                Init();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies : " + dependencyStaus);
            }
        });
    }

    private void Init()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBRefernce = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void  ClearLoginFields()
    {
        emailLoginField.text = "";
        passwordLoginFied.text = "";
    }

    public void ClearRegisterField()
    {
        usernamedRegisterFied.text = "";
        emailRegisterField.text = "";
        passwordRegisterFied.text = "";
        passwordRegisterVerifyField.text = "";
    }

    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginFied.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterFied.text, usernamedRegisterFied.text));
    }

    public void LogoutButton()
    {
        auth.SignOut();
        UIManager.instance.LoginScreen();
        ClearLoginFields();
        ClearRegisterField();
    }

    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));

        StartCoroutine(UpdateXP(int.Parse(xpField.text)));
        StartCoroutine(UpdateKills(int.Parse(killsField.text)));
        StartCoroutine(UpdateDeaths(int.Parse(deathsField.text)));
    }

    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    private IEnumerator Login(string email, string password)
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if(LoginTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to regidter task with {LoginTask.Exception}");

            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;

            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed";
            switch(errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Acount does not exist";
                    break;
            }
            warninLoginText.text = message;
            confirmLoginText.text = "";
        }
        else
        {
            user = LoginTask.Result;
            Debug.LogFormat("User signed in successfully : {0} ({1})", user.DisplayName, user.Email);
            warninLoginText.text = "";
            confirmLoginText.text = "Logged In";

            // 로그인이 되면 유저데이터를 로드함
            StartCoroutine(LoadUseData());

            // 로그인이 확인 된면 2초후에 Data 화면으로 변경
            yield return new WaitForSeconds(2);

            usernameField.text = user.DisplayName;
            UIManager.instance.UserDateScreen();
            confirmLoginText.text = "";

            ClearLoginFields();
            ClearRegisterField();
        }
    }

    private IEnumerator Register(string email, string password, string username)
    {
        if(username == "")
        {
            warninRegisterText.text = "Missing User Name";
        }
        else if(passwordRegisterFied.text != passwordRegisterVerifyField.text)
        {
            warninRegisterText.text = "Password Doed not Match";
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if(RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with { RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed";

                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warninRegisterText.text = message;
            }
            else
            {
                user = RegisterTask.Result;
                
                if(user != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = username };

                    var ProfileTask = user.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if(ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message:$"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warninRegisterText.text = "Username Set Failed";
                    }
                    else
                    {
                        // return login Scene
                        UIManager.instance.LoginScreen();
                        warninRegisterText.text = "";

                        ClearLoginFields();
                        ClearRegisterField();
                    }
                }
            }
            
        }
    }

    private IEnumerator UpdateUsernameAuth(string username)
    {
        UserProfile profile = new UserProfile { DisplayName = username };

        var ProfileTask = user.UpdateUserProfileAsync(profile);

        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if(ProfileTask.Exception != null)
        {
            Debug.Log(message: $"Faild to register task with {ProfileTask.Exception}");
        }
        else
        {

        }
    }

    private IEnumerator UpdateUsernameDatabase(string username)
    {
        var DBTask = DBRefernce.Child("users").Child(user.UserId).Child("username").SetValueAsync(username);

        yield return new WaitUntil( () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {DBTask.Exception}");
        }
        else
        {

        }
    }

    private IEnumerator UpdateXP(int xp)
    {
        var DBTask = DBRefernce.Child("users").Child(user.UserId).Child("xp").SetValueAsync(xp);

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {DBTask.Exception}");
        }
        else
        {

        }
    }

    private IEnumerator UpdateKills(int kills)
    {
        var DBTask = DBRefernce.Child("users").Child(user.UserId).Child("kills").SetValueAsync(kills);

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {DBTask.Exception}");
        }
        else
        {

        }
    }

    private IEnumerator UpdateDeaths(int deaths)
    {
        var DBTask = DBRefernce.Child("users").Child(user.UserId).Child("deaths").SetValueAsync(deaths);

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {DBTask.Exception}");
        }
        else
        {

        }
    }

    private IEnumerator LoadUseData()
    {
        var DBTask = DBRefernce.Child("users").Child(user.UserId).GetValueAsync();

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {DBTask.Exception}");
        }
        else if( DBTask.Result.Value == null)
        {
            xpField.text = "0";
            killsField.text = "0";
            deathsField.text = "0";
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            xpField.text = snapshot.Child("xp").Value.ToString();
            killsField.text = snapshot.Child("kills").Value.ToString();
            deathsField.text = snapshot.Child("deaths").Value.ToString();
        }
    }

    private IEnumerator LoadScoreboardData()
    {
        var DBTask = DBRefernce.Child("users").OrderByChild("kills").GetValueAsync();

        yield return new WaitUntil(predicate : () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            foreach(Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach(DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int kills = int.Parse( childSnapshot.Child("kills").Value.ToString() );
                int deaths = int.Parse(childSnapshot.Child("deaths").Value.ToString());
                int xp = int.Parse(childSnapshot.Child("xp").Value.ToString());

                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, kills, deaths, xp);
            }

            UIManager.instance.ScoreboardScreen();
        }
    }
}

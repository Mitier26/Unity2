using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;


public class FirebaseManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;               // ���̾� ���̽� ���¸� Ȯ�� �ϱ� ���� ��
    public FirebaseAuth auth;                               // ���̾� ���̽� �α����� ���� ��
    public FirebaseUser User;                               // ���̾� ���̽� ���� ������
    public DatabaseReference DBreference;                   // ���̾� ���̽� �����ͺ��̽�

    [Header("Login")]
    public TMP_InputField emailLoginField;              
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    [Header("UserData")]
    public TMP_InputField usernameField;
    public TMP_InputField xpField;
    public TMP_InputField killsField;
    public TMP_InputField deathField;
    public GameObject scoreElement;
    public Transform scoreboardContent;

    private void Awake()
    {
        // ���̾�̽��� �� �۵��ǰ� �ִ��� Ȯ���� �Ѵ�.
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if(dependencyStatus == DependencyStatus.Available)
            {
                InitialzeFirebase();
            }
            else
            {
                Debug.Log("Could not resolve all firebase dependencies :" + dependencyStatus);
            }
        });
    }

    private void InitialzeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        // ���̾�̽��� �� �۵��Ȱ� �ִٸ� auth�� �⺻������ �Ѵ�.
        auth = FirebaseAuth.DefaultInstance;
        // ���̾�̽� �⺻ ��Ʈ ����
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void ClearLoginField()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }

    public void ClearRegisterField()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
        
    }

    public void SignOutButton()
    {
        // �α׾ƿ� ��ư�� ������ �α��� ȭ���� �����ְ� �ؽ�Ʈ�� �ʱ�ȭ�Ѵ�.
        auth.SignOut();
        UIManager.instance.LoginScreen();
        ClearRegisterField();
        ClearLoginField();
    }

    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));


        StartCoroutine(UpdateXp(int.Parse(xpField.text)));
        StartCoroutine(UpdateKills(int.Parse(killsField.text)));
        StartCoroutine(UpdateDeaths(int.Parse(deathField.text)));
    }

    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreBoardData());
    }

    private IEnumerator Login(string email, string password)
    {
        // ���̾�̽��� �̸��ϰ� �н������ �α����� �������� ���� ����.
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        // �α��� �۵��� �Ϸ�� �� ���� ��ٸ���.
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        // ������ ���� ���
        if(LoginTask.Exception != null)
        {
            Debug.LogWarning(message:$"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed";
            switch (errorCode)
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
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else // ������ ���� ��� ( �α��� ���� )
        {
            // �α����� ������ �����͸� ������ �´�.
            User = LoginTask.Result;
            Debug.LogWarning($"User signed in successfully: {User.DisplayName} ({User.Email})");
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";

            StartCoroutine(LoadUserData());

            yield return new WaitForSeconds(2);

            usernameField.text = User.DisplayName;      // ������ �г��� ǥ��
            UIManager.instance.UserDataScreen();
            confirmLoginText.text = "";

            ClearLoginField();
            ClearRegisterField();
        }
    }

    private IEnumerator Register(string email, string password, string username)
    {
        if(username == "")
        {
            warningRegisterText.text = "Missing UserName";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            warningRegisterText.text = "Password Does not Match";
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if(RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: $"failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEX = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEX.ErrorCode;

                string message = "Register Failed";
                switch(errorCode)
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
                        message = "Email Alredy In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                User = RegisterTask.Result;

                if(User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = username };

                    var ProfileTask = User.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if(ProfileTask.Exception != null)
                    {
                        Debug.LogWarning($"Failed to register task with {ProfileTask.Exception}");
                        warningRegisterText.text = "Username Set Failed";
                    }
                    else
                    {
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";

                        ClearLoginField();
                        ClearRegisterField();
                    }
                }
            }
        }
    }

    private IEnumerator UpdateUsernameAuth(string username)
    {
        UserProfile profile = new UserProfile { DisplayName = username };

        var ProfileTask = User.UpdateUserProfileAsync(profile);

        yield return new WaitUntil(() => ProfileTask.IsCompleted);

        if(ProfileTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            // ������ ������Ʈ
        }
    }

    private IEnumerator UpdateUsernameDatabase(string username)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(username);

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            // ������ ����
        }
    }

    private IEnumerator UpdateXp(int xp)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("xp").SetValueAsync(xp);

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {

        }
    }

    private IEnumerator UpdateKills(int kills)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("kills").SetValueAsync(kills);

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {

        }
    }

    private IEnumerator UpdateDeaths(int deaths)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("deaths").SetValueAsync(deaths);

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {

        }
    }

    private IEnumerator LoadUserData()
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(() => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            xpField.text = "0";
            killsField.text = "0";
            deathField.text = "0";
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            xpField.text = snapshot.Child("xp").Value.ToString();
            killsField.text = snapshot.Child("kills").Value.ToString();
            deathField.text = snapshot.Child("deaths").Value.ToString();
        }
    }

    private IEnumerator LoadScoreBoardData()
    {
        var DBTask = DBreference.Child("users").OrderByChild("kills").GetValueAsync();

        yield return new WaitUntil( () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
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
                int kills = int.Parse(childSnapshot.Child("kills").Value.ToString());
                int deaths = int.Parse(childSnapshot.Child("deaths").Value.ToString());
                int xp = int.Parse(childSnapshot.Child("xp").Value.ToString());

                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                //scoreboardElement.GetComponent<ScoreElement>().newScoreElement(username, kills, deaths, xp);
            }

            //UIManager.instance.ScoreboardScreen();
        }
    }
}
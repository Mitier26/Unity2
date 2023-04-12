using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager2 : MonoBehaviour
{

    public class User
    {
        public int num;
        public string userName;
        public string email;
        public User(int num, string userName, string email)
        {
            this.num = num;
            this.userName = userName;
            this.email = email;
        }
    }

    DatabaseReference reference;
    // 전체 데이터의 갯수를 알아야 할 것 같다.
    // 왜냐면 100 개를 만들었다면 101 번 부터 만들어져야 하기 때문이다.
    int count = 1;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnClickSave()
    {
        // UserID 를 만드는 것을 해야 한다.
        WriteNewUser(count, "UserID", "UserName", "UserEmail");
        count++;
    }

    public void LoadButton()
    {
        readUser("UserID");
    }

    private void WriteNewUser(int count, string userId, string userName, string userEmail)
    {
        User user = new User(count, userName, userEmail);

        string json = JsonUtility.ToJson(user);

        reference.Child(userId).Child("num" + count.ToString()).SetRawJsonValueAsync(json);
    }

    private void readUser(string userId)
    {
        reference.Child(userId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {

            }
            else if (task.IsCompleted)
            {
                DataSnapshot smapshot = task.Result;

                foreach (DataSnapshot data in smapshot.Children)
                {
                    IDictionary personInfo = (IDictionary)data.Value;
                    Debug.Log("email: " + personInfo["email"] + ", UserName : " + personInfo["userName"] + ", num : " + personInfo["num"]);
                }
            }
        });
    }
}

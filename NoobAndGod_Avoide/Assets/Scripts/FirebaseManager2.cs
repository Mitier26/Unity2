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
    // ��ü �������� ������ �˾ƾ� �� �� ����.
    // �ֳĸ� 100 ���� ������ٸ� 101 �� ���� ��������� �ϱ� �����̴�.
    int count = 1;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnClickSave()
    {
        // UserID �� ����� ���� �ؾ� �Ѵ�.
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using File = UnityEngine.Windows.File;

public class SaveSystem : MonoBehaviour
{
    public TMP_InputField ImportField;
    public TMP_InputField ExportField;

    public Image CopyButton;
    public Image PasteButton;

    public TMP_Text CopyButtonText;
    public TMP_Text PasteButtonText;

    private const string FileType = ".txt";
    private const string FilePath = "PlayerData";
    private static string SavePath => Application.persistentDataPath + "/Saves/";
    private static string BackUpSavePath => Application.persistentDataPath + "/BackUps/";

    private static int SaveCount;

    public static void SaveData<T> (T data, string fileName)
    {
        Directory.CreateDirectory(SavePath);
        Directory.CreateDirectory(BackUpSavePath);

        if (SaveCount % 5 == 0) Save(BackUpSavePath);
        Save(SavePath);

        SaveCount++;

        void Save(string path)
        {
            using(StreamWriter writer = new StreamWriter(path + fileName + FileType)) 
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream();

                formatter.Serialize(memoryStream, data);
                string dataToSave = Convert.ToBase64String(memoryStream.ToArray());
                writer.Write(dataToSave);
                writer.Close();
            }
        }
    }

    public static T LoadData<T>(string fileName)
    {
        Directory.CreateDirectory(SavePath);
        Directory.CreateDirectory(BackUpSavePath);

        bool backUpNeeded = false;
        T dataToReturn;

        Load(SavePath);
        if (backUpNeeded) Load(BackUpSavePath);

        return dataToReturn;

        void Load(string path)
        {
            using(StreamReader reader = new StreamReader(path + fileName + FileType))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                string dataToLoad = reader.ReadToEnd();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(dataToLoad));
                
                try
                {
                    dataToReturn = (T)formatter.Deserialize(memoryStream);
                }
                catch
                {
                    backUpNeeded = true;
                    dataToReturn = default;
                }
            }
        }
    }

    public static bool SaveExists(string fileName)
    {
        return File.Exists(SavePath + fileName + FileType) || File.Exists(BackUpSavePath + fileName + FileType);
    }

    public void Import()
    {
        Directory.CreateDirectory(SavePath);

        using (StreamWriter writer = new StreamWriter( $"{SavePath}{FilePath}{FileType}"))
        {
            writer.WriteLine(ImportField.text);
            writer.Close();
        }

        Controller.Instance.data = SaveExists(FilePath) ? LoadData<Data>(FilePath) : new Data(); 
    }

    public void Export()
    {
        Controller.Instance.Save();
        Directory.CreateDirectory(SavePath);

        using (StreamReader reader = new StreamReader($"{SavePath}{FilePath}{FileType}"))
        {
            ExportField.text = reader.ReadToEnd();
            reader.Close();
        }
    }    

    public void Copy()
    {
        if (ExportField.text == "") return;

        GUIUtility.systemCopyBuffer = ExportField.text;
        CopyButton.color = Color.green;
        CopyButtonText.text = "Cpied!";
        StartCoroutine(CopyPasteButtonsNormal());
    }

    public void Paste()
    {
        if (ExportField.text == "") return;

        ImportField.text = GUIUtility.systemCopyBuffer;
        PasteButton.color = Color.green;
        PasteButtonText.text = "Pasted!";
        StartCoroutine(CopyPasteButtonsNormal());
    }

    public void Clear(string type)
    {
        if(type == "Export")
        {
            ExportField.text = "";
            return;
        }
        ImportField.text = "";
    }

    public IEnumerator CopyPasteButtonsNormal()
    {
        yield return new WaitForSeconds(2f);
        CopyButton.color = new Color(0.215f, 0.215f, 0.215f);
        CopyButtonText.text = "Copy to Clipboard";
        PasteButton.color = new Color(0.215f, 0.215f, 0.215f);
        PasteButtonText.text = "Paste to Clipboard";

    }
}


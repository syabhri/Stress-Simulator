using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public const string saveFolder = "/saves/";
    public const string fileExtention = ".sav";

    public static void Save<T>(T objectToSave, string key)
    {
        string path = Application.persistentDataPath + saveFolder;
        Directory.CreateDirectory(path);
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(path + key + fileExtention, FileMode.Create))
        {
            formatter.Serialize(fileStream, objectToSave);
        }
        Debug.Log("Saved");
    }

    public static T Load<T>(string key)
    {
        string path = Application.persistentDataPath + saveFolder;
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        using (FileStream fileStream = new FileStream(path + key + fileExtention, FileMode.Open))
        {
            returnValue = (T)formatter.Deserialize(fileStream);
        }
        Debug.Log("Loaded");
        return returnValue;
    }

    public static bool SaveExists(string key)
    {
        string path = Application.persistentDataPath + saveFolder + key + fileExtention;
        return File.Exists(path);
    }

    public static void DeleteSave(string key)
    {
        string path = Application.persistentDataPath + saveFolder + key + fileExtention;
        File.Delete(path);
        Debug.Log("Save Deleted");
    }

    public static void DeleteAllSave()
    {
        string path = Application.persistentDataPath + saveFolder;
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete();
        Directory.CreateDirectory(path);
        Debug.Log("Save Deleted");
    }
}

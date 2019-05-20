using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{ 
    public static void SaveGame (Stats playerStat)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(playerStat);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData loadData()
    {
        string path = Application.persistentDataPath + "/game.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}

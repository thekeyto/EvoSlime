using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class GameSaveManager : MonoBehaviour
{
    public Inventory myInventory;

    public void Save()
    {
        Debug.Log(Application.persistentDataPath);
        if(!Directory.Exists(Application.persistentDataPath+"/game_SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");

        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/inventory.txt");
        
        var json = JsonUtility.ToJson(myInventory);

        formatter.Serialize(file, json);

        file.Close();

    }

    public void Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if(File.Exists(Application.persistentDataPath + "/game_SaveData/inventory.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_SaveData/inventory.txt",FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file),myInventory);
        }
    }
}

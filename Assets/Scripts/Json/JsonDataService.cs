using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonDataService : IDataService
{
    //Save data to JSON
    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        try
        {
            //Check if file exists, overwrite if yes
            if (File.Exists(path)) 
            {
                Debug.Log("Data exists. Deleting old file and writing a new one!");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Writing file for the first time!");
            }

            //Get path and write JSON object into a file
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
        }
        catch (Exception ex) 
        {
            //What the fuck
            Debug.LogError($"Unable to save data due to: {ex.Message} {ex.StackTrace}");
            return false;
        }
    }

    //Load data from JSON
    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        //Check if file doesn't exist
        if (!File.Exists(path))
        {
            Debug.LogError($"Cannot load file at {path}. FIle does not exist!");
            throw new FileNotFoundException($"{path} does not exist!");
        }

        //Attempts to read the data from path
        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load data due to: {ex.Message} {ex.StackTrace}");
            throw ex;
        }
    }

    //Maan fuck that Json data amiright
    public bool DeleteData<T>(string RelativePath)
    {
        string path = Application.persistentDataPath + RelativePath;

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to delete data due to: {ex.Message} {ex.StackTrace}");
            return false;
        }
    }
}

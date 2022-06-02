using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Serializer
{
    private static readonly string _folderPath = Application.persistentDataPath + "/";
    private static readonly string _settingsFileName = "settings.config";
    private static readonly string _saveFileName = "save.copernicus";

    public static void Serialize(SettingsConfigInfo configInfo)
    {
        using (FileStream fileStream = new FileStream(_folderPath + _settingsFileName, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, configInfo);
        }
    }

    public static void Serialize(PlayerConfigInfo configInfo)
    {
        using (FileStream fileStream = new FileStream(_folderPath + _saveFileName, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, configInfo);
        }
    }

    public static SettingsConfigInfo DeserializeSettings()
    {
        if (File.Exists(_folderPath + _settingsFileName))
        {
            using (FileStream fileStream = new FileStream(_folderPath + _settingsFileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(fileStream) as SettingsConfigInfo;
            }
        }
        else return null;
    }
    public static PlayerConfigInfo DeserializeSave()
    {
        if (File.Exists(_folderPath + _saveFileName))
        {
            using (FileStream fileStream = new FileStream(_folderPath + _saveFileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(fileStream) as PlayerConfigInfo;
            }
        }
        else return null;
    }

    public static void ResetGameSave()
    {
        if (File.Exists(_folderPath + _saveFileName)) File.Delete(_folderPath + _saveFileName);
    }
}

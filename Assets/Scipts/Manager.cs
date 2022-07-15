using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public static string currentName;
    public static SaveData data;
    public static List<profileIcon> profileIcons = new List<profileIcon>();
    private void Awake()
    {
        if (data == null)
        {
            data = new SaveData();
        }
        if (instance != null)
        {
            Destroy(gameObject);
        }
        if (currentName == null)
        {
            currentName = "player1";
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        loadGame();
    }
    public static void saveGame()
    {
        string filename = Application.persistentDataPath + "/" + currentName + ".json";
        string jsonData = JsonUtility.ToJson(data);
  
        File.WriteAllText(filename, jsonData );

    }
    public static void loadGame()
    {
        string filename = Application.persistentDataPath + "/" + currentName + ".json";
        if (File.Exists(filename))
        {
            string fileData = File.ReadAllText(filename);
            data = JsonUtility.FromJson<SaveData>(fileData);
        }
        else
        {
            data = new SaveData();
        }
        
    }
    public static void addProfileIcon(profileIcon icon)
    {
        if (!profileIcons.Contains(icon))
        {
            profileIcons.Add(icon);
        }
    }
    public static void swapProfile(profileIcon icon)
    {
        foreach(profileIcon icons in profileIcons)
        {
            if (icons != icon)
            {
                icons.setNotCurrent();
            }
            else
            {
                string newName = icons.getProfileName();
                icons.setCurrent();
                currentName = newName;
                loadGame();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataSaver : MonoBehaviour
{
    public int Level;
    public string PlayerName;
    public float dollars;

    [ContextMenu("Save Data")]

    void SaveData()
    {
        PlayerPrefs.SetInt("Levels Complete",2);
        PlayerPrefs.SetString("Name", PlayerName);
        PlayerPrefs.SetFloat("Name", dollars);
        PlayerPrefs.Save();
    }

    [ContextMenu("Load Data")]

    void LoadData()
    {
        Level = PlayerPrefs.GetInt("Levels Complete", 1);
        PlayerName = PlayerPrefs.GetString ("Name", "You have no name");
        dollars  = PlayerPrefs.GetFloat("Money", 0);
    }
}

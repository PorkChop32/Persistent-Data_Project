using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataPersistent : MonoBehaviour
{
    public static DataPersistent instance;

    public string userNameData;

    public string bestUserNameData;
    
    public int pointsData;

    public int bestPointsData;

    void Awake()
    {
        if (instance != null )
        {
            Destroy(gameObject);
        }
        else if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (bestUserNameData == null)
        {
            bestUserNameData = "Name";
        }      
    }    
}

[System.Serializable]
public class SaveData
{
    public string bestUserNameDataSave;
    public int bestPointsDataSave;
}

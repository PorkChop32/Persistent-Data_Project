using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MenuUiHandler : MonoBehaviour
{   
    public TMP_InputField inputName;
    public string inputNameString;
    public TextMeshProUGUI nameRecordText;
    public static MenuUiHandler instance;


    void Awake()
    {                   
        if (instance == null)
        {
            instance = this;
        }else 
        {
            Destroy(gameObject);
        }
        
    }

    void Update()
    {
        bestRecordUpdate();
    }


    public void bestRecordUpdate()
    {
        nameRecordText.text = $"{DataPersistent.instance.bestUserNameData}: {DataPersistent.instance.bestPointsData}";
    }
    public void StartClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitClicked()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #elif UNITY_WEBGL
        Application.ExternalEval("window.location.href='https://play.unity.com';");
    #else
        Application.Quit();
    #endif
    }

    public void OnNameTyped()
    {
        DataPersistent.instance.userNameData = inputName.text;

    }

    public void toSaveData()
    {
        SaveData data = new SaveData();
        data.bestUserNameDataSave = DataPersistent.instance.bestUserNameData;
        data.bestPointsDataSave = DataPersistent.instance.bestPointsData;
        
        string json = JsonUtility.ToJson(data);
    #if UNITY_WEBGL
        PlayerPrefs.SetString("SaveFile", json);
        PlayerPrefs.Save();
    #elif UNITY_EDITOR
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    #endif
        
        Debug.Log("Data was saved.");
    }

    public void toLoadData()
    {
    #if UNITY_WEBGL    
        SaveData data = LoadData<SaveData>();
        DataPersistent.instance.bestUserNameData = data.bestUserNameDataSave; 
        DataPersistent.instance.bestPointsData = data.bestPointsDataSave;
    #elif UNITY_EDITOR
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            DataPersistent.instance.bestUserNameData = data.bestUserNameDataSave; 
            DataPersistent.instance.bestPointsData = data.bestPointsDataSave;
        }    
    #endif
        
        Debug.Log("Data was loaded.");

    }

    public T LoadData<T>() where T : new()
    {
        if (PlayerPrefs.HasKey("SaveFile"))
        {
            string json = PlayerPrefs.GetString("SaveFile");
            return JsonUtility.FromJson<T>(json);
        }
        
        return new T();
    }
}

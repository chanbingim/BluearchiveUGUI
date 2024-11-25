using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;
    [SerializeField] private CustomDictionary<ScriptableObject> CharacterDataList;
    static GameLogicmanager GameInstance;

    private void Awake()
    {
       if (null == instance)
       {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
       }
       else
       {
            Destroy(this.gameObject);
       }
    }
    public static DataManager GetInstance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    [ContextMenu("To Json Data")] // ������Ʈ �޴��� �Ʒ� �Լ��� ȣ���ϴ� To Json Data ��� ��ɾ ������
    public void SavePlayerDataToJson<T>(string FileName, T SaveData)
    {
        // ToJson�� ����ϸ� JSON���·� �����õ� ���ڿ��� �����ȴ�  
        string jsonData = JsonUtility.ToJson(SaveData, true);
        // �����͸� ������ ��� ����
        string path = Path.Combine(Application.dataPath, FileName);
        // ���� ���� �� ����
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")] // ������Ʈ �޴��� �Ʒ� �Լ��� ȣ���ϴ� To Json Data ��� ��ɾ ������
    public bool LoadPlayerDataToJson<T>(string FileName, System.Action<T> onLoadCallback)
    {
        // �����͸� �ҷ��� ��� ����
        string path = Path.Combine(Application.dataPath, FileName);

        if(File.Exists(path))
        {
            // ������ �ؽ�Ʈ�� string���� ����
            string jsonData = File.ReadAllText(path);
            // ���� ���� �� ����
            T LoadPlayerData = JsonUtility.FromJson<T>(jsonData);
            onLoadCallback.Invoke(LoadPlayerData);
            return true;
        }

        return false;
    }

    public ScriptableObject GetCharacterData(string Name)
    {
        return CharacterDataList.Find(Name);
    }

}

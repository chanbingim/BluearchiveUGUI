using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum evnetType
{
    clicked,
    Login,
}

public class CharacterDialogueTrigger : MonoBehaviour
{
    [SerializeField] private string CharacterName;
    [SerializeField] private GameObject ApplyTextView;
    [SerializeField] private float EndTime;
    [SerializeField] private CharacterScriptObj dataObj;
    private void Start()
    {
        dataObj = (CharacterScriptObj)DataManager.GetInstance.GetCharacterData(CharacterName);
        SetTextData(evnetType.Login);
        if (!ApplyTextView.activeSelf)
        {
            ApplyTextView.SetActive(true);
        }
    }

    public void OnclickedDialogueEvent()
    {
        if(ApplyTextView)
        {
            EndTime = 0;
            SetTextData(evnetType.clicked);
            if (!ApplyTextView.activeSelf)
            {
                ApplyTextView.SetActive(true);
            }
        }
    }

    public void SetTextData(evnetType type)
    {
        var text = ApplyTextView.GetComponentInChildren<Text>();

        int maxindex = GetDataLength(type);
        if(maxindex > 0)
        {
            int index = Random.RandomRange(0, maxindex);
            if (text)
                text.text = GetDatatext(type, index);

            var path = GetDataPath(type,index);
            GameAudioManager.GetInstance.CharacterVoiceEndDele += () => { InvisibleText(); };
            EndTime += GameAudioManager.GetInstance.playCharacterVoice(path);
        }
    }

    public int GetDataLength(evnetType type)
    {
        switch(type)
        {
            case evnetType.clicked:
                return dataObj.data.characterData.clickMessageString.Length;
            case evnetType.Login:
               
                return dataObj.data.characterData.LoginVoicepath.Length;
        }

        return -1;
    }
    public string GetDatatext(evnetType type, int index)
    {
        switch (type)
        {
            case evnetType.clicked:
                return dataObj.data.characterData.clickMessageString[index];
            case evnetType.Login:
                Debug.Log(index);
                return dataObj.data.characterData.LoginVoiceString[index];
        }

        return null;
    }
    public string GetDataPath(evnetType type, int index)
    {
        switch (type)
        {
            case evnetType.clicked:
                return dataObj.data.characterData.clickMessagespath[index];
            case evnetType.Login:
                return dataObj.data.characterData.LoginVoicepath[index];
        }

        return null;
    }
    public void InvisibleText()
    {
        if(ApplyTextView)
            ApplyTextView.SetActive(false);
    }
}

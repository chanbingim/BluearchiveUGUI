using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FCharacterInfoData
{
    //Login Voice
    public string[] LoginVoicepath;
    public string[] LoginVoiceString;

    //Lobby Voice
    public string[] clickMessagespath;
    public string[] clickMessageString;

    //Battle Voice
    public string[] battleVoicespath;

    //Special Voice
    public string[] specialMessagespath;
}

[System.Serializable]
public struct FCharacterData
{
    public string CharacterName;
    public FCharacterInfoData characterData;
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterScriptObj : ScriptableObject
{
    public FCharacterData data;
}
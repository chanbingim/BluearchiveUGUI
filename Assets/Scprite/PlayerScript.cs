using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public enum EPlayerState
{
    none,
    Idle,
    PhotoViewing,
    Battle,
    showEvent,
}

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private EPlayerState playerState;

    private void Start()
    {
        InitPlyerState();
    }

    public void ClickedShowCharacter()
    {
        playerState = EPlayerState.PhotoViewing;
    }

    public void SetPlayerState(EPlayerState newState)
    {
        playerState = newState;
    }

    public EPlayerState GetPlayerState()
    {
        return playerState;
    }

    public void InitPlyerState()
    {
        playerState = EPlayerState.Idle;
    }
    
}

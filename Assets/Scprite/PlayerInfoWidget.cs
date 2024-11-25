using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameLogicmanager;

public class PlayerInfoWidget : MonoBehaviour
{
    delegate void UpdateWidget();

    public GameObject Nickname;
    public GameObject experienceBar;
    public GameObject PlayerLevel;

    UpdateWidget UpdateDelegateFunc;

    private void Awake()
    {
        UpdateDelegateFunc = new UpdateWidget(UpdatePlayerInfoWidget);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateDelegateFunc.Invoke();
    }

    public void UpdatePlayerInfoWidget()
    {
        GameLogicmanager GameInstance = GameLogicmanager.GetInstance;
        FPlayerInfomation _PlayerData = GameInstance.GetPlayerData();
        Nickname.GetComponent<Text>().text = _PlayerData.PlayerName;
        experienceBar.GetComponentInChildren<Text>().text = _PlayerData.PlayerCurrentExperience + "/" + _PlayerData.MaxExperience;
        experienceBar.GetComponent<Slider>().value = (float)(_PlayerData.PlayerCurrentExperience * (_PlayerData.MaxExperience * 0.01));
        PlayerLevel.GetComponentInChildren<Text>().text = "LV.\n" + _PlayerData.PlayerLevel.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputManager : MonoBehaviour
{
    private static InputManager instance = null;
    #region //Player Touch Location Variable & Touch Variable
    private Vector2 currentLoc;
    private Vector2 moveParticleLoc;
    private bool bIsTouch = false;
    #endregion
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
    public static InputManager GetInstance
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

    private ParticleCanvasManager particleManager; 
    private void Start()
    {
        particleManager = ParticleCanvasManager.GetInstance;
    }
    private void Update()
    {
        PlayerKetInput();
    }

    void PlayerKetInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (bIsTouch)
                HandleTouch(TouchPhase.Moved, Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch(TouchPhase.Began, Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            TouchEndEvent();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouch(touch.phase, touch.position);
        }
    }

    private void HandleTouch(TouchPhase phase, Vector3 position)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                bIsTouch = true;
                TouchEventFromPlayerState();
                particleManager.TouchStartEvent(position);
                break;

            case TouchPhase.Moved:
                if(bIsTouch)
                    particleManager.InputMoveEvent(position);
                break;

            case TouchPhase.Ended:
                TouchEndEvent();
                break;
        }
    }

    private void TouchEndEvent()
    {
        bIsTouch = false;
        particleManager.TouchMoveParticleObj.SetActive(false);
    }

    private void TouchEventFromPlayerState()
    {
        var player = gameObject.GetComponent<PlayerScript>();

        switch (player.GetPlayerState())
        {
            case EPlayerState.PhotoViewing:
                UIManager.GetInstance.showDynamicBut();
                break;
        }
    }
}

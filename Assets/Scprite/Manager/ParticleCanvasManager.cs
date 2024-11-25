using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticleCanvasManager : MonoBehaviour
{
    static ParticleCanvasManager instance;
    [SerializeField] private GameObject ParticleObjclass;
    [SerializeField] private GameObject TouchMoveObjclass;
    [SerializeField] private float ParticleObjMoveSpeed;

    //public variable
    [HideInInspector] public  GameObject TouchParticleObj;
    [HideInInspector] public  GameObject TouchMoveParticleObj;

    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public static ParticleCanvasManager GetInstance
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
    private void Start()
    {
        if(ParticleObjclass && !TouchParticleObj)
        {
            var ParticleObj = Instantiate(ParticleObjclass);
            ParticleObj.transform.parent = gameObject.transform;
            ParticleObj.transform.localPosition = Vector3.zero;
            ParticleObj.transform.localScale = ParticleObjclass.transform.localScale;
            ParticleObj.SetActive(false);
            
            TouchParticleObj = ParticleObj;
        }

        if(TouchMoveObjclass && !TouchMoveParticleObj)
        {
            var ParticleMoveObj = Instantiate(TouchMoveObjclass);
            ParticleMoveObj.transform.parent = gameObject.transform;
            ParticleMoveObj.transform.localPosition = Vector3.zero;
            ParticleMoveObj.transform.localScale = TouchMoveObjclass.transform.localScale;
            ParticleMoveObj.SetActive(false);
            

            TouchMoveParticleObj = ParticleMoveObj;
        }
    }
    
    public float GetParticleSpeed() { return ParticleObjMoveSpeed; }
    public void TouchStartEvent(Vector3 position)
    {
        if (TouchParticleObj)
        {
            var canvas = TouchParticleObj.GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.GetComponent<RectTransform>(), // 캔버스의 RectTransform
                    position,                  // 마우스 스크린 좌표
                    canvas.worldCamera,                   // 캔버스의 카메라
                    out Vector2 currentLoc                // 로컬 좌표 결과
                );

                var particleObj = TouchParticleObj.GetComponent<RectTransform>();
                particleObj.localPosition = currentLoc; // 로컬 좌표 설정
                TouchParticleObj.SetActive(true);
                TouchMoveParticleObj.transform.localPosition = currentLoc;
                var animation = TouchParticleObj.GetComponent<ChangeScaleAnimation>();
                if (animation)
                {
                    animation.Play();
                }
            }
        }
    }
    public void InputMoveEvent(Vector3 position)
    {
        if (TouchMoveParticleObj)
        {
            var canvas = TouchMoveParticleObj.GetComponentInParent<Canvas>();
            Vector2 moveParticleLoc = TouchMoveParticleObj.transform.localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                     canvas.GetComponent<RectTransform>(), // 캔버스의 RectTransform
                     position,                  // 마우스 스크린 좌표
                     canvas.worldCamera,                   // 캔버스의 카메라
                     out Vector2 currentLoc                // 로컬 좌표 결과
            );

            if (Vector2.Distance(currentLoc, moveParticleLoc) < 0.1f)
            {
                if(TouchMoveParticleObj.activeSelf)
                    TouchMoveParticleObj.SetActive(false); // 사라짐
                return; // 이벤트 종료
            }
            if (currentLoc != moveParticleLoc)
            {
                if (!TouchMoveParticleObj.activeSelf)
                    TouchMoveParticleObj.SetActive(true);
                TouchMoveParticleObj.GetComponent<RectTransform>().localPosition = Vector2.Lerp(moveParticleLoc, currentLoc, Time.deltaTime * ParticleObjMoveSpeed);
            }
        }
    }
}

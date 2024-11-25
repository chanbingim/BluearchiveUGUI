using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance = null;
    [SerializeField] private GameObject DataContext;
    [SerializeField] private List<GameObject> showPhoto_DynamicButList;
    [SerializeField] private float ShowTimeRate;

    private List<GameObject> DynamicButList;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public static UIManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            else
                return instance;
        }
    }

    private void Start()
    {
        showPhoto_DynamicButList = DataContext.GetComponent<SceneContext>().showPhto_DyamicUIList;
    }

    public void showDynamicBut()
    {
        foreach (GameObject obj in showPhoto_DynamicButList)
        {
            var but = obj.GetComponent<CustomButtonScrite>();
            if (!but.gameObject.activeSelf)
            {
                but.gameObject.SetActive(true);
            }
            else
            {
                but.AddShowEndTime();
            }

        }
    }
}

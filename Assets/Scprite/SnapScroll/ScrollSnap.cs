using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ScrollSnap : MonoBehaviour
{
    public ScrollRect scrollRect;          // ScrollRect 컴포넌트
    public RectTransform content;          // Content 객체
    public List<GameObject> items;          // 개별 아이템들 (자식 요소)
    public int showPickUpevent;
    public float snapSpeed = 10f;          // 스냅 속도
    public Sprite[] ongoingEvent;

    [SerializeField]
    private Vector2 targetPosition;        // 스냅할 목표 위치
    [SerializeField]
    Vector2 currentPos;

    [SerializeField]
    private GameObject contextChildObj;
    private float contextObjWidth;
    private float halfContextWidth;
    private bool bisSnapping;

    private void Start()
    {
        currentPos = content.anchoredPosition;
        contextObjWidth = contextChildObj.GetComponent<RectTransform>().sizeDelta.x;
        halfContextWidth = contextObjWidth * 0.5f;

        content.sizeDelta = new Vector2(contextObjWidth * (float)showPickUpevent, content.anchoredPosition.y);
        for(int i = 0; i < showPickUpevent; i++)
        {
            GameObject newItem = Instantiate(contextChildObj,content);
            newItem.name = i.ToString();
            newItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * contextObjWidth, content.anchoredPosition.y);
            newItem.GetComponent<Image>().sprite = ongoingEvent[i];
            items.Add(newItem);
        }
    }

    private void Update()
    {
        if (bisSnapping)
        {
            // 타겟 위치로 부드럽게 스냅
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.deltaTime * snapSpeed);

            // 목표 위치에 도달하면 스냅 중지
            if (Vector2.Distance(content.anchoredPosition, targetPosition) < 0.1f)
            {
                content.anchoredPosition = targetPosition;
                scrollRect.velocity = Vector2.zero;
                bisSnapping = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            currentPos = content.anchoredPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            SnapToNearest();
        }
    }

    // 가장 가까운 아이템으로 스냅하는 함수
    private void SnapToNearest()
    {
        float minDistance = float.MaxValue;
        GameObject  closestItem = null;
       
        currentPos = content.anchoredPosition;

        // 각 아이템의 위치를 순회하며 가장 가까운 아이템 찾기
        for(int i = 0; i < items.Count; i++)
        {
            if(currentPos.x >= 0.0f)
            {
                if (items[i].transform.localPosition.x > 0.0f)
                    continue;
            }
            else
            {
                if (items[i].transform.localPosition.x < 0.0f)
                    continue;
            }

            float distance  = items[i].transform.localPosition.x +  currentPos.x;
            if (Mathf.Abs(distance) < minDistance)
            {
                minDistance = Mathf.Abs(distance);
                closestItem = items[i];
            }
        }

        // 가장 가까운 아이템이 있으면 해당 위치로 스냅 시작
        if (closestItem != null)
        {
            targetPosition = closestItem.GetComponent<RectTransform>().anchoredPosition * -1;
            bisSnapping = true;
        }
    }

    private void Relocationitem (GameObject Item)
    {
        float contentX = content.anchoredPosition.x;
        if(Item.transform.localPosition.x + contentX < -contextObjWidth )
        {
            Item.transform.localPosition += new Vector3(items.Count * contextObjWidth, 0.0f, 0.0f);
        }

        if (Item.transform.localPosition.x + contentX > contextObjWidth )
        {
            Item.transform.localPosition += new Vector3(items.Count * -contextObjWidth, 0.0f, 0.0f);
        }
    }
    public void CallbackChagneValue()
    {
        foreach (var item in items)
        {
            Relocationitem(item.gameObject);
        }
    }
}

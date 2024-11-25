using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LodingScene : MonoBehaviour
{
    private string NextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneChangeManager.GetInstance != null)
        {
            NextSceneName = SceneChangeManager.GetInstance.NextSceneName;

            if (NextSceneName != "")
                StartCoroutine(LoadSceneCorutine());
        }
    }

    IEnumerator LoadSceneCorutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextSceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            // �̶� �ִϸ��̼� ���

            yield return null;
        }

        yield return new WaitForSeconds(1); // �߰����� �ε� ��� �ð�

        asyncLoad.allowSceneActivation = true;
    }
}

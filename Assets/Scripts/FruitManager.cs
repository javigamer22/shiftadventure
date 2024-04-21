using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitManager : MonoBehaviour
{
    public string nameSceneNext;
    
    public void Update()
    {
        if (getFruitCount() == 0)
        {
            Debug.LogError("Juego Acabado!!!!!");
            goToNextScene();
        }
    }

    private bool CheckIfSceneExists(string sceneName)
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            Debug.Log(scenePath + " " + sceneNameFromPath);

            if (sceneNameFromPath == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    public int getFruitCount()
    {
        return transform.childCount;
    }

    public bool goToNextScene()
    {
        bool result = false;

        if (CheckIfSceneExists(nameSceneNext)) {
            SceneManager.LoadScene(nameSceneNext);
            result = true;
        }
        else
        {
            Debug.LogError("Ese nivel NO existe!!!");
        }

        return result;
    }
}

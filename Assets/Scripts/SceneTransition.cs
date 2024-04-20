using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneTransition : MonoBehaviour
{
    // Метод для перехода на другую сцену по имени
    public void GoToSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Метод для перехода на другую сцену по индексу
    public void GoToSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoToLink(string link){
        Application.OpenURL(link);
    }
}
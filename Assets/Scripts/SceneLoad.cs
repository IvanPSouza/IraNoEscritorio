using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoad : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
    public void IQuit()
    {
        Application.Quit();
    }
    public void InAndOut(GameObject inAndOut)
    {
        inAndOut.SetActive(true);
    }
    public void OutAndIn(GameObject outAndIn)
    {
        outAndIn.SetActive(false);
    }
}

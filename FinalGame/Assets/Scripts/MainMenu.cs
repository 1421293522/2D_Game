using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);//把1替换成要载入的场景
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

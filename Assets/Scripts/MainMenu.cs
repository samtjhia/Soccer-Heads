using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Instructions;

    public void PlaySingleGame()
    {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void PlayMultiGame()
    {
        SceneManager.LoadScene("MultiPlayer");
    }

    public void OpenInstructions()
    {
        Instructions.SetActive(true);
    }

    public void CloseInstructions()
    {
        Instructions.SetActive(false);
    }

}

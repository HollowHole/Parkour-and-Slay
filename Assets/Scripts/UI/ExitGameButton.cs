using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ExitGame);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    // Start is called before the first frame update
    bool isOpen;
    private void Start()
    {
        Resume();
    }
    public void Resume()
    {
        transform.localScale = Vector3.zero;
        isOpen = false;
        TimeMgr.Instance.ResumeGame();
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("Start");
    }
    public void OpenSettingMenu()
    {
        transform.localScale = Vector3.one;
        TimeMgr.Instance.PauseGame();
        isOpen = true;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isOpen)
                Resume();
            else
                OpenSettingMenu();
        }
    }

}

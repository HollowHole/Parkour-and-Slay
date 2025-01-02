using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartGameButton: MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
    }
    void StartGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string MainSceneName;

    public void StartGame() {
        SceneManager.LoadScene(MainSceneName);
    }
}
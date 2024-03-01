using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject exitButton;
    private void Awake()
    {
        if (BuildConstants.isMobile) Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value; // fix mobile FPS
    }
    private void Start()
    {
        if (BuildConstants.isWebGL || BuildConstants.isMobile)
        {
            exitButton.SetActive(false);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}

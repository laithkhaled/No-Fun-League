using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Canvas canvasToDisable;

    public void Level1Button()
    {
        SceneManager.LoadScene("JoeLevel");
    }

    public void Level2Button()
    {
        SceneManager.LoadScene("BobaLevel");
    }

    public void Level3Button()
    {
        SceneManager.LoadScene("DuckLevel");
    }

    public void Level4Button()
    {
        SceneManager.LoadScene("TanukiLevel");
    }

    public void Level5Button()
    {
        SceneManager.LoadScene("SnakeLevel");
    }

    public void Level6Button()
    {
        SceneManager.LoadScene("BucLevel");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DisableCanvas()
    {
        canvasToDisable.enabled = false;
    }

    public void EnableCanvas()
    {
        canvasToDisable.enabled = true;
    }
}
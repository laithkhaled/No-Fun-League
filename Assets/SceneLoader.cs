using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Level1Button()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2Button()
    {
        //SceneManager.LoadScene("Level2");
        Debug.Log("Level2");
    }

    public void Level3Button()
    {
        //SceneManager.LoadScene("Level3");
        Debug.Log("Level3");
    }

    public void Level4Button()
    {
        //SceneManager.LoadScene("Level4");
        Debug.Log("Level4");
    }

    public void Level5Button()
    {
        //SceneManager.LoadScene("Level5");
        Debug.Log("Level5");
    }

    public void Level6Button()
    {
        //SceneManager.LoadScene("Level6");
        Debug.Log("Level6");
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
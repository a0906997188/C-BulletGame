using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoBackButtonController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoBackButton_Click()
    {
        SceneManager.LoadScene(0);
    }

    public void ReStartButton_Click()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevelButton_Click()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

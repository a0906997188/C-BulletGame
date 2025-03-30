using UnityEngine;

public class GoBackButtonController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

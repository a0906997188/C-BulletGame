using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
    public void OpenLevelChoosePanel()
    {
        GameObject a = transform.GetChild(3).gameObject;
        a.SetActive(!a.activeInHierarchy);
    }


    public void LoadLevel0()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(3);
    }


}

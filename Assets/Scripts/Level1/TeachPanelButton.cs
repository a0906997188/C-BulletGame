using UnityEngine;

public class TeachPanelButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CloseTeachPanel()
    {
        this.transform.parent.gameObject.SetActive(false);
        GameManager.Instance.StartTheGame();
        
    }
}

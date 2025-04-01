using System.Collections;
using UnityEngine;

public class DreamController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ItWillDispear());
    }
    /// <summary>
    /// ´X¬í®ø¥¢
    /// </summary>
    /// <returns></returns>
    IEnumerator ItWillDispear()
    {
        yield return new WaitForSeconds(GameManager.Instance.DreamDispearTime);
        if(gameObject.CompareTag("Dream"))
        {
            GameManager.Instance.DisappearDream(gameObject, 0);
        }
        else if (gameObject.CompareTag("Vaxation"))
        {
            GameManager.Instance.EnterBoysBrain(gameObject, -1);
        }
    }


}

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
    /// 3.5¬í«á®ø¥¢
    /// </summary>
    /// <returns></returns>
    IEnumerator ItWillDispear()
    {
        yield return new WaitForSeconds(3.5f);
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

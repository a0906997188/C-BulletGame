using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void PlayMusic(bool b)
    {
        if(b)
        {
            AudioSource g = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
            g.DOPlay();
            g.DOFade(0.4f, 3);
        }
        else
        {
            AudioSource g = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
            g.DOFade(0, 3).OnComplete(() => { Destroy(gameObject); });
        }

    }

    /// <summary>
    /// 等待音樂播放完畢
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public IEnumerator WaitForMusicEnd(GameObject a)
    {
        AudioSource b = a.transform.GetChild(0).GetComponent<AudioSource>();
        yield return new WaitWhile(() => b.isPlaying);
    }


}

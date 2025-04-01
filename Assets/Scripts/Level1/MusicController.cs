using DG.Tweening;
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
            g.Play();
            g.DOFade(0.4f, 3);
        }
        else
        {
            AudioSource g = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
            g.DOFade(0, 3).OnComplete(() => { Destroy(gameObject); });
        }
    }
}

using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float StartTime = 30f;
    public GameObject Dream;
    public GameObject Vaxation;
    GameObject[] DreamArray;
    public GameObject FatherImage;

    //分數
    public int Score = 0;
    public GameObject ScoreObject;
    public GameObject TimeDownCount;
    TextMeshProUGUI ScoreText;//記分板
    TextMeshProUGUI TimeLast;//時間倒數
    public bool onTime = false;//時間到了
    public int TargetScore = 10;//目標分數
    public GameObject TargetScoreObject;//目標分數的物件
    TextMeshProUGUI TargetScoreText;//目標分數的文字

    public GameObject Boy;//男孩

    public int FatherDisappearTime = 10;//父親消失時間


    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        DreamArray =new GameObject[]{ Dream, Vaxation };
        ScoreText = ScoreObject.GetComponent<TextMeshProUGUI>();
        TimeLast = TimeDownCount.GetComponent<TextMeshProUGUI>();
        TargetScoreText = TargetScoreObject.GetComponent<TextMeshProUGUI>();
        TargetScoreText.text = "Target Score: " + TargetScore;
    }
    private void FixedUpdate()
    {
        ScoreText.text = "Score:" + Score;
        ifMouseClick();

    }


    /// <summary>
    /// 開始遊戲的button
    /// </summary>
    public void StartTheGame()
    {
        MusicController.Instance.PlayMusic(true);
        StartCoroutine(TheFather());
        StartCoroutine(InstantiateDream());
        StartCoroutine(StartCountdown(StartTime));
    }

    /// <summary>
    /// 滑鼠點擊事件
    /// </summary>
    public void ifMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 15);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Dream"))
                {
                    EnterBoysBrain(hit.collider.gameObject, 1);
                    hit.collider.enabled = false;
                }
                if (hit.collider.CompareTag("Vaxation"))
                {
                    EnterBoysBrain(hit.collider.gameObject, -1);
                    hit.collider.enabled = false;
                }
            }
        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Dream"))
                {
                    DisappearDream(hit.collider.gameObject, 0);
                    hit.collider.enabled = false;
                }
                if (hit.collider.CompareTag("Vaxation"))
                {
                    DisappearDream(hit.collider.gameObject, 1);
                    hit.collider.enabled = false;
                }
            }
        }
    }


    /// <summary>
    /// 父親消失
    /// </summary>
    /// <returns></returns>
    IEnumerator TheFather()
    {
        yield return new WaitForSeconds(FatherDisappearTime);
        FatherImage.GetComponent<Image>().DOFade(0, 10f).OnComplete(() => { FatherImage.SetActive(false); });

    }


    /// <summary>
    /// 生成夢境
    /// </summary>
    /// <returns></returns>
    IEnumerator InstantiateDream()
    {   float MaxRandomTime = 3f;
        //隨機時間到就生成夢境
        while (true)
        {
            GameObject claude = Instantiate(DreamArray[Random.Range(0,2)],new Vector2(Random.Range(-7.75f,7f),Random.Range(-4.65f,3.5f)),Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0, MaxRandomTime));
            if (onTime)
            {
                break;
            }
        }

    }

    /// <summary>
    /// 夢境消失
    /// </summary>
    public void DisappearDream(GameObject de,int AddScore)
    {
        //消失的動畫
        
        de.transform.DOScale(0, 1f).OnComplete(() => { Destroy(de); Score += AddScore; });
        
    }

    /// <summary>
    /// 進入男孩的腦中
    /// </summary>
    public void EnterBoysBrain(GameObject de,int AddScore)
    {
        de.transform.DOMove(Boy.GetComponent<RectTransform>().position, 1f).OnComplete(() => { Destroy(de); Score += AddScore; });
    }

    /// <summary>
    /// 開始倒數計時
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator StartCountdown(float time)
    {
        float remainingTime = time;
        while (remainingTime > 0)
        {
            TimeLast.text = "Time: " + remainingTime.ToString();
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }
        TimeLast.text = "Time: 0";
        onTime = true; // 時間到
        WinOrLose();
    }

    void WinOrLose()
    {
        if (Score >= TargetScore)
        {
            MusicController.Instance.PlayMusic(false);
            Debug.Log("Win");
            SceneManager.LoadScene(4);
        }
        else
        {
            MusicController.Instance.PlayMusic(false);
            Debug.Log("Lose");
            SceneManager.LoadScene(5);
        }
    }
}

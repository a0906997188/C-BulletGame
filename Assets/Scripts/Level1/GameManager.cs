using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float StartTime = 30f;
    public GameObject[] Dreams;//�x�s�ڪ��}�C
    public GameObject[] Vaxations;//�x�s�дo���}�C
    public float DreamDispearTime = 3;
    public GameObject FatherImage;

    //����
    public int Score = 0;
    public GameObject ScoreObject;
    public GameObject TimeDownCount;
    TextMeshProUGUI ScoreText;//�O���O
    TextMeshProUGUI TimeLast;//�ɶ��˼�
    public int TargetScore = 10;//�ؼФ���
    public GameObject TargetScoreObject;//�ؼФ��ƪ�����
    TextMeshProUGUI TargetScoreText;//�ؼФ��ƪ���r

    public GameObject Boy;//�k��

    public int FatherDisappearTime = 10;//���ˮ����ɶ�
    bool onTime = false;


    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
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
    /// �}�l�C����button
    /// </summary>
    public void StartTheGame()
    {
        MusicController.Instance.PlayMusic(true);
        if(FatherImage != null) StartCoroutine(TheFather());
        StartCoroutine(InstantiateDream());
        StartCoroutine(StartCountdown(StartTime));
    }

    /// <summary>
    /// �ƹ��I���ƥ�
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
    /// ���ˮ���
    /// </summary>
    /// <returns></returns>
    IEnumerator TheFather()
    {
        yield return new WaitForSeconds(FatherDisappearTime);
        FatherImage.GetComponent<Image>().DOFade(0, 10f).OnComplete(() => { FatherImage.SetActive(false); });

    }


    /// <summary>
    /// �ͦ��ڹ�
    /// </summary>
    /// <returns></returns>
    IEnumerator InstantiateDream()
    {   float MaxRandomTime = 2.5f;
        GameObject g = new GameObject("Dream");
        //�H���ɶ���N�ͦ��ڹ�
        while (true)
        {
            GameObject[][] claudes = new GameObject[][] {Dreams,Vaxations};//�x�s�ڤηдo��2���Ʋ�
            GameObject[] claude = claudes[Random.Range(0, claudes.Length)];//�H�����کάO�дo
            GameObject c = claude[Random.Range(0, claude.Length)];//�H���쪺����
            int a = claude.Length;//�H���쪺�}�C����
            GameObject Ins = Instantiate(c,new Vector2(Random.Range(-8,8.5f),Random.Range(-4,4f)),Quaternion.identity);
            Ins.transform.SetParent(g.transform);
            yield return new WaitForSeconds(Random.Range(0, MaxRandomTime));
            if (onTime)
            {
                Destroy(g);
                break;
            }
        }

    }

    /// <summary>
    /// �ڹҮ���
    /// </summary>
    public void DisappearDream(GameObject de,int AddScore)
    {
        //�������ʵe
        
        de.transform.DOScale(0, 1f).OnComplete(() => { Destroy(de); Score += AddScore; });
        
    }

    /// <summary>
    /// �i�J�k�Ī�����
    /// </summary>
    public void EnterBoysBrain(GameObject de,int AddScore)
    {
        de.transform.DOMove(Boy.GetComponent<RectTransform>().position, 1f).OnComplete(() => { Destroy(de); Score += AddScore; });
    }

    /// <summary>
    /// �}�l�˼ƭp��
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
        onTime = true; // �ɶ���
        WinOrLose();
    }

    public GameObject WinPanel;
    public GameObject LosePanel;
    void WinOrLose()
    {
        if (Score >= TargetScore)
        {
            MusicController.Instance.PlayMusic(false);
            Debug.Log("Win");
            WinPanel.SetActive(!WinPanel.activeInHierarchy);
        }
        else
        {
            MusicController.Instance.PlayMusic(false);
            Debug.Log("Lose");
            LosePanel.SetActive(!WinPanel.activeInHierarchy);
        }
    }
}

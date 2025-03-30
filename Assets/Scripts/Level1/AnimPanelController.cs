using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimPanelController : MonoBehaviour
{
    public GameObject MouseImage1;
    public GameObject MouseImage2;

    [Space(10)]public GameObject Claude1;
    public GameObject Claude2;


    [Space(10)]public Sprite LeftMouse;
    public Sprite RightMouse;

    Image image1;
    Image image2;
    Sprite temp_Image;
    private void Awake()
    {
        image1 = MouseImage1.GetComponent<Image>();
        image2 = MouseImage2.GetComponent<Image>();
        temp_Image = image1.sprite;
        StartCoroutine(PlayTheStartAnim());
    }

    IEnumerator PlayTheStartAnim()
    {
        Vector2 temp = image1.rectTransform.anchoredPosition;
        Vector2 temp2 = image2.rectTransform.anchoredPosition;
        MoveUIImageToTarget(MouseImage1, Claude1, 1.5f);
        MoveUIImageToTarget(MouseImage2, Claude2, 1.5f);
        yield return new WaitForSeconds(1.5f);

        image1.sprite = LeftMouse;
        image2.sprite = RightMouse;
        yield return new WaitForSeconds(0.5f);

        MouseImage1.GetComponent<RectTransform>().anchoredPosition = temp;
        MouseImage2.GetComponent<RectTransform>().anchoredPosition = temp2;


        image1.sprite = temp_Image;
        image2.sprite = temp_Image;

        StartCoroutine(PlayTheStartAnim());

    }

    //移動的邏輯
    void MoveUIImageToTarget(GameObject image, GameObject target ,float during)
    {
        RectTransform imageRectTransform = image.GetComponent<RectTransform>();
        RectTransform targetRectTransform = target.GetComponent<RectTransform>();

        // 使用 DOTween 進行動畫
        imageRectTransform.DOAnchorPos(targetRectTransform.anchoredPosition, during);
    }
}

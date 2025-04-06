using System.Collections;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    public GameObject cardPrefab; // 卡牌预制体
    public Transform handPanel;   // 手牌区域的父对象
    public float duration = 0.15f;

    private Coroutine coroutine;


    private void Update()
    {
        if (Input.GetButton("shift") && GetComponent<RectTransform>().position.y != 0)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(MoveUIElement(new Vector3(0, 0, 0), duration));
        }
        else if (GetComponent<RectTransform>().position.y != -230)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine =  StartCoroutine(MoveUIElement(new Vector3(0, -230, 0), duration));
        };
    }

    // 添加卡牌到手牌区域
    public void AddCardToHand(CardData cardData)
    {
        Debug.Log("进入到添加手牌的代码段");
        GameObject card = Instantiate(cardPrefab, handPanel);
        CardUI cardUI = card.GetComponent<CardUI>();
        if (cardUI != null)
        {
            cardUI.Setup(cardData);
        }
        else
        {
            Debug.LogError("CardUI 组件未找到！");
        }
    }

    // 清空手牌区域
    public void ClearHand()
    {
        foreach (Transform child in handPanel)
        {
            Destroy(child.gameObject);
        }
    }


    private IEnumerator MoveUIElement(Vector3 target, float duration)
    {
        Vector3 start = GetComponent<RectTransform>().anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(start, target, elapsed / duration);
            yield return null;
        }

        GetComponent<RectTransform>().anchoredPosition = target;
        coroutine = null;
    }
}

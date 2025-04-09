using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject cardPrefab;
    public Transform handPanel;
    public float slideDuration = 0.15f;
    public float visibleYPosition = 0f;
    public float hiddenYPosition = -230f;

    private Coroutine _slideCoroutine;
    private bool _isHandVisible;

    private void Update()
    {
        bool shouldShow = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (shouldShow != _isHandVisible)
        {
            _isHandVisible = shouldShow;
            SlideHandUI(_isHandVisible);
        }
    }

    public void AddCardToHand(Card.Data cardData) // 修改为 Card.Data
    {
        if (cardPrefab == null || handPanel == null) return;

        GameObject cardObj = Instantiate(cardPrefab, handPanel);
        CardUI cardUI = cardObj.GetComponent<CardUI>();

        if (cardUI != null)
        {
            cardUI.Setup(cardData);
        }
    }

    public void RemoveCardFromHand(Card.Data cardData) // 修改为 Card.Data
    {
        foreach (Transform child in handPanel)
        {
            CardUI cardUI = child.GetComponent<CardUI>();
            if (cardUI != null && cardUI.GetCardData() == cardData)
            {
                Destroy(child.gameObject);
                return;
            }
        }
    }

    private void SlideHandUI(bool show)
    {
        if (_slideCoroutine != null) StopCoroutine(_slideCoroutine);

        Vector3 target = show ?
            new Vector3(0, visibleYPosition, 0) :
            new Vector3(0, hiddenYPosition, 0);

        _slideCoroutine = StartCoroutine(SlideAnimation(target));
    }

    private IEnumerator SlideAnimation(Vector3 target)
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 start = rt.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            rt.anchoredPosition = Vector3.Lerp(start, target, elapsed / slideDuration);
            yield return null;
        }

        rt.anchoredPosition = target;
        _slideCoroutine = null;
    }
}
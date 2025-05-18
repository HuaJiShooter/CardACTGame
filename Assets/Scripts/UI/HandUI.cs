using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{
    [Header("UI Settings")]
    public float slideDuration = 0.15f;
    public float visibleYPosition = 0f;
    public float hiddenYPosition = -230f;

    private Coroutine _slideCoroutine;
    private bool _isHandVisible;

    private void Update()
    {
        // ±Õ£ΩÁ√Ê
        bool shouldShow = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (shouldShow != _isHandVisible)
        {
            _isHandVisible = shouldShow;
            SlideHandUI(_isHandVisible);
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
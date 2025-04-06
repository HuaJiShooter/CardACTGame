using UnityEngine;
using TMPro;

public class FeeUI : MonoBehaviour
{
    public GameObject player; // ��Ҷ���
    private FeeManager feeManager; // FeeManager ���
    private TextMeshProUGUI feeText; // TextMeshPro ���

    void Start()
    {
        // ��ȡ FeeManager ���
        feeManager = player.GetComponent<FeeManager>();

        // ��ȡ TextMeshPro ���
        feeText = GetComponentInChildren<TextMeshProUGUI>();

        // ��ʼ����ʾ
        UpdateFeeText();
    }

    void Update()
    {
        // ÿ֡���·�����ʾ
        UpdateFeeText();
    }

    void UpdateFeeText()
    {
        if (feeManager != null && feeText != null)
        {
            feeText.text = $"Current Fee: {feeManager.GetCurrentFee()}";
        }
    }
}


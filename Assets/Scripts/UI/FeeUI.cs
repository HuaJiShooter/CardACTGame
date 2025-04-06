using UnityEngine;
using TMPro;

public class FeeUI : MonoBehaviour
{
    public GameObject player; // 玩家对象
    private FeeManager feeManager; // FeeManager 组件
    private TextMeshProUGUI feeText; // TextMeshPro 组件

    void Start()
    {
        // 获取 FeeManager 组件
        feeManager = player.GetComponent<FeeManager>();

        // 获取 TextMeshPro 组件
        feeText = GetComponentInChildren<TextMeshProUGUI>();

        // 初始化显示
        UpdateFeeText();
    }

    void Update()
    {
        // 每帧更新费用显示
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


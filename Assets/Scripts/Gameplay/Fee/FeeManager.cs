using System;
using UnityEngine;

public class FeeManager : MonoBehaviour
{
    public float MaxFee = 100f; // 最大费用值
    public float RecoveryRate = 5f; // 每秒恢复的费用值
    private float currentFee; // 当前费用值

    // 定义费用变化事件
    public event Action<float, float> OnFeeChanged;

    private void Start()
    {
        currentFee = 0;
        NotifyFeeChanged();
    }

    private void Update()
    {
        RecoverFee();
        //LogFee();
    }

    private void LogFee()
    {
        Debug.Log("目前费用为");
        Debug.Log(currentFee);
    }

    // 恢复费用
    private void RecoverFee()
    {
        if (currentFee < MaxFee)
        {
            currentFee += RecoveryRate * Time.deltaTime;
            currentFee = Mathf.Min(currentFee, MaxFee);
            NotifyFeeChanged();
        }
    }

    // 消耗费用
    public bool ConsumeFee(float amount)
    {
        if (currentFee >= amount)
        {
            currentFee -= amount;
            NotifyFeeChanged();
            return true;
        }
        return false;
    }

    // 获取当前费用值
    public float GetCurrentFee()
    {
        return currentFee;
    }

    // 通知费用变化
    private void NotifyFeeChanged()
    {
        OnFeeChanged?.Invoke(currentFee, MaxFee);
    }
}
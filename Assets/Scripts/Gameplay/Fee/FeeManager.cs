using System;
using UnityEngine;

public class FeeManager : MonoBehaviour
{
    public float MaxFee = 100f; // ������ֵ
    public float RecoveryRate = 5f; // ÿ��ָ��ķ���ֵ
    private float currentFee; // ��ǰ����ֵ

    // ������ñ仯�¼�
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
        Debug.Log("Ŀǰ����Ϊ");
        Debug.Log(currentFee);
    }

    // �ָ�����
    private void RecoverFee()
    {
        if (currentFee < MaxFee)
        {
            currentFee += RecoveryRate * Time.deltaTime;
            currentFee = Mathf.Min(currentFee, MaxFee);
            NotifyFeeChanged();
        }
    }

    // ���ķ���
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

    // ��ȡ��ǰ����ֵ
    public float GetCurrentFee()
    {
        return currentFee;
    }

    // ֪ͨ���ñ仯
    private void NotifyFeeChanged()
    {
        OnFeeChanged?.Invoke(currentFee, MaxFee);
    }
}
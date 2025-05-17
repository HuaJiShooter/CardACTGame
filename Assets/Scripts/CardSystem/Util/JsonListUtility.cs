using System;
using System.Collections.Generic;
using UnityEngine;

/// ���ַ����б����л� / �����л��� JSON��TEXT����
/// ������ SQLite �� TEXT �У���ȡ������ԭ List<string>��
public static class JsonListUtility
{
    /// JsonUtility ����ֱ�Ӵ���List����Ҫ��һ������л�����
    [Serializable]
    private class Wrapper
    {
        public List<string> list = new List<string>();
    }

    /// �� List&lt;string&gt; ת�� JSON �ַ�����
    public static string Serialize(List<string> list)
    {
        // false = ����������ʡ�洢�ռ䣻����ɶ��Ըĳ� true
        return JsonUtility.ToJson(new Wrapper { list = list ?? new List<string>() }, false);
    }

    /// �� JSON �ַ�����ԭ�� List&lt;string&gt;��
    public static List<string> Deserialize(string json)
    {
        if (string.IsNullOrEmpty(json))
            return new List<string>();

        var wrapper = JsonUtility.FromJson<Wrapper>(json);
        return wrapper?.list ?? new List<string>();
    }

    /// ---------- ������д JSON TEXT ʱ���õ�С���� ----------

    /// �� JSON �ַ�������б�׷��Ԫ�أ����Ѵ�������ԣ���
    public static string Add(string json, string element)
    {
        var list = Deserialize(json);
        if (!list.Contains(element))
            list.Add(element);
        return Serialize(list);
    }

    /// �� JSON �ַ�������б��Ƴ�Ԫ�أ��������������·�������
    public static string Remove(string json, string element)
    {
        var list = Deserialize(json);
        list.Remove(element);
        return Serialize(list);
    }

    // ---------- ��˳�ֵ���չ���� ----------

    /// string ��չ��ֱ�� .ToStringList() �� JSON TEXT ��ԭ���б�
    public static List<string> ToStringList(this string json) => Deserialize(json);

    /// List ��չ��ֱ�� .ToJson() ���� TEXT�������������ݿ⡣
    public static string ToJson(this List<string> list) => Serialize(list);
}


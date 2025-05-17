using System;
using System.Collections.Generic;
using UnityEngine;

/// 把字符串列表序列化 / 反序列化到 JSON（TEXT），
/// 方便存进 SQLite 的 TEXT 列，再取出来还原 List<string>。
public static class JsonListUtility
{
    /// JsonUtility 不能直接处理List，需要包一层可序列化对象。
    [Serializable]
    private class Wrapper
    {
        public List<string> list = new List<string>();
    }

    /// 把 List&lt;string&gt; 转成 JSON 字符串。
    public static string Serialize(List<string> list)
    {
        // false = 不缩进，节省存储空间；如需可读性改成 true
        return JsonUtility.ToJson(new Wrapper { list = list ?? new List<string>() }, false);
    }

    /// 把 JSON 字符串还原成 List&lt;string&gt;。
    public static List<string> Deserialize(string json)
    {
        if (string.IsNullOrEmpty(json))
            return new List<string>();

        var wrapper = JsonUtility.FromJson<Wrapper>(json);
        return wrapper?.list ?? new List<string>();
    }

    /// ---------- 下面是写 JSON TEXT 时常用的小助手 ----------

    /// 向 JSON 字符串里的列表追加元素（如已存在则忽略）。
    public static string Add(string json, string element)
    {
        var list = Deserialize(json);
        if (!list.Contains(element))
            list.Add(element);
        return Serialize(list);
    }

    /// 从 JSON 字符串里的列表移除元素（若不存在则无事发生）。
    public static string Remove(string json, string element)
    {
        var list = Deserialize(json);
        list.Remove(element);
        return Serialize(list);
    }

    // ---------- 更顺手的扩展方法 ----------

    /// string 扩展：直接 .ToStringList() 把 JSON TEXT 还原成列表。
    public static List<string> ToStringList(this string json) => Deserialize(json);

    /// List 扩展：直接 .ToJson() 生成 TEXT，用来塞进数据库。
    public static string ToJson(this List<string> list) => Serialize(list);
}


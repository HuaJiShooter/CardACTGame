```C#
// 1. 准备要存进数据库的新记录
var tagList = new List<string> { "consumable", "healing" };
row.TagsJson = JsonListUtility.Serialize(tagList);
conn.Insert(row);

// 2. 查询
var loaded = conn.Find<ItemRow>(row.Id);
var tags   = JsonListUtility.Deserialize(loaded.TagsJson);
// 3. 修改
tags.Add("rare");
loaded.TagsJson = JsonListUtility.Serialize(tags);
conn.Update(loaded);

// 也可以用扩展方法的写法：
var tags2  = loaded.TagsJson.ToStringList();
tags2.Remove("healing");
loaded.TagsJson = tags2.ToJson();
conn.Update(loaded);
```
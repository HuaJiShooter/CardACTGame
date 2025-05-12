#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
public class CardDataCreator
{
    [MenuItem("Assets/Create/Card Data")]
    public static void CreateCardData()
    {
        var data = ScriptableObject.CreateInstance<Card.Data>();
        AssetDatabase.CreateAsset(data, "Assets/Resources/CardData/NewCardData.asset");
        AssetDatabase.SaveAssets();
    }
}
#endif

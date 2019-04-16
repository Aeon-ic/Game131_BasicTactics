using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateCustomAIList : MonoBehaviour
{
    [MenuItem("Assets/Create/Custom AI List")]
    public static CustomAIList Create()
    {
        CustomAIList asset = ScriptableObject.CreateInstance<CustomAIList>();

        AssetDatabase.CreateAsset(asset, "Assets/InventoryItemList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}

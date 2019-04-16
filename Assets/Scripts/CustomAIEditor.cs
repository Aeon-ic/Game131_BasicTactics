using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomAIEditor : EditorWindow
{
    public CustomAIList customAIList;
    private int viewIndex = 1;

    [MenuItem("Window/Inventory Item Editor %#e")]
    static void Init()
    {
        GetWindow(typeof(CustomAIList));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            customAIList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(CustomAIList)) as CustomAIList;
        }
    }

    void CreateNewCustomAIList()
    {
        viewIndex = 1;
        customAIList = CreateCustomAIList.Create();
        if (customAIList)
        {
            customAIList.customAIs = new List<CustomAI>();
            string relPath = AssetDatabase.GetAssetPath(customAIList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenCustomAIList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Custom AI List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            customAIList = AssetDatabase.LoadAssetAtPath(relPath, typeof(CustomAIList)) as CustomAIList;
            if (customAIList.customAIs == null)
                customAIList.customAIs = new List<CustomAI>();
            if (customAIList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddCustomAI()
    {
        CustomAI newAI = new CustomAI();
        newAI.highestLowest = CustomAI.HighestLowest.Highest;
        customAIList.customAIs.Add(newAI);
        viewIndex = customAIList.customAIs.Count;
    }

    void DeleteAI(int index)
    {
        customAIList.customAIs.RemoveAt(index);
    }
}

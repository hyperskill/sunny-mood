using UnityEditor;
using UnityEngine;

public class StageHelper : MonoBehaviour
{
    bool PlatformTagExist = false;
    bool EnemyTagExist = false;
    
    private void Start()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals("Platform")) { PlatformTagExist = true;}
            if (t.stringValue.Equals("Enemy")) { EnemyTagExist = true;}
        }
    }

    public void RemovePlatforms()
    {
        if (!PlatformTagExist)
        {
            return;
        }

        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            Destroy(platform);
        }
    }
    
    public void RemoveEnemies()
    {
        if (!EnemyTagExist)
        {
            return;
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    }
}

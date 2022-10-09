using UnityEditor;
using UnityEngine;

public class StageHelper : MonoBehaviour
{
    bool PlatformTagExist = false;
    bool EnemyTagExist = false;

    private void Start()
    {
        SerializedObject tagManager =
            new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals("Platform"))
            {
                PlatformTagExist = true;
            }

            if (t.stringValue.Equals("Enemy"))
            {
                EnemyTagExist = true;
            }
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

public class PlatformInfo : MonoBehaviour
{
    [HideInInspector] public Vector2 leftBottom, rightBottom, leftUp, rightUp;

    public Vector2 center;
    public Vector2 size;

    public bool reachable = false;

    private BoxCollider2D col;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        center = col.bounds.center;
        size = col.size;
        rightUp = (center + new Vector2(size.x / 2 + col.edgeRadius, size.y / 2 + col.edgeRadius));
        rightBottom = (center + new Vector2(size.x / 2 + col.edgeRadius, -size.y / 2 - col.edgeRadius));
        leftUp = (center + new Vector2(-size.x / 2 - col.edgeRadius, size.y / 2 + col.edgeRadius));
        leftBottom = (center + new Vector2(-size.x / 2 - col.edgeRadius, -size.y / 2 - col.edgeRadius));
    }
}
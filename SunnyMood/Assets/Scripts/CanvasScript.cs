using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.offsetMax = Vector2.zero;
        rt.offsetMin = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

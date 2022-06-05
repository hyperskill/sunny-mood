using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class enemyScript : MonoBehaviour
{
    public SpriteRenderer sr;
    
    private float lastDirChangeTime;
    private float dir = -1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        lastDirChangeTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastDirChangeTime > 3.0f)
        {
            sr.flipX = !sr.flipX;
            lastDirChangeTime = Time.time;
            dir *= -1.0f;
        }
        
        transform.Translate(dir * 0.02f, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Level 3");
        }
    }
}

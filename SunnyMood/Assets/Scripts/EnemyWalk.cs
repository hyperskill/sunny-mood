using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWalk : MonoBehaviour
{
    private float lastDeltaX;
    private float deltax = -1.0f;
    private SpriteRenderer s;

    // Start is called before the first frame update
    void Start()
    {
        lastDeltaX = Time.time;
        s = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastDeltaX > 3.1f)
        {
            s.flipX = !s.flipX;
            lastDeltaX = Time.time;
            deltax *= -1;
        }
        transform.Translate(deltax * 0.03f, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Level 3");
        }
    }
}

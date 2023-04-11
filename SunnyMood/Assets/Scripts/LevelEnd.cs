using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            int currentLevel = Convert.ToInt32(SceneManager.GetActiveScene().name.Split()[1]) + 1;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            PlayerPrefs.Save();
            if(currentLevel<=3)
                SceneManager.LoadScene("Level "+currentLevel.ToString());
            else
                SceneManager.LoadScene("Main Menu");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
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
        //print("Door collision");
        if (other.name == "Player")
        {
            var currentScene = SceneManager.GetActiveScene().name;
            
            if (currentScene == "Level 1")
            {
                setMaxLevel(1);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Level 2");
            }
            else if (currentScene == "Level 2")
            {
                setMaxLevel(2);
                
                PlayerPrefs.Save();
                SceneManager.LoadScene("Level 3");
            }
            else if (currentScene == "Level 3")
            {
                setMaxLevel(3);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Level 3");
            }
            
        }
    }

    private void setMaxLevel(int thisLevel)
    {
        if (PlayerPrefs.HasKey("max_level"))
        {
            if (PlayerPrefs.GetInt("max_level") < thisLevel)
            {
                PlayerPrefs.SetInt("max_level", thisLevel);
            }
        }
        else
        {
            PlayerPrefs.SetInt("max_level", thisLevel);
        }
    }
}

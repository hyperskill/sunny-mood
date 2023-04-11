using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentLevel = 1;
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    void Start()
    {
        if (PlayerPrefs.HasKey("currentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel");
        }
        else
        {
            PlayerPrefs.SetInt("currentLevel", currentLevel);
        }

        level2Button.interactable = false;
        level3Button.interactable = false;
        if (currentLevel > 1)
        {
            if (currentLevel >= 2)
                level2Button.interactable = true;
            if (currentLevel == 3)
                level3Button.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButtonClick()
    {
        if(currentLevel<=3)
            SceneManager.LoadScene("Level "+currentLevel.ToString());
    }

    public void Level1ButtonClick()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Level2ButtonClick()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void Level3ButtonClick()
    {
        SceneManager.LoadScene("Level 3");
    }

}

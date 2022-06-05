using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class menuScript : MonoBehaviour
{
    public Button lvl1Btn;
    public Button lvl2Btn;
    public Button lvl3Btn;
    
    // Start is called before the first frame update
    void Start()
    {
        //lvl1Btn.interactable = false;

        if (PlayerPrefs.HasKey("max_level"))
        {
            var maxLevel = PlayerPrefs.GetInt("max_level");
            if (maxLevel >= 1)
            {
                lvl2Btn.interactable = true;
            }
            else
            {
                lvl2Btn.interactable = false;
            }
            
            if (maxLevel >= 2)
            {
                lvl3Btn.interactable = true;
            }
            else
            {
                lvl3Btn.interactable = false;
            }
        }
        else
        {
            lvl2Btn.interactable = false;
            lvl3Btn.interactable = false;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void PlayButton()
    {
        if (PlayerPrefs.HasKey("max_level"))
        {


            var maxLevel = PlayerPrefs.GetInt("max_level");
            switch (maxLevel)
            {
                case 1:
                    SceneManager.LoadScene("Level 2");
                    break;
                case 2:
                    SceneManager.LoadScene("Level 3");
                    break;
                case 3:
                    SceneManager.LoadScene("Level 3");
                    break;
                default:
                    SceneManager.LoadScene("Level 1");
                    break;
            }
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    public void ExitButton()
    {
        if (PlayerPrefs.HasKey("max_level"))
        {
            PlayerPrefs.DeleteKey("max_level");
        }
        Application.Quit();
    }
    
    public void Level1Button()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void Level2Button()
    {
        SceneManager.LoadScene("Level 2");
    }
    
    public void Level3Button()
    {
        SceneManager.LoadScene("Level 3");
    }
}

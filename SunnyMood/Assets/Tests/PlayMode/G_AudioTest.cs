using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using WindowsInput;
using WindowsInput.Native;
using UnityEngine.UI;

public class G_AudioTest
{
    private InputSimulator IS = new InputSimulator();
    private GameObject player, gem, ground;
    private AudioSource sTheme, sJump, sGem;
    
    [UnityTest, Order(0)]
    public IEnumerator InGameSoundCheck()
    {
        SceneManager.LoadScene("Main Menu");
        yield return null;
        
        sTheme = PMHelper.AudioSourcePlaying("Theme");
        yield return null;
        if (!sTheme) 
            Assert.Fail("There is no \"Theme\" AudioSource on \"Main Menu\" scene, after it was loaded");
        if (!sTheme.isPlaying)
            Assert.Fail("\"Theme\" AudioSource should be playing after scene was loaded");
        if (!sTheme.loop)
            Assert.Fail("\"Theme\" AudioSource should be looped");
        
        SceneManager.LoadScene("Level 1");
        yield return null;
        
        player = PMHelper.Exist("Player");
        ground = PMHelper.Exist("Ground");
        gem = GameObject.FindWithTag("Gem");
        yield return null;
        Collider2D playerCL = PMHelper.Exist<Collider2D>(player);
        Collider2D groundCL = PMHelper.Exist<Collider2D>(ground);
        
        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            playerCL.IsTouching(groundCL) || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail("Level 1: Player's start position is too high from ground");
        }
        
        yield return null;
        
        sTheme = PMHelper.AudioSourcePlaying("Theme");
        if (!sTheme) 
            Assert.Fail("There is no \"Theme\" AudioSource on levels' scenes, after it was loaded");
        if (!sTheme.isPlaying)
            Assert.Fail("\"Theme\" AudioSource should be playing after scene was loaded");
        if (!sTheme.loop)
            Assert.Fail("\"Theme\" AudioSource should be looped");
        

        yield return null;
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        yield return null;
        
        sJump = PMHelper.AudioSourcePlaying("Jump");
        if (!sJump)
            Assert.Fail("There is no \"Jump\" AudioSource when jump was performed");
        if (!sJump.isPlaying)
            Assert.Fail("\"Jump\" AudioSource should be played when player's jump is performed");
        if (sJump.loop)
            Assert.Fail("\"Jump\" AudioSource should not be looped");

        yield return null;
        gem.transform.position = playerCL.bounds.center;
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            !gem || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("Level 1: \"Gem\"s should be destroyed instantly when the player collides with them");
        }

        sGem = PMHelper.AudioSourcePlaying("PickGem");
        if (!sGem)
            Assert.Fail("There is no \"PickGem\" AudioSource when gem was collected");
        if (!sGem.isPlaying)
            Assert.Fail("\"PickGem\" sound should be played when player has collected gem");
        if (sGem.loop)
            Assert.Fail("\"PickGem\" sound should not be looped");
    }
}
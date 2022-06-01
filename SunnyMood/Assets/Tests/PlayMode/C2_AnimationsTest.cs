using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using WindowsInput;
using WindowsInput.Native;
using UnityEditor;

public class C2_Animations_Test
{
    private InputSimulator IS = new InputSimulator();
    private GameObject player, gem, levelEnd;
    private SpriteRenderer sr, gemSR;
    private Animator anim, gemAnim;
    private AnimationClip[] aclips, gemAclips;
    private AnimationClip idle, jump, walk, gemClip;
    private Collider2D playerCl, groundCl, gemCL, levelEndCl;
    
    
    [UnityTest, Order(1)]
    public IEnumerator NecessaryComponents()
    {
        Time.timeScale = 0;
        PMHelper.TurnCollisions(false);
        SceneManager.LoadScene("Level 2");
        yield return null;
        
        GameObject helperObj = new GameObject();
        StageHelper helper = helperObj.AddComponent<StageHelper>();
        yield return null;
        helper.RemovePlatforms();
        helper.RemoveEnemies();
        yield return null;

        levelEnd = PMHelper.Exist("LevelEnd");
        yield return null;
        levelEndCl = PMHelper.Exist<Collider2D>(levelEnd);
        levelEndCl.enabled = false;
        PMHelper.TurnCollisions(true);
        yield return null;
        
        player = PMHelper.Exist("Player");
        GameObject ground = PMHelper.Exist("Ground");
        yield return null;
        playerCl = PMHelper.Exist<Collider2D>(player);
        sr = PMHelper.Exist<SpriteRenderer>(player);
        anim = PMHelper.Exist<Animator>(player);
        groundCl = PMHelper.Exist<Collider2D>(ground);
        yield return null;

        if (!anim || !anim.enabled)
        {
            Assert.Fail(" Level 2: Player should have assigned enabled <Animator> component in order to perform animations");
        }
        if (!anim.runtimeAnimatorController)
        {
            Assert.Fail(" Level 2: There should be created controller, attached to <Animator> component");
        }
        aclips = anim.runtimeAnimatorController.animationClips;
    }

    [UnityTest, Order(2)]
    public IEnumerator CheckAnimationClips()
    {
        yield return null;
        if (aclips.Length != 3)
        {
            Assert.Fail(" Level 2: There should be added 3 clips to \"Player\"'s animator: Idle, Jump, Walk");
        }

        AnimationClip idle = Array.Find(aclips, clip => clip.name.Equals("Idle"));
        
        if (!idle) Assert.Fail(" Level 2: There should be a clip in \"Player\"'s animator, called \"Idle\"");
        if (idle.legacy) Assert.Fail(" Level 2: \"Idle\" clip should be animated by animator, not by the <Legacy Animation>" +
                                     " component, so it's legacy property should be unchecked");
        if (idle.empty) Assert.Fail(" Level 2: \"Idle\" clip in Player's animator should have animation keys");
        if (!idle.isLooping) Assert.Fail(" Level 2: \"Idle\" clip in Player's animator should be looped"); 
        
        AnimationClip jump = Array.Find(aclips, clip => clip.name.Equals("Jump"));
        
        if (!jump) Assert.Fail(" Level 2: There should be a clip in \"Player\"'s animator, called \"Jump\"");
        if (jump.legacy) Assert.Fail(" Level 2: \"Jump\" clip should be animated by animator, not by the <Legacy Animation>" +
                                     " component, so it's legacy property should be unchecked");
        if (jump.empty) Assert.Fail(" Level 2: \"Jump\" clip in Player's animator should have animation keys");
        if (!jump.isLooping) Assert.Fail(" Level 2: \"Jump\" clip in Player's animator should be looped"); 
        
        AnimationClip walk = Array.Find(aclips, clip => clip.name.Equals("Walk"));
        
        if (!walk) Assert.Fail(" Level 2: There should be a clip in \"Player\"'s animator, called \"Walk\"");
        if (walk.legacy) Assert.Fail(" Level 2: \"Walk\" clip should be animated by animator, not by the <Legacy Animation>" +
                                     " component, so it's legacy property should be unchecked");
        if (walk.empty) Assert.Fail(" Level 2: \"Walk\" clip in Player's animator should have animation keys");
        if (!walk.isLooping) Assert.Fail(" Level 2: \"Walk\" clip in Player's animator should be looped");
    }

    [UnityTest, Order(3)]
    public IEnumerator CheckTransitions()
    {
        Time.timeScale = 2;
        
        if (sr.flipX)
        {
            Assert.Fail(" Level 2: By default \"Player\"'s sprite should not be flipped");
        }
        
        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle" || (Time.unscaledTime - start) * Time.timeScale > 2);
        if ((Time.unscaledTime - start) * Time.timeScale >= 2)
        {
            Assert.Fail(" Level 2: While character is not moving - \"Idle\" clip should be played");
        }
        
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_D);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walk" || (Time.unscaledTime - start) * Time.timeScale > 2);
        if ((Time.unscaledTime - start) * Time.timeScale >= 2)
        {
            Assert.Fail(" Level 2: While character is moving on the ground to the right - \"Walk\" clip should be played");
        }

        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump" || (Time.unscaledTime - start) * Time.timeScale > 2);
        if ((Time.unscaledTime - start) * Time.timeScale >= 2)
        {
            Assert.Fail(" Level 2: While character is in air - \"Jump\" clip should be played");
        }

        yield return null;
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_D);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            playerCl.IsTouching(groundCl) || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail(" Level 2: In some time after the scene was loaded \"Player\"'s collider should be \"touching\" \"Ground\"'s collider" +
                        ". But after 5 seconds of game-time, that didn't happen");
        }

        IS.Keyboard.KeyDown(VirtualKeyCode.VK_A);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walk" || (Time.unscaledTime - start) * Time.timeScale > 2);
        if ((Time.unscaledTime - start) * Time.timeScale >= 2)
        {
            Assert.Fail(" Level 2: While character is moving on the ground to the left - \"Walk\" clip should be played");
        }
        
        if (!sr.flipX)
        {
            Assert.Fail(" Level 2: If last character's movement was performed to the left - it's sprite should be flipped");
        }
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_A);
        
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump" || (Time.unscaledTime - start) * Time.timeScale > 2);
        if ((Time.unscaledTime - start) * Time.timeScale >= 2)
        {
            Assert.Fail(" Level 2: While character is in air - \"Jump\" clip should be played");
        }
        
        if (!sr.flipX)
        {
            Assert.Fail(" Level 2: If last character's movement was performed to the left - it's sprite should be flipped");
        }

        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            playerCl.IsTouching(groundCl) || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail(" Level 2: In some time after the scene was loaded \"Player\"'s collider should be \"touching\" \"Ground\"'s collider" +
                        ". But after 5 seconds of game-time, that didn't happen");
        }
        
        if (!sr.flipX)
        {
            Assert.Fail(" Level 2: If last character's movement was performed to the left - it's sprite should be flipped");
        }
    }

    [UnityTest, Order(4)]
    public IEnumerator NecessaryGemComponents()
    {
        bool GemTagExist = false;
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals("Gem")) { GemTagExist = true; break; }
        }

        if (!GemTagExist)
        {
            Assert.Fail(" Level 2: \"Gem\" tag was not added to project");
        }
        
        Time.timeScale = 0;
        PMHelper.TurnCollisions(false);
        SceneManager.LoadScene("Level 2");
        yield return null;
        
        GameObject helperObj = new GameObject();
        StageHelper helper = helperObj.AddComponent<StageHelper>();
        yield return null;
        helper.RemovePlatforms();
        helper.RemoveEnemies();
        yield return null;

        levelEnd = PMHelper.Exist("LevelEnd");
        yield return null;
        levelEndCl = PMHelper.Exist<Collider2D>(levelEnd);
        levelEndCl.enabled = false;
        PMHelper.TurnCollisions(true);
        yield return null;

        player = GameObject.Find("Player");
        
        gem = GameObject.FindWithTag("Gem");
        yield return null;
        if (!gem)
        {
            Assert.Fail(" Level 2: Gems were not added to a scene or their tag is misspelled");
        }
        
        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
        yield return null;
        if (gems.Length != 3)
        {
            Assert.Fail(" Level 2: There should be three gems in scene");
        }

        gemSR = PMHelper.Exist<SpriteRenderer>(gem);
        if(!gemSR||!gemSR.enabled)
            Assert.Fail(" Level 2: There is no <SpriteRenderer> component on \"Gem\" or it is disabled");
        if(!gemSR.sprite)
            Assert.Fail(" Level 2: There should be sprite, attached to \"Gem\" objects' <SpriteRenderer>");

        GameObject background = PMHelper.Exist("Background");
        yield return null;
        SpriteRenderer backgroundSR = PMHelper.Exist<SpriteRenderer>(background);
        
        if (backgroundSR.sortingLayerID != gemSR.sortingLayerID)
        {
            Assert.Fail(" Level 2: There is no need here to create new sorting layers. " +
                        "Set all the <SpriteRenderer>s on the same sorting layer. To order visibility you should change their" +
                        "\"Order in layer\" property");
        }
        
        if (gemSR.sortingOrder <= backgroundSR.sortingOrder)
        {
            Assert.Fail(" Level 2: \"Gem\"'s order in layer should be greater than \"Background\"'s one");
        }

        gemCL = PMHelper.Exist<Collider2D>(gem);
        if (!gemCL || !gemCL.enabled)
        {
            Assert.Fail(" Level 2: \"Gem\" objects should have assigned enabled <Collider2D> component");
        }
        if (!gemCL.isTrigger)
        {
            Assert.Fail(" Level 2: \"Gem\" objects's <Collider2D> component should be triggerable");
        }

        gemCL.enabled = false;

        gemAnim = PMHelper.Exist<Animator>(gem);
        if (gemAnim == null)
        {
            Assert.Fail(" Level 2: There is no attached <Animator> component to gems");
        }
        yield return null;
        gemAclips = gemAnim.runtimeAnimatorController.animationClips;
    }

    [UnityTest, Order(5)]
    public IEnumerator CheckGemAnimationClips()
    {
        if (gemAclips.Length != 1)
        {
            Assert.Fail(" Level 2: There should be added 1 clip to Gem's animator, called \"Gem\"");
        }
        
        gemClip = Array.Find(gemAclips, clip => clip.name.Equals("Gem"));
        yield return null;
        
        if (gemClip == null) Assert.Fail(" Level 2: There should be a clip in Gem's animator, called \"Gem\"");
        if (gemClip.legacy) Assert.Fail(" Level 2: \"Gem\" clip should be animated by animator, not by the <Legacy Animation>" +
                                        " component, so it's legacy property should be unchecked");
        if (gemClip.empty) Assert.Fail(" Level 2: \"Gem\" clip in Gem's animator should have animation keys");
        if (!gemClip.isLooping) Assert.Fail(" Level 2: \"Gem\" clip in Gem's animator should be looped");
    }

    [UnityTest, Order(6)]
    public IEnumerator CheckTransitionsIdle()
    {
        Time.timeScale = 10;
        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            gemAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Gem" || (Time.unscaledTime - start) * Time.timeScale > 2);
        if ((Time.unscaledTime - start) * Time.timeScale >= 2)
        {
            Assert.Fail(" Level 2: \"Gem\" clip should be played by default");
        }
    }
    
    [UnityTest, Order(7)]
    public IEnumerator CheckDestroying()
    {
        gem.transform.position = (Vector2) levelEnd.transform.position + levelEndCl.offset;
        levelEndCl.enabled = true;
        yield return new WaitForSeconds(0.1f);
        if (!gem)
        {
            Assert.Fail(" Level 2: Gems should not be destroyed when colliding with anything except \"Player\"");
        }
        
        gem.transform.position = player.transform.position;
        gemCL.enabled = true;
        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            gem == null || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail(" Level 2: Gems should be destroyed when colliding with Player");
        }
    }
}
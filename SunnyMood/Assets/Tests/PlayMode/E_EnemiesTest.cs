using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class E_EnemiesTest
{
    private GameObject[] enemies;
    private GameObject player;

    private GameObject rat;
    private Animator ratAnim;
    private AnimationClip[] ratAclips;
    
    [UnityTest, Order(0)]
    public IEnumerator CheckSpawn()
    {
        bool EnemyTagExist = false;
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals("Enemy")) { EnemyTagExist = true; break; }
        }

        if (!EnemyTagExist)
        {
            Assert.Fail("Level 3: \"Enemy\" tag was not added to project");
        }
        
        Time.timeScale = 0;
        PMHelper.TurnCollisions(false);
        SceneManager.LoadScene("Level 3");
        
        yield return null;
        
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Assert.Fail("Level 3: Enemies were not added to a scene or their tag misspelled");
        }
    }
    
    [UnityTest, Order(1)]
    public IEnumerator NecessaryComponents()
    {
        GameObject background = PMHelper.Exist("Background");
        yield return null;
        SpriteRenderer backgroundSR = PMHelper.Exist<SpriteRenderer>(background);
        
        foreach (GameObject rat in enemies)
        {
            SpriteRenderer ratSR = PMHelper.Exist<SpriteRenderer>(rat);
            if(!ratSR||!ratSR.enabled)
                Assert.Fail("Level 3: There is no <SpriteRenderer> component on \"Enemy\"'s objects or it is disabled");
            if(!ratSR.sprite)
                Assert.Fail("Level 3: There should be sprite, attached to \"Enemy\"'s objects' <SpriteRenderer>");
            
            if (backgroundSR.sortingLayerID != ratSR.sortingLayerID)
            {
                Assert.Fail("Level 3: There is no need here to create new sorting layers. " +
                            "Set all the <SpriteRenderer>s on the same sorting layer. To order visibility you should change their" +
                            "\"Order in layer\" property");
            }
        
            if (ratSR.sortingOrder <= backgroundSR.sortingOrder)
            {
                Assert.Fail("Level 3: \"Enemy\"'s order in layer should be greater than \"Background\"'s one");
            }

            Collider2D ratCL = PMHelper.Exist<Collider2D>(rat);
            if (!ratCL || !ratCL.enabled)
            {
                Assert.Fail("Level 3: \"Enemy\" objects should have assigned enabled <Collider2D> component");
            }
            if (!ratCL.isTrigger)
            {
                Assert.Fail("Level 3: \"Enemy\" objects' <Collider2D> component should be triggerable");
            }

            ratAnim = PMHelper.Exist<Animator>(rat);
            if (!ratAnim)
            {
                Assert.Fail("Level 3: There is no attached <Animator> component to enemies");
            }

            if (!ratAnim.runtimeAnimatorController)
            {
                Assert.Fail("Level 3: There should be created controller, attached to enemies' <Animator> component!");
            }
            yield return null;
            ratAclips = ratAnim.runtimeAnimatorController.animationClips;
        }
        yield return null;
    }
    
    [UnityTest, Order(2)]
    public IEnumerator AnimationCheck()
    {
        
        if (ratAclips.Length != 1)
        {
            Assert.Fail("Level 3: There should be added 1 clip to enemies' animator: \"EnemyWalk\"");
        }

        AnimationClip walk = Array.Find(ratAclips, clip => clip.name.Equals("EnemyWalk"));
        yield return null;
        
        if (walk == null) Assert.Fail("Level 3: There should be a clip in enemies' animator, called \"EnemyWalk\"");
        if (walk.legacy) Assert.Fail("Level 3: \"EnemyWalk\" clip should be animated by animator, not by the <Legacy Animation>" +
                                     " component, so it's legacy property should be unchecked");
        if (walk.empty) Assert.Fail("Level 3: \"EnemyWalk\" clip in enemies' animator should have animation keys");
        if (!walk.isLooping) Assert.Fail("Level 3: \"EnemyWalk\" clip in enemies' animator should be looped");

        yield return null;
        if (ratAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "EnemyWalk")
        {
            Assert.Fail("Level 3: \"EnemyWalk\" clip should be played by default");
        }
    }
    
    [UnityTest, Order(3)]
    public IEnumerator CorrectPlacementAndMovement()
    {
        PMHelper.TurnCollisions(true);
        Time.timeScale = 5;
        foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Platform"))
        {
            pl.layer = LayerMask.NameToLayer("Test");
        }
        rat = GameObject.FindWithTag("Enemy");
        Collider2D ratCL = rat.GetComponent<Collider2D>();
        if (!PMHelper.RaycastFront2D(ratCL.bounds.center, Vector2.down,
            1 << LayerMask.NameToLayer("Test")).collider)
        {
            Assert.Fail("Level 3: Enemies should be spawned on platforms, right above them");
        }

        SpriteRenderer sr = rat.GetComponent<SpriteRenderer>();
        bool rotated = sr.flipX;
        
        Vector2 firstPos = rat.transform.position;
        yield return null;
        Vector2 secondPos = rat.transform.position;
        if (firstPos == secondPos)
        {
            Assert.Fail("Level 3: Enemies are not moving");
        }
        bool movingLeft = firstPos.x < secondPos.x;
        
        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            sr.flipX!=rotated || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail("Level 3: Enemies should change their movement direction and flip their sprite in less than 5 seconds");
        }
        firstPos = rat.transform.position;
        yield return null;
        secondPos = rat.transform.position;
        if (movingLeft == firstPos.x < secondPos.x)
        {
            Assert.Fail("Level 3: Enemies should change their movement direction and flip their sprite in less than 5 seconds");
        }
        yield return null;
    }
    
    [UnityTest, Order(4)]
    public IEnumerator CollisionCheck()
    {
        SceneManager.LoadScene("Level 3");
        yield return null;
        
        player = GameObject.Find("Player");
        rat = GameObject.FindWithTag("Enemy");
        GameObject gem = GameObject.FindWithTag("Gem");
        gem.transform.position = rat.transform.position;
        if (!gem)
        {
            Assert.Fail("Level 3: There should be no collisions with enemies, except the player one");
        }

        Scene cur = SceneManager.GetActiveScene();
        String name = cur.name;
        yield return null;
        player.transform.position = rat.transform.position;
        
        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("Level 3: When \"Player\" collides with an \"Enemy\" object - scene should reload");
        }

        if (SceneManager.GetActiveScene().name != name)
        {
            Assert.Fail("Level 3: When \"Player\" collides with an \"Enemy\" object - scene should reload");
        }
    }
}
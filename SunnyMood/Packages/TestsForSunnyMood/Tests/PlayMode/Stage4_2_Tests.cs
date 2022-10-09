using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[Description(""), Category("4")]
public class Stage4_2_Tests
{
    private GameObject player, ground, levelEnd;
    private Transform playerT;
    private Collider2D playerCl, groundCl, levelEndCl;
    private Collider2D leftBoundCl, rightBoundCl;
    private Rigidbody2D playerRB;
    private Vector3 jumpPlace;
    private Vector2 leftBoundPoint;
    private Vector2 rightBoundPoint;

    [UnityTest, Order(0)]
    public IEnumerator NotMovingWithoutInputCheck()
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

        levelEnd = GameObject.Find("LevelEnd");
        levelEndCl = PMHelper.Exist<Collider2D>(levelEnd);
        levelEndCl.enabled = false;
        PMHelper.TurnCollisions(true);

        player = GameObject.Find("Player");
        yield return null;
        playerT = player.transform;
        playerCl = PMHelper.Exist<Collider2D>(player);
        yield return null;

        ground = GameObject.Find("Ground");
        yield return null;
        groundCl = PMHelper.Exist<Collider2D>(ground);
        yield return null;

        Time.timeScale = 10;

        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            playerCl.IsTouching(groundCl) || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail(
                "Level 2: In some time after the scene was loaded \"Player\"'s collider should be \"touching\" \"Ground\"'s collider" +
                ". But after 5 seconds of game-time, that didn't happen");
        }

        yield return new WaitForSeconds(1);
        Vector3 startPos = playerT.position;
        yield return null;
        if (startPos != playerT.position)
        {
            Assert.Fail("Level 2: \"Player\"'s position should not change if there were no input provided");
        }
    }

    [UnityTest, Order(1)]
    public IEnumerator MovementLeftCheck()
    {
        Vector3 posStart = playerT.position;
        VInput.KeyDown(KeyCode.A);
        yield return null;
        VInput.KeyUp(KeyCode.A);
        yield return null;
        Vector3 posEnd = player.transform.position;
        if (posEnd.x >= posStart.x)
        {
            Assert.Fail("Level 2: When the A-key is pressed X-axis of \"Player\"'s object should decrease");
        }

        if (posEnd.y != posStart.y || posEnd.z != posStart.z)
        {
            Assert.Fail("Level 2: When the A-key is pressed y-axis and z-axis should not change");
        }

        yield return null;
    }

    [UnityTest, Order(2)]
    public IEnumerator MovementRightCheck()
    {
        Vector3 posStart = playerT.position;
        VInput.KeyDown(KeyCode.D);
        yield return null;
        VInput.KeyUp(KeyCode.D);
        yield return null;
        Vector3 posEnd = player.transform.position;
        if (posEnd.x <= posStart.x)
        {
            Assert.Fail("Level 2: When the D-key is pressed X-axis of \"Player\"'s object should increase");
        }

        if (posEnd.y != posStart.y || posEnd.z != posStart.z)
        {
            Assert.Fail("Level 2: When the D-key is pressed y-axis and z-axis should not change");
        }

        yield return null;
    }

    [UnityTest, Order(3)]
    public IEnumerator CorrectMovement()
    {
        playerRB = PMHelper.Exist<Rigidbody2D>(player);
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        Vector3 posStart = playerT.position;
        VInput.KeyDown(KeyCode.D);
        yield return null;
        VInput.KeyUp(KeyCode.D);
        yield return null;
        Vector3 posEnd = player.transform.position;
        if (posEnd.x != posStart.x)
        {
            Assert.Fail("Level 2: \"Player\"'s movement should be implemented via <Rigidbody2D> component");
        }

        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        VInput.KeyDown(KeyCode.D);
        yield return null;
        yield return null;
        if (playerRB.velocity.x == 0)
        {
            Assert.Fail("Level 2: \"Player\"'s horizontal movement should be implemented by changing x-axis velocity");
        }

        VInput.KeyUp(KeyCode.D);
        yield return null;
    }

    [UnityTest, Order(4)]
    public IEnumerator JumpCheck()
    {
        VInput.KeyPress(KeyCode.Space);
        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            playerRB.velocity.y > 0 || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail("Level 2: When the Space-button was pressed jump should be provided, so the Rigidbody2D's " +
                        "y-axis velocity should increase");
        }

        start = Time.unscaledTime;
        float startOfJump = start;
        yield return new WaitUntil(() =>
            playerRB.velocity.y < 0 || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail(
                "Level 2: Y-axis velocity of <Rigidbody2D> component should decrease after the jump was provided," +
                " because of the gravity, and after some time it should become negative");
        }

        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            playerCl.IsTouching(groundCl) || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail("Level 2: After the jump is provided, player should fall down to the ground. " +
                        "Jump duration should be less than 2 seconds");
        }

        float duration = (Time.unscaledTime - startOfJump) * Time.timeScale;
        if (duration >= 2)
        {
            Assert.Fail("Level 2: Jump duration should be less than 2 seconds, but in your case it's " + duration);
        }
    }

    [UnityTest, Order(5)]
    public IEnumerator JumpInAirCheck()
    {
        Time.timeScale = 1;

        VInput.KeyPress(KeyCode.Space);
        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            playerRB.velocity.y < 0 || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail(
                "Level 2: Y-axis velocity of <Rigidbody2D> component should decrease after the jump was provided," +
                " because of the gravity, and after some time it should become negative");
        }

        jumpPlace = playerT.position;

        float velocityPrev = playerRB.velocity.y;
        VInput.KeyPress(KeyCode.Space);
        yield return null;
        yield return null;
        if (playerRB.velocity.y > velocityPrev)
        {
            Assert.Fail("Level 2: \"Player\" should not be able to jump while it's in midair");
        }

        yield return new WaitUntil(() =>
            playerCl.IsTouching(groundCl) || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail("Level 2: After the jump is provided, player should fall down to the ground. " +
                        "Jump duration should be less than 2 seconds");
        }
    }

    [UnityTest, Order(6)]
    public IEnumerator CheckBounds()
    {
        bool BoundsTagExist = false;
        SerializedObject tagManager =
            new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals("Bounds"))
            {
                BoundsTagExist = true;
                break;
            }
        }

        if (!BoundsTagExist)
        {
            Assert.Fail("Level 2: \"Bounds\" tag was not added to project");
        }

        yield return null;
        GameObject[] bounds = GameObject.FindGameObjectsWithTag("Bounds");
        if (bounds.Length != 2)
        {
            Assert.Fail("Level 2: There should be 2 game objects with \"Bounds\" tag on scene");
        }

        foreach (GameObject b in bounds)
        {
            SpriteRenderer boundsSR = PMHelper.Exist<SpriteRenderer>(b);
            if (boundsSR)
                Assert.Fail(
                    "Level 2: There should be no <SpriteRenderer> component on \"Bounds\" objects in order to make them non-visible");
            Collider2D boundsCL = PMHelper.Exist<Collider2D>(b);
            if (!boundsCL || !boundsCL)
            {
                Assert.Fail("Level 2: \"Bounds\" objects should have an assigned enabled <Collider2D> component");
            }

            if (boundsCL.isTrigger)
            {
                Assert.Fail("Level 2: \"Bounds\"' <Collider2D> component should not be triggerable");
            }

            if (!boundsCL.sharedMaterial || boundsCL.sharedMaterial.friction != 0)
            {
                Assert.Fail(
                    "Level 2: \"Bounds\"' <Collider2D> component should have assigned <2D Physics Material> with " +
                    "friction set to zero, so the player won't be able to hang on the boundaries");
            }

            b.layer = LayerMask.NameToLayer("Test");
        }

        leftBoundPoint = PMHelper.RaycastFront2D(playerT.position, Vector2.left,
            1 << LayerMask.NameToLayer("Test")).point;
        rightBoundPoint = PMHelper.RaycastFront2D(playerT.position, Vector2.right,
            1 << LayerMask.NameToLayer("Test")).point;

        leftBoundCl = PMHelper.RaycastFront2D(playerT.position, Vector2.left,
            1 << LayerMask.NameToLayer("Test")).collider;
        rightBoundCl = PMHelper.RaycastFront2D(playerT.position, Vector2.right,
            1 << LayerMask.NameToLayer("Test")).collider;


        if (!leftBoundCl)
        {
            Assert.Fail("Level 2: There should be an object with \"Bounds\" tag on \"Player\"'s left and stop it from" +
                        " passing through (\"Player\" should not be able to jump it over, or crawl underneath it)");
        }

        if (!rightBoundCl)
        {
            Assert.Fail(
                "Level 2: There should be an object with \"Bounds\" tag on \"Player\"'s right and stop it from" +
                " passing through (\"Player\" should not be able to jump it over, or crawl underneath it)");
        }

        if (!PMHelper.RaycastFront2D(jumpPlace, Vector2.left,
            1 << LayerMask.NameToLayer("Test")).collider)
        {
            Assert.Fail("Level 2: There should be an object with \"Bounds\" tag on \"Player\"'s left and stop it from" +
                        " passing through (\"Player\" should not be able to jump it over, or crawl underneath it)");
        }

        if (!PMHelper.RaycastFront2D(jumpPlace, Vector2.right,
            1 << LayerMask.NameToLayer("Test")).collider)
        {
            Assert.Fail(
                "Level 2: There should be an object with \"Bounds\" tag on \"Player\"'s right and stop it from" +
                " passing through (\"Player\" should not be able to jump it over, or crawl underneath it)");
        }


        foreach (GameObject b in bounds)
        {
            b.layer = LayerMask.NameToLayer("Default");
        }

        ground.layer = LayerMask.NameToLayer("Test");

        if (!PMHelper.RaycastFront2D(leftBoundPoint, Vector2.down,
            1 << LayerMask.NameToLayer("Test")).collider)
        {
            Assert.Fail(
                "Level 2: Left boundary should be placed above or 'touching' the ground object, so \"Player\" won't" +
                " be able to fall down to the void");
        }

        if (!PMHelper.RaycastFront2D(rightBoundPoint, Vector2.down,
            1 << LayerMask.NameToLayer("Test")).collider)
        {
            Assert.Fail(
                "Level 2: Right boundary should be placed above or 'touching' the ground object, so \"Player\" won't" +
                " be able to fall down to the void");
        }
    }

    [UnityTest, Order(7)]
    public IEnumerator CameraMovementCheck()
    {
        GameObject cameraObj = GameObject.Find("Main Camera");
        Camera camera = PMHelper.Exist<Camera>(cameraObj);
        yield return null;

        Time.timeScale = 20;
        float start = Time.unscaledTime;
        VInput.KeyDown(KeyCode.A);
        yield return new WaitUntil(() =>
            playerCl.IsTouching(leftBoundCl) || (Time.unscaledTime - start) * Time.timeScale > 10);
        if ((Time.unscaledTime - start) * Time.timeScale >= 10)
        {
            Assert.Fail(
                "Level 2: Player should be able to get from left bound to the right one in less than 10 game-time seconds");
        }

        VInput.KeyUp(KeyCode.A);
        yield return null;

        if (!PMHelper.CheckVisibility(camera, playerT, 2))
        {
            Assert.Fail("Level 2: Player should always stay in a camera view");
        }

        start = Time.unscaledTime;
        VInput.KeyDown(KeyCode.D);
        yield return new WaitUntil(() =>
            playerCl.IsTouching(rightBoundCl) || (Time.unscaledTime - start) * Time.timeScale > 10);
        if ((Time.unscaledTime - start) * Time.timeScale >= 10)
        {
            Assert.Fail(
                "Level 2: Player should be able to get from left bound to the right one in less than 10 game-time seconds");
        }

        VInput.KeyUp(KeyCode.D);
        yield return null;

        if (!PMHelper.CheckVisibility(camera, playerT, 2))
        {
            Assert.Fail("Level 2: Player should always stay in a camera view");
        }
    }

    [UnityTest, Order(8)]
    public IEnumerator LevelEndPositionCheck()
    {
        Vector2 LevelEndPlace = (Vector2) levelEnd.transform.position + levelEndCl.offset;
        yield return null;
        if (!PMHelper.RaycastFront2D(LevelEndPlace, Vector2.down,
            1 << LayerMask.NameToLayer("Test")).collider)
        {
            Assert.Fail(
                "Level 2: \"LevelEnd\" should be placed above or 'touching' the ground object, so \"Player\" could reach it");
        }

        if (LevelEndPlace.x <= leftBoundPoint.x || LevelEndPlace.x >= rightBoundPoint.x)
        {
            Assert.Fail("Level 2: \"LevelEnd\" should be placed between boundaries, so \"Player\" could reach it");
        }

        if (LevelEndPlace.y >= jumpPlace.y)
        {
            Assert.Fail("Level 2: \"LevelEnd\" should not be placed too high, so \"Player\" could reach it");
        }
    }

    [UnityTest, Order(9)]
    public IEnumerator LevelEndActionCheck()
    {
        Scene cur = SceneManager.GetActiveScene();
        yield return null;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        playerT.position = (Vector2) levelEnd.transform.position + levelEndCl.offset;

        levelEndCl.enabled = true;
        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 5);
        if ((Time.unscaledTime - start) * Time.timeScale >= 5)
        {
            Assert.Fail(
                "Level 2: When \"Player\" collides with a \"LevelEnd\" object - scene should change (or just reload).");
        }
    }
}
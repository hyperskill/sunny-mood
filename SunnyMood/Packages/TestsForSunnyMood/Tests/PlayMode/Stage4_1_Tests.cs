using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

[Description(""), Category("4")]
public class Stage4_1_Tests
{
    private GameObject background,
        player,
        grid,
        ground,
        cameraObj,
        levelend;

    private bool exist;
    private SpriteRenderer playerSR, backgroundSR, levelEndSR;
    private TilemapRenderer groundTMR;

    [UnityTest, Order(1)]
    public IEnumerator ObjectsExistCheck()
    {
        int layer = LayerMask.NameToLayer("Test");
        if (layer < 0)
        {
            Assert.Fail("Level 2: Please, do not remove \"Test\" layer, it's existance necessary for tests");
        }

        Time.timeScale = 0;

        if (!Application.CanStreamedLevelBeLoaded("Level 2"))
        {
            Assert.Fail("Level 2: \"Level 2\" scene is misspelled or was not added to build settings");
        }

        yield return null;

        SceneManager.LoadScene("Level 2");
        yield return null;

        (background, exist) = PMHelper.Exist("Background");
        if (!exist)
            Assert.Fail("Level 2: There should be a background, named \"Background\" on scene");

        (player, exist) = PMHelper.Exist("Player");
        if (!exist)
            Assert.Fail("Level 2: There should be a player, named \"Player\" on scene");

        (levelend, exist) = PMHelper.Exist("LevelEnd");
        if (!levelend)
            Assert.Fail("Level 2: There should be a level end, named \"LevelEnd\" on scene");
    }

    [UnityTest, Order(2)]
    public IEnumerator BasicObjectComponentsCheck()
    {
        backgroundSR = PMHelper.Exist<SpriteRenderer>(background);
        if (!backgroundSR || !backgroundSR.enabled)
            Assert.Fail("Level 2: There is no <SpriteRenderer> component on \"Background\" object or it is disabled");
        if (!backgroundSR.sprite)
            Assert.Fail("Level 2: There should be sprite, attached to \"Background\"'s <SpriteRenderer>");

        playerSR = PMHelper.Exist<SpriteRenderer>(player);
        if (!playerSR || !playerSR.enabled)
            Assert.Fail("Level 2: There is no <SpriteRenderer> component on \"Player\" object or it is disabled");
        if (!playerSR.sprite)
            Assert.Fail("Level 2: There should be sprite, attached to \"Player\"'s <SpriteRenderer>");

        Collider2D playerCL = PMHelper.Exist<Collider2D>(player);
        if (!playerCL || !playerCL.enabled)
        {
            Assert.Fail("Level 2: Player should have assigned enabled <Collider2D> component");
        }

        if (playerCL.isTrigger)
        {
            Assert.Fail("Level 2: Player's <Collider2D> component should not be triggerable");
        }

        Rigidbody2D playerRb = PMHelper.Exist<Rigidbody2D>(player);
        if (!playerRb)
        {
            Assert.Fail("Level 2: Player should have assigned <Rigidbody2D> component");
        }

        if (playerRb.bodyType != RigidbodyType2D.Dynamic)
        {
            Assert.Fail("Level 2: Player's <Rigidbody2D> component should be Dynamic");
        }

        if (!playerRb.simulated)
        {
            Assert.Fail("Level 2: Player's <Rigidbody2D> component should be simulated");
        }

        if (playerRb.gravityScale <= 0)
        {
            Assert.Fail("Level 2: Player's <Rigidbody2D> component should be affected by gravity, " +
                        "so it's Gravity Scale parameter should not be less or equal to 0");
        }

        if (playerRb.interpolation != RigidbodyInterpolation2D.None)
        {
            Assert.Fail("Level 2: Do not change interpolation of Player's <Rigidbody2D> component. Set it as None");
        }

        if (playerRb.constraints != RigidbodyConstraints2D.FreezeRotation)
        {
            Assert.Fail(
                "Level 2: Player's <Rigidbody2D> component's constraints should be freezed by rotation and unfreezed by position");
        }

        levelEndSR = PMHelper.Exist<SpriteRenderer>(levelend);
        if (!levelEndSR || !levelEndSR.enabled)
            Assert.Fail("Level 2: There is no <SpriteRenderer> component on \"LevelEnd\" object or it is disabled");
        if (!levelEndSR.sprite)
            Assert.Fail("Level 2: There should be sprite, attached to \"LevelEnd\"'s <SpriteRenderer>");

        Collider2D levelEndCL = PMHelper.Exist<Collider2D>(levelend);
        if (!levelEndCL || !levelEndCL.enabled)
        {
            Assert.Fail("Level 2: \"LevelEnd\" object should have an assigned enabled <Collider2D> component");
        }

        if (!levelEndCL.isTrigger)
        {
            Assert.Fail("Level 2: \"LevelEnd\"'s <Collider2D> component should be triggerable");
        }

        yield return null;
    }

    [UnityTest, Order(3)]
    public IEnumerator GridCheck()
    {
        yield return null;
        (grid, exist) = PMHelper.Exist("Grid");
        if (!exist)
        {
            Assert.Fail("Level 2: There should be a tilemap grid, named \"Grid\" on scene");
        }

        (ground, exist) = PMHelper.Exist("Ground");
        if (!exist)
        {
            Assert.Fail("Level 2: There should be a ground tilemap, named \"Ground\" on scene");
        }
    }

    [UnityTest, Order(4)]
    public IEnumerator BasicGridComponentsCheck()
    {
        yield return null;
        Grid gridGR = PMHelper.Exist<Grid>(grid);
        if (!gridGR)
            Assert.Fail("Level 2: There should be a <Grid> component on \"Grid\"'s object to use tilemaps");
        if (gridGR.cellLayout != GridLayout.CellLayout.Rectangle)
            Assert.Fail("Level 2: \"Grid\"'s <Grid> component should have Rectangle layout");
        if (gridGR.cellSwizzle != GridLayout.CellSwizzle.XYZ)
            Assert.Fail("Level 2: \"Grid\"'s <Grid> component should have XYZ swizzle");

        Tilemap groundTM = PMHelper.Exist<Tilemap>(ground);
        groundTMR = PMHelper.Exist<TilemapRenderer>(ground);
        if (!groundTM)
            Assert.Fail("Level 2: There should be a <Tilemap> component on \"Ground\"'s object to use tilemaps");
        if (!groundTMR)
            Assert.Fail(
                "Level 2: There should be a <TilemapRenderer> component on \"Ground\"'s object to view created tilemaps");
        if (groundTM.size.x <= 0 || groundTM.size.y <= 0)
            Assert.Fail("Level 2: There are no added tiles to \"Ground\"'s tilemap");

        Collider2D groundCL = PMHelper.Exist<Collider2D>(ground);
        if (!groundCL || !groundCL.enabled)
        {
            Assert.Fail("Level 2: \"Ground\" object should have an assigned enabled <Collider2D> component");
        }

        if (groundCL.isTrigger)
        {
            Assert.Fail("Level 2: \"Ground\"'s <Collider2D> component should not be triggerable");
        }
    }

    [UnityTest, Order(5)]
    public IEnumerator CameraCheck()
    {
        yield return null;
        (cameraObj, exist) = PMHelper.Exist("Main Camera");
        if (!exist)
            Assert.Fail("Level 2: There should be a camera, named \"Main Camera\" on scene");

        Camera camera = PMHelper.Exist<Camera>(cameraObj);
        if (!camera)
            Assert.Fail("Level 2: \"Main Camera\"'s object should have an attached <Camera> component");

        if (!PMHelper.CheckVisibility(camera, player.transform, 2))
            Assert.Fail("Level 2: Player's object should be in a camera view");
        if (!PMHelper.CheckVisibility(camera, background.transform, 2))
            Assert.Fail("Level 2: Background's object should be in a camera view");
    }

    [UnityTest, Order(6)]
    public IEnumerator ChildrenCheck()
    {
        yield return null;
        if (!PMHelper.Child(background, cameraObj))
            Assert.Fail(
                "Level 2: \"Background\"'s object should be a child of \"Main Camera\" object in order to move" +
                " camera with background, so that the background will be always in the camera view");
        if (!PMHelper.Child(ground, grid))
            Assert.Fail(
                "Level 2: \"Ground\"'s object should be a child of \"Grid\" object, because tilemap's should be a child of grid!");
    }

    [UnityTest, Order(7)]
    public IEnumerator SortingCheck()
    {
        yield return null;
        int layer = playerSR.sortingLayerID;
        if (backgroundSR.sortingLayerID != layer || levelEndSR.sortingLayerID != layer ||
            groundTMR.sortingLayerID != layer)
        {
            Assert.Fail("Level 2: There is no need here to create new sorting layers. " +
                        "Set all the <SpriteRenderer>s on the same sorting layer. To order visibility you should change their" +
                        "\"Order in layer\" property");
        }

        bool correctSort =
            backgroundSR.sortingOrder < levelEndSR.sortingOrder &&
            levelEndSR.sortingOrder < groundTMR.sortingOrder &&
            groundTMR.sortingOrder < playerSR.sortingOrder;
        if (!correctSort)
            Assert.Fail(
                "Level 2: Order in layers should be placed in correct order: Background < LevelEnd < Ground < Player!");
    }

    [UnityTest, Order(8)]
    public IEnumerator CheckPositions()
    {
        ground.layer = LayerMask.NameToLayer("Test");
        yield return null;
        if (!PMHelper.RaycastFront2D(player.transform.position, Vector2.down,
            1 << LayerMask.NameToLayer("Test")).collider)
        {
            Assert.Fail("Level 2: Player should be placed above the ground");
        }
    }
}
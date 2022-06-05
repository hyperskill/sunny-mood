using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using WindowsInput;
using WindowsInput.Native;

public class F_MenuTest
{
    InputSimulator IS = new InputSimulator();
    private GameObject canvas, chooselevel, play, exit, panel, close, level1, level2, level3;
    private Button chooseLevelButton, playButton, exitButton, closeButton, level1Button, level2Button, level3Button;

    [UnityTest, Order(0)]
    public IEnumerator SetUp()
    {
        yield return null;
        PlayerPrefs.DeleteAll();
        yield return null;

        if (!Application.CanStreamedLevelBeLoaded("Main Menu"))
        {
            Assert.Fail("\"Main Menu\" scene is misspelled or was not added to build settings");
        }

        yield return null;

        SceneManager.LoadScene("Main Menu");
        yield return null;
    }
    
    [UnityTest, Order(1)]
    public IEnumerator ChooseLevelNecessary()
    {
        canvas = PMHelper.Exist("Canvas");
        yield return null;
        if (!canvas)
            Assert.Fail("There should be canvas on scene named \"Canvas\"");
        
        RectTransform canvasRT = PMHelper.Exist<RectTransform>(canvas);
        Canvas canvasCV = PMHelper.Exist<Canvas>(canvas);
        CanvasScaler canvasCS = PMHelper.Exist<CanvasScaler>(canvas);
        GraphicRaycaster canvasGR = PMHelper.Exist<GraphicRaycaster>(canvas);
        yield return null;
        
        if (!canvasRT)
            Assert.Fail("There should be <RectTransform> component on \"Canvas\" object");
        if (!canvasCV || !canvasCV.enabled)
            Assert.Fail("There should be enabled <Canvas> component on \"Canvas\" object");
        if (!canvasCS || !canvasCS.enabled)
            Assert.Fail("There should be enabled <CanvasScaler> component on \"Canvas\" object");
        if (!canvasGR || !canvasGR.enabled)
            Assert.Fail("There should be enabled <GraphicRaycaster> component on \"Canvas\" object");
        
        chooselevel = PMHelper.Exist("ChooseLevel");
        yield return null;
        
        if (!chooselevel)
            Assert.Fail("There should be button on scene named \"ChooseLevel\"");

        RectTransform chooseLevelRT = PMHelper.Exist<RectTransform>(chooselevel);
        chooseLevelButton = PMHelper.Exist<Button>(chooselevel);
        yield return null;
        
        if (!chooseLevelRT)
            Assert.Fail("There should be <RectTransform> component on \"ChooseLevel\" object");
        if (!chooseLevelButton || !chooseLevelButton.enabled)
            Assert.Fail("There should be <Button> component on \"ChooseLevel\" object");
        if (!PMHelper.CheckRectTransform(chooseLevelRT))
        {
            Assert.Fail("Anchors of \"ChooseLevel\"'s <RectTransform> component are incorrect or it's offsets " +
                        "are not equal to zero, might be troubles with different resolutions");
        }
        
        
        play = PMHelper.Exist("Play");
        yield return null;
        
        if (!play)
            Assert.Fail("There should be button on scene named \"Play\"");

        RectTransform playRT = PMHelper.Exist<RectTransform>(play);
        playButton = PMHelper.Exist<Button>(play);
        yield return null;
        
        if (!playRT)
            Assert.Fail("There should be <RectTransform> component on \"Play\" object");
        if (!playButton || !playButton.enabled)
            Assert.Fail("There should be <Button> component on \"Play\" object");
        if (!PMHelper.CheckRectTransform(playRT))
        {
            Assert.Fail("Anchors of \"Play\"'s <RectTransform> component are incorrect or it's offsets " +
                        "are not equal to zero, might be troubles with different resolutions");
        }
        
        
        exit = PMHelper.Exist("Exit");
        yield return null;
        
        if (!exit)
            Assert.Fail("There should be button on scene named \"Exit\"");

        RectTransform exitRT = PMHelper.Exist<RectTransform>(exit);
        exitButton = PMHelper.Exist<Button>(exit);
        yield return null;
        
        if (!exitRT)
            Assert.Fail("There should be <RectTransform> component on \"Exit\" object");
        if (!exitButton || !exitButton.enabled)
            Assert.Fail("There should be <Button> component on \"Exit\" object");
        if (!PMHelper.CheckRectTransform(exitRT))
        {
            Assert.Fail("Anchors of \"Exit\"'s <RectTransform> component are incorrect or it's offsets " +
                        "are not equal to zero, might be troubles with different resolutions");
        }
        
        panel = PMHelper.Exist("Panel");
        yield return null;
        
        if (panel)
            Assert.Fail("There should be no \"Panel\" object on scene by default");
        
        chooseLevelButton.onClick.Invoke();
        yield return null;
        
        panel = PMHelper.Exist("Panel");
        yield return null;
        
        if (!panel)
            Assert.Fail("There is no \"Panel\" object after pressing \"ChooseLevel\" button");

        RectTransform panelRT = PMHelper.Exist<RectTransform>(panel);
        yield return null;
        
        if (!panelRT)
            Assert.Fail("There should be <RectTransform> component on \"Panel\" object");
        if (!PMHelper.CheckRectTransform(panelRT))
        {
            Assert.Fail("Anchors of \"Panel\"'s <RectTransform> component are incorrect or it's offsets " +
                        "are not equal to zero, might be troubles with different resolutions");
        }
        
        close = PMHelper.Exist("Close");
        yield return null;
        
        if (!close)
            Assert.Fail("There should be button on scene after pressing \"ChooseLevel\" button, named \"Close\"");

        RectTransform closeRT = PMHelper.Exist<RectTransform>(close);
        closeButton = PMHelper.Exist<Button>(close);
        yield return null;
        
        if (!closeRT)
            Assert.Fail("There should be <RectTransform> component on \"Close\" object");
        if (!closeButton || !closeButton.enabled)
            Assert.Fail("There should be <Button> component on \"Close\" object");
        if (!PMHelper.CheckRectTransform(closeRT))
        {
            Assert.Fail("Anchors of \"Close\"'s <RectTransform> component are incorrect or it's offsets " +
                        "are not equal to zero, might be troubles with different resolutions");
        }
        
        
        level1 = PMHelper.Exist("Level 1");
        yield return null;
        
        if (!level1)
            Assert.Fail("There should be button on scene after pressing \"ChooseLevel\" button, named \"Level 1\"");

        RectTransform level1RT = PMHelper.Exist<RectTransform>(level1);
        level1Button = PMHelper.Exist<Button>(level1);
        yield return null;
        
        if (!level1RT)
            Assert.Fail("There should be <RectTransform> component on \"Level 1\" object");
        if (!level1Button || !level1Button.enabled)
            Assert.Fail("There should be <Button> component on \"Level 1\" object");
        if (!PMHelper.CheckRectTransform(level1RT))
        {
            Assert.Fail("Anchors of \"Level 1\"'s <RectTransform> component are incorrect or it's offsets " +
                        "are not equal to zero, might be troubles with different resolutions");
        }
        
        
        level2 = PMHelper.Exist("Level 2");
        yield return null;
        
        if (!level2)
            Assert.Fail("There should be button on scene after pressing \"ChooseLevel\" button, named \"Level 2\"");

        RectTransform level2RT = PMHelper.Exist<RectTransform>(level2);
        level2Button = PMHelper.Exist<Button>(level2);
        yield return null;
        
        if (!level2RT)
            Assert.Fail("There should be <RectTransform> component on \"Level 2\" object");
        if (!level2Button || !level2Button.enabled)
            Assert.Fail("There should be <Button> component on \"Level 2\" object");
        if (!PMHelper.CheckRectTransform(level2RT))
        {
            Assert.Fail("Anchors of \"Level 2\"'s <RectTransform> component are incorrect or it's offsets " +
                        "are not equal to zero, might be troubles with different resolutions");
        }
        
        
        level3 = PMHelper.Exist("Level 3");
        yield return null;
        
        if (!level3)
            Assert.Fail("There should be button on scene after pressing \"ChooseLevel\" button, named \"Level 3\"");

        RectTransform level3RT = PMHelper.Exist<RectTransform>(level3);
        level3Button = PMHelper.Exist<Button>(level3);
        yield return null;
        
        if (!level3RT)
            Assert.Fail("There should be <RectTransform> component on \"Level 3\" object");
        if (!level3Button || !level3Button.enabled)
            Assert.Fail("There should be <Button> component on \"Level 3\" object");
        if (!PMHelper.CheckRectTransform(level3RT))
        {
            Assert.Fail("Anchors of \"Level 3\"'s <RectTransform> component are incorrect or it's offsets " +
                        "are not equal to zero, might be troubles with different resolutions");
        }
    }

    [UnityTest, Order(2)]
    public IEnumerator CheckChildren()
    {
        yield return null;
        if (!PMHelper.Child(close, panel)) Assert.Fail("\"Close\" object should be a child of \"Panel\" object");
        if (!PMHelper.Child(level1, panel)) Assert.Fail("\"Level 1\" object should be a child of \"Panel\" object");
        if (!PMHelper.Child(level2, panel)) Assert.Fail("\"Level 2\" object should be a child of \"Panel\" object");
        if (!PMHelper.Child(level3, panel)) Assert.Fail("\"Level 3\" object should be a child of \"Panel\" object");
        if (!PMHelper.Child(panel, canvas)) Assert.Fail("\"Panel\" object should be a child of \"Canvas\" object");
        if (!PMHelper.Child(play, canvas)) Assert.Fail("\"Play\" object should be a child of \"Canvas\" object");
        if (!PMHelper.Child(exit, canvas)) Assert.Fail("\"Exit\" object should be a child of \"Canvas\" object");
        if (!PMHelper.Child(chooselevel, canvas)) Assert.Fail("\"Choose Level\" object should be a child of \"Canvas\" object");
    }

    [UnityTest, Order(3)]
    public IEnumerator PlayTest()
    {
        Time.timeScale = 3;
        Scene cur = SceneManager.GetActiveScene();
        
        yield return null;
        bool correctButtonLevels = level1Button.interactable &&
                                   !level2Button.interactable &&
                                   !level3Button.interactable;
        if (!correctButtonLevels)
        {
            Assert.Fail("When the game was started by the first time - player should be able to choose only first level," +
                        " others should not be interactable");
        }
        
        closeButton.onClick.Invoke();
        yield return null;
        
        panel = PMHelper.Exist("Panel");
        if (panel) 
            Assert.Fail("After pressing \"Close\" button - \"Panel\" object should become non active");
        
        playButton.onClick.Invoke();
        yield return null;

        float start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("When player started a game first time - \"Play\" button should transfer him to \"Level 1\"");
        }

        cur = SceneManager.GetActiveScene();
        yield return null;
        
        if (!cur.name.Equals("Level 1"))
        {
            Assert.Fail("When player started a game first time - \"Play\" button should transfer him to \"Level 1\"");
        }
        
        IS.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;
        
        if (!cur.name.Equals("Main Menu"))
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }
        
        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        level1 = PMHelper.Exist("Level 1");
        level2 = PMHelper.Exist("Level 2");
        level3 = PMHelper.Exist("Level 3");
        if (!level1 || !level2 || !level3)
        {
            Assert.Fail("To disable buttons there is no need to deactivate it's object, you should just uncheck " +
                        "it's <Button>'s interactable property");
        }
        level1Button = PMHelper.Exist<Button>(level1);
        level2Button = PMHelper.Exist<Button>(level2);
        level3Button = PMHelper.Exist<Button>(level3);
        if (!level1Button || !level2Button || !level3Button)
        {
            Assert.Fail("To disable buttons there is no need to disable it's <Button> component, you should just uncheck " +
                        "<Button>'s interactable property");
        }
        
        correctButtonLevels = level1Button.interactable &&
                              !level2Button.interactable &&
                              !level3Button.interactable;
        if (!correctButtonLevels)
        {
            Assert.Fail("When player still had not passed first level - he should not be able to " +
                        "play any other levels, chosen from \"Main Menu\"'s \"ChooseLevel\" panel, so others" +
                        "should be non-interactable");
        }
        
        GameObject.Find("Close").GetComponent<Button>().onClick.Invoke();
        yield return null;
        
        GameObject.Find("Play").GetComponent<Button>().onClick.Invoke();
        
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("When player left level without having it passed," +
                        " \"Play\" button should transfer him to same level");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;
        
        if (!cur.name.Equals("Level 1"))
        {
            Assert.Fail("When player left level without having it passed," +
                        " \"Play\" button should transfer him to same level");
        }

        GameObject.Find("Player").transform.position =
            GameObject.Find("LevelEnd").GetComponent<Collider2D>().bounds.center;

        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("After passing first level, second one should be loaded");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;
        
        if (!cur.name.Equals("Level 2"))
        {
            Assert.Fail("After passing first level, second one should be loaded");
        }
        
        IS.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;
        
        if (!cur.name.Equals("Main Menu"))
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }
        
        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        level1 = PMHelper.Exist("Level 1");
        level2 = PMHelper.Exist("Level 2");
        level3 = PMHelper.Exist("Level 3");
        if (!level1 || !level2 || !level3)
        {
            Assert.Fail("To disable buttons there is no need to deactivate it's object, you should just uncheck " +
                        "it's <Button>'s interactable property");
        }
        level1Button = PMHelper.Exist<Button>(level1);
        level2Button = PMHelper.Exist<Button>(level2);
        level3Button = PMHelper.Exist<Button>(level3);
        if (!level1Button || !level2Button || !level3Button)
        {
            Assert.Fail("To disable buttons there is no need to disable it's <Button> component, you should just uncheck " +
                        "<Button>'s interactable property");
        }
        
        correctButtonLevels = level1Button.interactable &&
                              level2Button.interactable &&
                              !level3Button.interactable;
        if (!correctButtonLevels)
        {
            Assert.Fail("From \"Main Menu\"'s ChooseLevel panel, player should be able to choose between levels," +
                        " that he had already passed and the one, that is next, others should be non-interactable");
        }
        
        GameObject.Find("Close").GetComponent<Button>().onClick.Invoke();
        yield return null;
        GameObject.Find("Play").GetComponent<Button>().onClick.Invoke();
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("When player left level without having it passed," +
                        " \"Play\" button should transfer him to same level");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;

        if (!cur.name.Equals("Level 2"))
        {
            Assert.Fail("When player left level without having it passed," +
                        " \"Play\" button should transfer him to same level");
        }

        GameObject.Find("Player").transform.position =
            GameObject.Find("LevelEnd").GetComponent<Collider2D>().bounds.center;

        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("After passing second level, third one should be loaded");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;

        if (!cur.name.Equals("Level 3"))
        {
            Assert.Fail("After passing second level, third one should be loaded");
        }
        
        IS.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;
        
        if (!cur.name.Equals("Main Menu"))
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }

        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        level1 = PMHelper.Exist("Level 1");
        level2 = PMHelper.Exist("Level 2");
        level3 = PMHelper.Exist("Level 3");
        if (!level1 || !level2 || !level3)
        {
            Assert.Fail("To disable buttons there is no need to deactivate it's object, you should just uncheck " +
                        "it's <Button>'s interactable property");
        }
        level1Button = PMHelper.Exist<Button>(level1);
        level2Button = PMHelper.Exist<Button>(level2);
        level3Button = PMHelper.Exist<Button>(level3);
        if (!level1Button || !level2Button || !level3Button)
        {
            Assert.Fail("To disable buttons there is no need to disable it's <Button> component, you should just uncheck " +
                        "<Button>'s interactable property");
        }
        
        correctButtonLevels = level1Button.interactable &&
                              level2Button.interactable &&
                              level3Button.interactable;
        
        if (!correctButtonLevels)
        {
            Assert.Fail("From \"Main Menu\"'s ChooseLevel panel, player should be able to choose between levels," +
                        " that he had already passed and the one, that is next");
        }
        
        level1Button.onClick.Invoke();
        
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("\"Level 1\" button should transfer player to \"Level 1\"");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;

        if (!cur.name.Equals("Level 1"))
        {
            Assert.Fail("\"Level 1\" button should transfer player to \"Level 1\"");
        }
        
        IS.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;
        
        if (!cur.name.Equals("Main Menu"))
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }

        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        level1 = PMHelper.Exist("Level 1");
        level2 = PMHelper.Exist("Level 2");
        level3 = PMHelper.Exist("Level 3");
        if (!level1 || !level2 || !level3)
        {
            Assert.Fail("To disable buttons there is no need to deactivate it's object, you should just uncheck " +
                        "it's <Button>'s interactable property");
        }
        level1Button = PMHelper.Exist<Button>(level1);
        level2Button = PMHelper.Exist<Button>(level2);
        level3Button = PMHelper.Exist<Button>(level3);
        if (!level1Button || !level2Button || !level3Button)
        {
            Assert.Fail("To disable buttons there is no need to disable it's <Button> component, you should just uncheck " +
                        "<Button>'s interactable property");
        }
        
        correctButtonLevels = level1Button.interactable &&
                              level2Button.interactable &&
                              level3Button.interactable;
        
        if (!correctButtonLevels)
        {
            Assert.Fail("From \"Main Menu\"'s ChooseLevel panel, player should be able to choose between levels," +
                        " that he had already passed and the one, that is next");
        }
        
        
        level2Button.onClick.Invoke();
        
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("\"Level 2\" button should transfer player to \"Level 2\"");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;

        if (!cur.name.Equals("Level 2"))
        {
            Assert.Fail("\"Level 2\" button should transfer player to \"Level 2\"");
        }
        
        IS.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;
        
        if (!cur.name.Equals("Main Menu"))
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }

        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        level1 = PMHelper.Exist("Level 1");
        level2 = PMHelper.Exist("Level 2");
        level3 = PMHelper.Exist("Level 3");
        if (!level1 || !level2 || !level3)
        {
            Assert.Fail("To disable buttons there is no need to deactivate it's object, you should just uncheck " +
                        "it's <Button>'s interactable property");
        }
        level1Button = PMHelper.Exist<Button>(level1);
        level2Button = PMHelper.Exist<Button>(level2);
        level3Button = PMHelper.Exist<Button>(level3);
        if (!level1Button || !level2Button || !level3Button)
        {
            Assert.Fail("To disable buttons there is no need to disable it's <Button> component, you should just uncheck " +
                        "<Button>'s interactable property");
        }
        
        correctButtonLevels = level1Button.interactable &&
                              level2Button.interactable &&
                              level3Button.interactable;
        
        if (!correctButtonLevels)
        {
            Assert.Fail("From \"Main Menu\"'s ChooseLevel panel, player should be able to choose between levels," +
                        " that he had already passed and the one, that is next");
        }
        
        
        level3Button.onClick.Invoke();
        
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("\"Level 3\" button should transfer player to \"Level 3\"");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;

        if (!cur.name.Equals("Level 3"))
        {
            Assert.Fail("\"Level 3\" button should transfer player to \"Level 3\"");
        }
        
        IS.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        start = Time.unscaledTime;
        yield return new WaitUntil(() =>
            SceneManager.GetActiveScene() != cur || (Time.unscaledTime - start) * Time.timeScale > 1);
        if ((Time.unscaledTime - start) * Time.timeScale >= 1)
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }
        
        cur = SceneManager.GetActiveScene();
        yield return null;
        
        if (!cur.name.Equals("Main Menu"))
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to \"Main Menu\"");
        }

        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        level1 = PMHelper.Exist("Level 1");
        level2 = PMHelper.Exist("Level 2");
        level3 = PMHelper.Exist("Level 3");
        if (!level1 || !level2 || !level3)
        {
            Assert.Fail("To disable buttons there is no need to deactivate it's object, you should just uncheck " +
                        "it's <Button>'s interactable property");
        }
        level1Button = PMHelper.Exist<Button>(level1);
        level2Button = PMHelper.Exist<Button>(level2);
        level3Button = PMHelper.Exist<Button>(level3);
        if (!level1Button || !level2Button || !level3Button)
        {
            Assert.Fail("To disable buttons there is no need to disable it's <Button> component, you should just uncheck " +
                        "<Button>'s interactable property");
        }
        
        correctButtonLevels = level1Button.interactable &&
                              level2Button.interactable &&
                              level3Button.interactable;
        
        if (!correctButtonLevels)
        {
            Assert.Fail("From \"Main Menu\"'s ChooseLevel panel, player should be able to choose between levels," +
                        " that he had already passed and the one, that is next");
        }
    }
}
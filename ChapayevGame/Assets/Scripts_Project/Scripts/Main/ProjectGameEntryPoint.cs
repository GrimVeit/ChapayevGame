using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectGameEntryPoint
{
    private static ProjectGameEntryPoint instance;
    private readonly UIProjectRootView rootView;
    private readonly Coroutines coroutines;
    public ProjectGameEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIProjectRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);

    }

    private static void GlobalSettings()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Autorun()
    {
        GlobalSettings();

        instance = new ProjectGameEntryPoint();
        instance.Run();

    }

    private void Run()
    {
        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        rootView.SetLoadScreen(0);

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        var sceneEntryPoint = Object.FindObjectOfType<MenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToGame += () => coroutines.StartCoroutine(LoadAndStartMiniGameScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMiniGameScene()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MINI_GAME);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToGame += () => coroutines.StartCoroutine(LoadAndStartMiniGameScene());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("Загрузка сцены - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}

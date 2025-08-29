using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class OtherSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UIOtherSceneRoot sceneRootPrefab;

    private UIOtherSceneRoot sceneRoot;

    private ViewContainer viewContainer;
    private WebViewPresenter otherWebViewPresenter;

    private FirebaseDatabasePresenter firebaseDatabasePresenter;

    public void Run(UIProjectRootView uIRootView)
    {
        Debug.Log("OPEN OTHER SCENE");

        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
        DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        sceneRoot = sceneRootPrefab;
        //uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        otherWebViewPresenter = new WebViewPresenter (new WebViewModel(), viewContainer.GetView<WebViewView>());
        otherWebViewPresenter.Initialize();

        firebaseDatabasePresenter = new FirebaseDatabasePresenter(new FirebaseDatabaseModel(firebaseAuth, databaseReference), viewContainer.GetView<FirebaseDatabaseView>());
        firebaseDatabasePresenter.Initialize();

        ActivateActions();

        //otherWebViewPresenter.GetLinkInTitleFromURL("https://dssm.us/1py6Kc");

        firebaseDatabasePresenter.GetLink();
    }

    private void ActivateActions()
    {
        firebaseDatabasePresenter.OnGetLink += GetUrlBD;
        firebaseDatabasePresenter.OnErrorGetLink += GoToMainMenu;

        otherWebViewPresenter.OnGetLinkFromTitle += GetUrl;
        otherWebViewPresenter.OnFail += GoToMainMenu;
    }

    private void DeactivateActions()
    {
        otherWebViewPresenter.OnGetLinkFromTitle -= GetUrl;
        otherWebViewPresenter.OnFail -= GoToMainMenu;
    }


    private void GetUrlBD(string url)
    {
        otherWebViewPresenter.GetLinkInTitleFromURL(url);
    }



    private void GetUrl(string URL)
    {
        if(URL == null)
        {
            GoToMainMenu();
            return;
        }

        Debug.Log("WOW");

        otherWebViewPresenter.SetURL(URL);
        otherWebViewPresenter.Load();
    }

    private void GoToMainMenu()
    {
        Debug.Log("NO GOOD, OPEN MAIN MENU");
        OnGoToMainMenu?.Invoke();
    }

    private void OnDestroy()
    {
        DeactivateActions();

        otherWebViewPresenter.Dispose();
    }

    #region Input

    public event Action OnGoToMainMenu;

    #endregion
}

using System;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;

public class CountryCheckerSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UICountryCheckerSceneRoot sceneRootPrefab;

    private UICountryCheckerSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private GeoLocationPresenter geoLocationPresenter;
    private InternetPresenter internetPresenter;
    private AudioPresenter soundPresenter;

    private FirebaseDatabasePresenter firebaseDatabaseRealtimePresenter;

    private string currentCountry;

    public void Run(UIProjectRootView uIRootView)
    {
        Debug.Log("OPEN COUNTRY CHECKER SCENE");

        sceneRoot = sceneRootPrefab;
        //uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                soundPresenter = new AudioPresenter(new AudioModel(sounds.sounds, PrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<AudioView>());
                soundPresenter.Initialize();

                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                firebaseDatabaseRealtimePresenter = new FirebaseDatabasePresenter
                (new FirebaseDatabaseModel(firebaseAuth, databaseReference), 
                viewContainer.GetView<FirebaseDatabaseView>());

                geoLocationPresenter = new GeoLocationPresenter(new GeoLocationModel());

                internetPresenter = new InternetPresenter(new InternetModel(), viewContainer.GetView<InternetView>());
                internetPresenter.Initialize();

                ActivateActions();

                internetPresenter.StartCheckConnection();
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }

    public void Dispose()
    {
        DeactivateActions();

        internetPresenter?.Dispose();
    }

    private void ActivateActions()
    {
        internetPresenter.OnInternetUnavailable += TransitionToMainMenu;
        internetPresenter.OnInternetAvailable += OnInternetAvailable;

        geoLocationPresenter.OnErrorGetCountry += TransitionToMainMenu;
        geoLocationPresenter.OnGetCountry += ActivateSceneInCountry;

        firebaseDatabaseRealtimePresenter.OnErrorGetCountries += TransitionToMainMenu;
        firebaseDatabaseRealtimePresenter.OnGetCountries += CheckCountry;
    }

    private void DeactivateActions()
    {
        internetPresenter.OnInternetUnavailable -= TransitionToMainMenu;
        internetPresenter.OnInternetAvailable -= OnInternetAvailable;

        geoLocationPresenter.OnErrorGetCountry -= TransitionToMainMenu;
        geoLocationPresenter.OnGetCountry -= ActivateSceneInCountry;

        firebaseDatabaseRealtimePresenter.OnErrorGetCountries -= TransitionToMainMenu;
        firebaseDatabaseRealtimePresenter.OnGetCountries -= CheckCountry;
    }

    private void OnInternetAvailable()
    {
        Debug.Log("INTERNET CONNECTION = TRUE");
        geoLocationPresenter.GetUserCountry();
    }

    private void ActivateSceneInCountry(string country)
    {
        currentCountry = country;

        firebaseDatabaseRealtimePresenter.GetCountries();
    }

    private void CheckCountry(List<string> countries)
    {
        if (countries.Contains(currentCountry))
        {
            Debug.Log("GOOD COUNTRY = TRUE");
            TransitionToOther();
        }
        else
        {
            Debug.Log("GOOD COUNTRY = FALSE");
            TransitionToMainMenu();
        }
    }

    #region Input

    public event Action GoToMainMenu;
    public event Action GoToOther;

    private void TransitionToMainMenu()
    {
        Dispose();
        Debug.Log("NO GOOD");
        GoToMainMenu?.Invoke();
    }

    private void TransitionToOther()
    {
        Dispose();
        Debug.Log("GOOD");
        GoToOther?.Invoke();
    }

    #endregion
}

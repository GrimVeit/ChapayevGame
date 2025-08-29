using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using UnityEngine;

public class FirebaseAuthenticationModel
{
    private readonly FirebaseAuth _auth;

    public string Nickname;

    private readonly ISoundProvider _soundProvider;

    public FirebaseAuthenticationModel(FirebaseAuth auth, ISoundProvider soundProvider)
    {
        _auth = auth;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        //auth = FirebaseAuth.DefaultInstance;
        //databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    #region Input

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
    }

    public bool IsAuthorization()
    {
        if (_auth.CurrentUser != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SignUp()
    {
        Debug.Log(Nickname);
        Coroutines.Start(SignUpCoroutine(Nickname + "@gmail.com", "123456"));
    }

    public void SignOut()
    {
        _auth.SignOut();
        OnSignOut_Action?.Invoke();
        OnChangeUser?.Invoke(_auth.CurrentUser.UserId);
    }

    public void DeleteAccount()
    {
        OnDeleteAccount_Action?.Invoke();
        Coroutines.Start(DeleteAuth_Coroutine());
    }

    #endregion

    #region Output

    public event Action<string> OnChangeUser;

    public event Action OnSignIn_Action;
    public event Action<string> OnSignInError_Action;

    public event Action OnSignUp_Action;
    public event Action OnSignUpError_Action;
    public event Action<string> OnSignUpMessage_Action;

    public event Action OnSignOut_Action;

    public event Action OnDeleteAccount_Action;

    public event Action OnEnterRegisterLoginSuccess;
    public event Action<string> OnEnterRegisterLoginError;

    public event Action<string> OnGetRandomNickname;

    #endregion

    #region Coroutines

    private IEnumerator SignUpCoroutine(string emailTextValue, string passwordTextValue)
    {
        var task = _auth.CreateUserWithEmailAndPasswordAsync(emailTextValue, passwordTextValue);

        OnSignUpMessage_Action?.Invoke("Generating your nickname...");

        yield return new WaitUntil(predicate: () => task.IsCompleted);
        yield return null;

        if (task.Exception != null)
        {
            FirebaseException firebaseException = task.Exception.Flatten().InnerExceptions[0] as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;

            Debug.Log(authError);

            switch (authError)
            {
                case AuthError.NetworkRequestFailed:
                    OnSignUpMessage_Action?.Invoke("Network error. Please check your internet connection...");
                    break;
                //case AuthError.EmailAlreadyInUse:
                //    OnSignUpMessage_Action?.Invoke("This nickname is already in use.");
                //    break;
                //case AuthError.InvalidEmail:
                //    OnSignUpMessage_Action?.Invoke("Invalid nickname format.");
                //    break;
                default:
                    OnSignUpMessage_Action?.Invoke("Unknown error or network error...");
                    break;
            }

            Debug.Log("Не удалось создать аккаунт - " + task.Exception);
            _soundProvider.PlayOneShot("SignUpError");
            OnSignUpError_Action?.Invoke();
            yield break;
        }

        Debug.Log("Аккаунт создан");
        _soundProvider.PlayOneShot("SignUpSuccess");
        OnSignUpMessage_Action?.Invoke("Success...");
        OnChangeUser?.Invoke(_auth.CurrentUser.UserId);
        OnSignUp_Action?.Invoke();

    }

    private IEnumerator DeleteAuth_Coroutine()
    {
        var task = _auth.CurrentUser.DeleteAsync();

        yield return new WaitUntil(predicate: () => task.IsCompleted);

        if (task.Exception != null)
        {
            Debug.Log("Ошибка удаления аккаунта - " + task.Exception.Message);
            yield break;
        }

        SignOut();
    }

    #endregion
}

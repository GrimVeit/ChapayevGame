using System;

public class FirebaseAuthenticationPresenter : IAuthenticationSignUpInfoProvider
{
    private readonly FirebaseAuthenticationModel _model;

    public FirebaseAuthenticationPresenter(FirebaseAuthenticationModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {

    }

    #region Input

    public bool IsAuthorization()
    {
        return _model.IsAuthorization();
    }

    public void DeleteAccount()
    {
        _model.DeleteAccount();
    }

    public void SignOut()
    {
        _model.SignOut();
    }

    public void SignUp()
    {
        _model.SignUp();
    }

    public void SetNickname(string nickname)
    {
        _model.SetNickname(nickname);
    }

    public event Action<string> OnChangeCurrentUser
    {
        add { _model.OnChangeUser += value; }
        remove { _model.OnChangeUser -= value; }
    }



    public event Action OnSignIn
    {
        add { _model.OnSignIn_Action += value; }
        remove { _model.OnSignIn_Action -= value; }
    }

    public event Action<string> OnSignUpMessage_Action
    {
        add => _model.OnSignUpMessage_Action += value;
        remove => _model.OnSignUpMessage_Action -= value;
    }



    public event Action OnSignUp
    {
        add { _model.OnSignUp_Action += value; }
        remove { _model.OnSignUp_Action -= value; }
    }

    public event Action OnSignUpError
    {
        add { _model.OnSignUpError_Action += value; }
        remove { _model.OnSignUpError_Action -= value; }
    }


    public event Action OnSignOut
    {
        add { _model.OnSignOut_Action += value; }
        remove { _model.OnSignOut_Action -= value; }
    }

    public event Action OnDeleteAccount
    {
        add { _model.OnDeleteAccount_Action += value; }
        remove { _model.OnDeleteAccount_Action -= value; }
    }



    #endregion
}

public interface IAuthenticationSignUpInfoProvider
{
    public event Action<string> OnSignUpMessage_Action;
}

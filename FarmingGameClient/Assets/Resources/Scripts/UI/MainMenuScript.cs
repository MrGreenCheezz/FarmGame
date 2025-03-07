using TMPro;
using UnityEngine;


public enum MainMenuStates
{
    Start,
    Register,
    Login
}
public class MainMenuScript : MonoBehaviour
{
    public MainMenuStates currentMenuState = MainMenuStates.Start;
    [SerializeField]
    private GameObject LoginMenu;
    [SerializeField]
    private GameObject RegisterMenu;
    [SerializeField]
    private GameObject StartMenu;
    [SerializeField]
    private TextMeshProUGUI _errorText;

    [SerializeField]
    private Material _errorMaterial;
    [SerializeField]
    private Material _successMaterial;

    public string LoginFormEmail = "";
    public string LoginFormPassword = "";

    public string RegisterFormEmail = "";
    public string RegisterFormUserName = "";
    public string RegisterFormPassword = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void ChangeMenuState(MainMenuStates state)
    {
        currentMenuState = state;
        switch (state)
        {
            case MainMenuStates.Start:
                {
                    LoginMenu.SetActive(false);
                    RegisterMenu.SetActive(false);
                    StartMenu.SetActive(true);
                    break;
                }

            case MainMenuStates.Register:
                {
                    LoginMenu.SetActive(false);
                    RegisterMenu.SetActive(true);
                    StartMenu.SetActive(false);
                    break;
                }
            case MainMenuStates.Login:
                {
                    LoginMenu.SetActive(true);
                    RegisterMenu.SetActive(false);
                    StartMenu.SetActive(false);
                    break;
                }
        }
    }
 
    public void SetErrorString(string errorText)
    {
        _errorText.text = errorText;
        _errorText.material = _errorMaterial;
    }

    public void SetSuccesString(string succesString)
    {
        _errorText.text = succesString;
        _errorText.fontMaterial = _successMaterial;
    }

    public void ResetErrorString()
    {
        _errorText.text = "";
    }
    #region MainMenu
    public void LoginButtonPressed()
    {
        ChangeMenuState(MainMenuStates.Login);
    }
    public void RegisterButtonPressed()
    {
        ChangeMenuState(MainMenuStates.Register);
    }
    public void ExitButtonPressed()
    {
        Application.Quit();
    }
    public void ReturnButtonPressed()
    {
        ChangeMenuState(MainMenuStates.Start);
        ResetErrorString();
    }
    #endregion
    #region LoginForm
    public void SetLoginEmail(string email)
    {
        LoginFormEmail = email;
    }
    public void SetLoginPassword(string password)
    {
        LoginFormPassword = password;
    }
    public void LoginFormLoginButtonPressed()
    {
        ResetErrorString();
        StartCoroutine(GameInstance.instance.Login(LoginFormEmail, LoginFormPassword, this));
    }
    #endregion
    #region RegisterForm
    public void RegisterFormRegisterButtonPressed()
    {
        ResetErrorString();
        StartCoroutine(GameInstance.instance.RegisterUser(RegisterFormUserName, RegisterFormEmail ,RegisterFormPassword,this));
    }
    public void SetRegisterEmail(string email)
    {
        RegisterFormEmail = email;
    }
    public void SetRegisterUsername(string username)
    {
        RegisterFormUserName = username;
    }
    public void SetRegisterPassword(string password)
    {
        RegisterFormPassword = password;
    }
    #endregion


    // Update is called once per frame
    void Update()
        {

        }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    private VisualElement mainMenu;
    private VisualElement settingsMenu;
    private UIDocument document;
    private Button continueBtn;
    private Button newGameBtn;
    private Button savesBtn;
    private Button settingBtn;
    private Button exitBtn;
    private Button backButton;
    private List<Button> menuButtons = new List<Button>();

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        continueBtn = document.rootVisualElement.Q("ContinueBtn") as Button;
        continueBtn.RegisterCallback<ClickEvent>(OnContinueClick);
        newGameBtn = document.rootVisualElement.Q("NewGameBtn") as Button;
        newGameBtn.RegisterCallback<ClickEvent>(OnNewGameClick);
        savesBtn = document.rootVisualElement.Q("SavesBtn") as Button;
        savesBtn.RegisterCallback<ClickEvent>(OnSavesClick);
        settingBtn = document.rootVisualElement.Q("SettingsBtn") as Button;
        settingBtn.RegisterCallback<ClickEvent>(OnSettingsClick);
        exitBtn = document.rootVisualElement.Q("ExitBtn") as Button;
        exitBtn.RegisterCallback<ClickEvent>(OnExitClick);
        backButton = document.rootVisualElement.Q("BackBtn") as Button;
        backButton.RegisterCallback<ClickEvent>(OnBackClick);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        mainMenu = root.Q<VisualElement>("MainMenu");
        settingsMenu = root.Q<VisualElement>("SettingsMenu");
    }

    private void OnDisable()
    {
        continueBtn.UnregisterCallback<ClickEvent>(OnContinueClick);
        newGameBtn.UnregisterCallback<ClickEvent>(OnNewGameClick);
        savesBtn.UnregisterCallback<ClickEvent>(OnSavesClick);
        settingBtn.UnregisterCallback<ClickEvent>(OnSettingsClick);
        exitBtn.UnregisterCallback<ClickEvent>(OnExitClick);
        backButton.UnregisterCallback<ClickEvent>(OnBackClick);

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnContinueClick(ClickEvent evt)
    {
        SceneManager.LoadScene("TestLvlMax");
    }

    private void OnNewGameClick(ClickEvent evt)
    {
        Debug.Log("OnNewGameClick");
    }

    private void OnSavesClick(ClickEvent evt)
    {
        Debug.Log("OnSavesClick");
    }

    private void OnSettingsClick(ClickEvent evt)
    {
        mainMenu.style.display = DisplayStyle.None;
        settingsMenu.style.display = DisplayStyle.Flex;
    }

    private void OnExitClick(ClickEvent evt)
    {
        Debug.Log("OnExitClick");
    }

    private void OnBackClick(ClickEvent evt)
    {
        settingsMenu.style.display = DisplayStyle.None;
        mainMenu.style.display = DisplayStyle.Flex;
    }

    private void OnAllButtonsClick(ClickEvent evt) // для допустим звука нажатия на все кнопки
    {
        Debug.Log("Бииип");
    }
}

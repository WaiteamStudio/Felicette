using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private VisualElement pauseMenu;
    private VisualElement bgMenu;
    private VisualElement settingsMenu;
    private UIDocument document;
    private Button continueBtn;
    private Button settingsBtn;
    private Button exitBtn;
    private Button backBtn;
    private List<Button> menuButtons = new List<Button>();

    public static bool isPaused;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        continueBtn = document.rootVisualElement.Q("UnpauseBtn") as Button;
        continueBtn.RegisterCallback<ClickEvent>(OnContinueClick);
        settingsBtn = document.rootVisualElement.Q("SettingsBtn") as Button;
        settingsBtn.RegisterCallback<ClickEvent>(OnSettingsClick);
        exitBtn = document.rootVisualElement.Q("ExitBtn") as Button;
        exitBtn.RegisterCallback<ClickEvent>(OnExitClick);
        backBtn = document.rootVisualElement.Q("BackBtn") as Button;
        backBtn.RegisterCallback<ClickEvent>(OnBackBtn);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pauseMenu = root.Q<VisualElement>("PauseMenu");
        settingsMenu = root.Q<VisualElement>("SettingsMenu");
        bgMenu = root.Q<VisualElement>("BG");
    }

    private void OnDisable()
    {
        continueBtn.UnregisterCallback<ClickEvent>(OnContinueClick);
        settingsBtn.UnregisterCallback<ClickEvent>(OnSettingsClick);
        exitBtn.UnregisterCallback<ClickEvent>(OnExitClick);
        backBtn.UnregisterCallback<ClickEvent>(OnBackBtn);

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.style.display = DisplayStyle.Flex;
        settingsMenu.style.display = DisplayStyle.None;
        bgMenu.style.display = DisplayStyle.Flex;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.style.display = DisplayStyle.None;
        settingsMenu.style.display = DisplayStyle.None;
        bgMenu.style.display = DisplayStyle.None;
        isPaused = false;
    }

    private void OnContinueClick(ClickEvent evt)
    {
        Time.timeScale = 1f;
        pauseMenu.style.display = DisplayStyle.None;
        isPaused = false;
    }

    private void OnSettingsClick(ClickEvent evt)
    {
        bgMenu.style.display = DisplayStyle.None;
        settingsMenu.style.display = DisplayStyle.Flex;
    }

    private void OnBackBtn(ClickEvent evt)
    {
        settingsMenu.style.display = DisplayStyle.None;
        bgMenu.style.display = DisplayStyle.Flex;
    }

    private void OnExitClick(ClickEvent evt)
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MenuTest");
    }

    private void OnAllButtonsClick(ClickEvent evt) // для допустим звука нажатия на все кнопки
    {
        Debug.Log("Бииип");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.EventSystems;


public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject pauseScreenFirstButton, firstWeaponFirstButton, levelUpInterfaceFirstButton, victoryFirstButton, levelUpInterfaceFirstButtonAlternate1, levelUpInterfaceFirstButtonAlternate2, endLevelFirstButton;

    [SerializeField] private Slider expLvlSlider;
    [SerializeField] private TMP_Text expLvlText;

    public LevelUpSelectionButton[] levelUpButtons;
    public GameObject levelUpPanel;

    public TMP_Text coinText;
    public TMP_Text timeText;

    public GameObject levelEndScreen;
    public TMP_Text endTimeText;

    public string mainMenuName;
    public GameObject pauseScreen;
    public GameObject finalScreen;

    public GameObject firstWeaponSelectionPanel;
    public FirstWeaponSelectionButton[] firstWeaponsSelectionButtons;


    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstWeaponFirstButton);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            PauseUnpause();
        }
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentLevel)
    {
        expLvlSlider.maxValue = levelExp;
        expLvlSlider.value = currentExp;

        expLvlText.text = "Level:  " + currentLevel;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void UpdateCoins() 
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }

    
    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }
    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange() 
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons() 
    {
        PlayerStatController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = "Time: " + minutes + "." + seconds.ToString("00");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("I'm Quitting!");
    }

    public void PauseUnpause()
    {
        if (!levelUpPanel.activeSelf && !levelEndScreen.activeSelf && !firstWeaponSelectionPanel.activeSelf)
        {
            if (pauseScreen.activeSelf)
            {

                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;

                //clear selected object
                EventSystem.current.SetSelectedGameObject(null);
                //set a new selected object
                EventSystem.current.SetSelectedGameObject(pauseScreenFirstButton);
            }
        }
    }

    public void OpenFinalScreen()
    {
        finalScreen.SetActive(true);
        Time.timeScale = 0f;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(victoryFirstButton);
    }
}

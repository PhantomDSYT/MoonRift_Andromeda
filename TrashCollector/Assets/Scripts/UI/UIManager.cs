using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text scoreText;
    [SerializeField] Text trashText;
    [SerializeField] Text evolveText;
    [SerializeField] Text shopText;
    public Slider healthSlider;

    [SerializeField] Slider xpSlider;
    [SerializeField] Image xpSliderFill;
    [SerializeField] Color xpSliderColor;
    private bool xpMax = false;

    [SerializeField] GameObject EvoManager;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject CongratsUI;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject TipUI;
    [SerializeField] GameObject evoUI;

    public static bool GamePaused = false;

    public bool endgame = false;
    public int endgameRequired = 0;

    public int score = 0;
    public int trashPoints = 0;
    public int xp = 0;
    public int xpCap = 30;
    public int level = 1;
    public bool evolve = false;

    //Tutorial
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] PlayerBehaviour playerBehaviour;
    bool onceOpened = false;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        OpenTip();
        scoreText.text = "Score: " + score.ToString();
        trashText.text = trashPoints.ToString();
        shopText.text = trashPoints.ToString();
        xpSlider.maxValue = xpCap;
        xpSlider.value = xp;
    }
    public void AddPoint(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }


    public void AddTrash(int points)
    {
        trashPoints += points;
        trashText.text = trashPoints.ToString();
        shopText.text = trashPoints.ToString();
    }

    public void AddXP(int points)
    {
        if(!xpMax)
        {
            xp += points;
            xpSlider.value = xp;
        }
    }

    public void SubtractTrash(int points)
    {
        trashPoints -= points;
        trashText.text = trashPoints.ToString();
        shopText.text = trashPoints.ToString();
        
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            if(GamePaused)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GamePaused && !shopUI.activeInHierarchy)
            {
                ResumeGame();
            }
            else if (!GamePaused && !shopUI.activeInHierarchy)
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y) && mainUI.activeInHierarchy && evolve)
        {
            EvoManager.GetComponent<EvolutionManager>().Evolve();
        }

        if (evolve)
            evolveText.gameObject.SetActive(true);
        else
            evolveText.gameObject.SetActive(false);



        if (endgameRequired == 30)
            endgame = true;


        openTutorial();

        if(GameObject.FindGameObjectsWithTag("trash").Length <= 40 && endgame)
        {
            Debug.Log("Zero Trash");

            PlayerPrefs.SetInt("highscore", score);

            mainUI.SetActive(false);
            CongratsUI.SetActive(true);

            Time.timeScale = 0f;
            GamePaused = true;

            AIBehavior[] bevs = FindObjectsOfType<AIBehavior>();
            AIOverhaul[] ovs = FindObjectsOfType<AIOverhaul>();
            foreach (AIBehavior aib in bevs)
            {
                aib.pause = true;
            }
            foreach (AIOverhaul ove in ovs)
            {
                ove.pause = true;
            }
        }
    }

    private void CloseShop()
    {
        mainUI.SetActive(true);
        shopUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        AIBehavior[] bevs = FindObjectsOfType<AIBehavior>();
        AIOverhaul[] ovs = FindObjectsOfType<AIOverhaul>();
        foreach (AIBehavior aib in bevs)
        {
            aib.pause = false;
        }
        foreach (AIOverhaul ove in ovs)
        {
            ove.pause = false;
        }
    }

    private void OpenShop()
    {
        mainUI.SetActive(false);
        shopUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        AIBehavior[] bevs = FindObjectsOfType<AIBehavior>();
        AIOverhaul[] ovs = FindObjectsOfType<AIOverhaul>();
        foreach (AIBehavior aib in bevs)
        {
            aib.pause = true;
        }
        foreach (AIOverhaul ove in ovs)
        {
            ove.pause = true;
        }
    }
    private void OpenTip()
    {
        TipUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        AIBehavior[] bevs = FindObjectsOfType<AIBehavior>();
        foreach (AIBehavior aib in bevs)
            aib.pause = true;

    }

    public void ResumeGame()
    {
        mainUI.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        AIBehavior[] bevs = FindObjectsOfType<AIBehavior>();
        foreach(AIBehavior aib in bevs)
            aib.pause = false;
    }

    void PauseGame()
    {
        mainUI.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        AIBehavior[] bevs = FindObjectsOfType<AIBehavior>();
        foreach (AIBehavior aib in bevs)
            aib.pause = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void LevelUp()
    {
        xp -= xpCap;
        level++;

        if (level % 5 == 0)
            evolve = true;

        if (level >= 10)
        {
            xpSlider.value = xpSlider.maxValue;
            xpSliderFill.color = xpSliderColor;
            xpMax = true;
        }
        else
        {
            xpCap =(int)(xpCap * 1.25);
            xpSlider.maxValue = xpCap;
            xpSlider.value = xp;
        }
        
    }

    public void openTutorial()
    {
        if(playerBehaviour.firstHit == true && onceOpened == false)
        {
            tutorialPanel.SetActive(true);
            onceOpened = true;
        }
    }
}
 
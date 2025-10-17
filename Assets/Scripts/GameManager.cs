using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //--> variable estatica, es global se puede acceder desde cualquier script y compartida entre todas las copias
    //get; private set; (con esto hacemos que cualquiera pueda acceder pero que solo se pueda modificar desde aquí)
    public static GameManager instance { get; private set; }

    public bool _isPaused = false;
    public InputActionAsset playerInputs;
    private InputAction _pauseInput;
    public int _stars = 0;
    public int _coins = 0;

    private bool hasWon = false;

    private int starsInlevel;
    //private StarSensor _starSensor;

    //estrellas

    

    void Awake()
    {
        //busca si instance esta rellenado, si ya esta relleno comprueba si lo que está dentro es este objeto u otro
        if (instance != null && instance != this)
        {
            //si se cumple se destruye
            Destroy(gameObject);
        }
        else
        {
            //si no se cumple se rellena
            instance = this;
        }

        //cuando cargas escenas, todo lo que había en la anterior desaparecía
        // esto hace que cuando cambies de escena no se destruya
        DontDestroyOnLoad(gameObject);

        //_starSensor = GameObject.Find("StarSensor").GetComponent<StarSensor>();

        _pauseInput = InputSystem.actions["Pause"];
    }

    void Start()
    {
        AudioManager.instance.ChangeBGM(AudioManager.instance.level1BGM);
        //starsInLevel = StarSensor.instance.StarsRemaining();
        starsInlevel = StarsRemaining();
    }

    void Update()
    {
        if (_pauseInput.WasPressedThisFrame() && !hasWon)
        {
            Pause();
        }

        //Cambiar la condición, que se compruebe fuera del update que si no se lia por rendimiento
        /*if(StarSensor.instance.StarsRemaining() == 0)
        {
            Victory();
        }*/
    }

    public int StarsRemaining()
    {
        //Collider2D[] stars = Physics2D.OverlapBoxAll(_starSensor.position, _starSensorArea, 0, _starMask);

        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star"); 
        return stars.Length;
    }

    public void AddStar()
    {
        _stars++;
        starsInlevel--;
        GUIManager.Instance.UpdateStarsText();
        if(starsInlevel == 0)
        {
            Victory();
        }
    }

    public void AddCoin()
    {
        _coins++;
        GUIManager.Instance.UpdateCoinsText();
    }

    public void Pause()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._pauseCanvas, false);
            playerInputs.FindActionMap("Player").Enable();
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._pauseCanvas, true);
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._optionsCanvas, false);
            playerInputs.FindActionMap("Player").Disable();
            _isPaused = true;
        }
    }

    public void Victory()
    {
        Time.timeScale = 0;
        GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._victoryCanvas, true);
        playerInputs.FindActionMap("Player").Disable();
        hasWon = true;
    }

    /*public void IsInMenu()
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            AudioManager.instance.ChangeBGM(AudioManager.instance.menuBGM);
        }
        
    }*/

    
}
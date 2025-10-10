using UnityEngine;
using UnityEngine.InputSystem;

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

        _pauseInput = InputSystem.actions["Pause"];
    }

    void Update()
    {
        if (_pauseInput.WasPressedThisFrame())
        {
            Pause();
        }
    }

    public void AddStar()
    {
        _stars++;
        GUIManager.Instance.UpdateStarsText();
        Debug.Log("Estrellas recogidas = " + _stars);
    }

    public void AddCoin()
    {
        _coins++;
        GUIManager.Instance.UpdateCoinsText();
        Debug.Log("Monedas recogidas = " + _coins);
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

    
}

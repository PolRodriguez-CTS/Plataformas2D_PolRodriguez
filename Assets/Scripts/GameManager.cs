using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //--> variable estatica, es global se puede acceder desde cualquier script y compartida entre todas las copias
    //get; private set; (con esto hacemos que cualquiera pueda acceder pero que solo se pueda modificar desde aquí)
    public static GameManager instance { get; private set; }
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private InputActionAsset playerInputs;
    private InputAction _pauseInput;
    int _stars = 0;
    private bool _isPaused = false;

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
        Debug.Log("Estrellas recogidas = " + _stars);
    }

    public void Pause()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            _pauseCanvas.SetActive(false);
            playerInputs.FindActionMap("Player").Enable();
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            _pauseCanvas.SetActive(true);
            playerInputs.FindActionMap("Player").Disable();
            _isPaused = true;
        }
    }
}

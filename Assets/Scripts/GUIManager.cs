using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    public GameObject _pauseCanvas;
    public GameObject _optionsCanvas;
    public GameObject _victoryCanvas;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Text _starText;
    [SerializeField] private Text _coinText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        //DontDestroyOnLoad(gameObject);
    }

    public void Resume()
    {
        GameManager.instance.Pause();
    }

    public void ChangeScene(string sceneName)
    {
        SceneLoader.instance.ChangeScene(sceneName);   
    }

    public void ChangeCanvasStatus(GameObject canvas, bool status)
    {
        canvas.SetActive(status);
    }

    public void UpdateHealthBar(float _currentHealth, float _maxHealth)
    {
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    public void UpdateStarsText()
    {
        _starText.text = "x" + GameManager.instance._stars.ToString();
    }

    public void UpdateCoinsText()
    {
        _coinText.text = "x" + GameManager.instance._coins.ToString();
    }
}

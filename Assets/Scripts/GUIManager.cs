using UnityEngine;
using UnityEngine.UI;
public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    public GameObject _pauseCanvas;
    public GameObject _optionsCanvas;
    private Image _healthBar;

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

    public void UpdateHealthBar(int _currentHealth, int _maxHealth)
    {
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }
}

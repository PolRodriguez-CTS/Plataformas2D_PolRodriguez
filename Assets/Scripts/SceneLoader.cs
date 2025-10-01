using System.Collections;
using Unity.Burst;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private Text _loadingText;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadNewScene(sceneName));
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        yield return null; //con esto se pausa una corutina, una corutina tiene que tener el yield return siempre

        _loadingCanvas.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        float fakeLoadPercentage = 0;

        while (!asyncLoad.isDone)
        {
            //_loadingBar.fillAmount = asyncLoad.progress; //carga según el progreso de carga, en escenas poco pesadas es casi instantaneo

            //barra de carga falsa
            fakeLoadPercentage += 0.01f;
            Mathf.Clamp01(fakeLoadPercentage); //clamp01, limitar valor entre 0 y 1
            _loadingBar.fillAmount = fakeLoadPercentage;
            _loadingText.text = (fakeLoadPercentage * 100).ToString("F0") + "%";

            if (asyncLoad.progress >= 0.9f && fakeLoadPercentage >= 0.99f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            float _loadingBarFakeTime = 0.01f;
            yield return new WaitForSecondsRealtime(_loadingBarFakeTime);
        }

        Time.timeScale = 1;
        GameManager.instance.playerInputs.FindActionMap("Player").Enable();
        GameManager.instance._isPaused = false;
        
        _loadingCanvas.SetActive(false);
    }
}

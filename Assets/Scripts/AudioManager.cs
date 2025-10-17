using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance{get; private set; }

    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;
    //SFX
    public AudioClip _starSFX;
    public AudioClip _coinSFX;
    public AudioClip _heartSFX;
    public AudioClip _jumpSFX;
    public AudioClip _attackSFX;
    public AudioClip _dashAttackSFX;
    public AudioClip _hurtSFX;
    public AudioClip _deathSFX;
    public AudioClip _mimikSFX;
    //BGM
    public AudioClip menuBGM;
    public AudioClip level1BGM;
    public AudioClip gameOverBGM;

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

    public void ReproduceSound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void ChangeBGM(AudioClip bgmClip)
    {
        _bgmSource.Stop();
        _bgmSource.clip = bgmClip;
        _bgmSource.Play();
    }

    public void IsInMenu()
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            ChangeBGM(menuBGM);
        }
    }
    
    
    /*public void StarSFX()
    {
        _sfxSource.PlayOneShot(_starSFX);
    }*/

    /*public void CoinSFX()
    {
        _sfxSource.PlayOneShot(_coinSFX);
    }*/
}
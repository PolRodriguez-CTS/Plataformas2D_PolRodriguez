using UnityEngine;

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
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ReproduceSound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void ChangeBGM(AudioClip bgmClip)
    {
        _bgmSource.clip = bgmClip;
        _bgmSource.Play();
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
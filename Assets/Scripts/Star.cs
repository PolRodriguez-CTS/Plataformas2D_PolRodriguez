using UnityEngine;

public class Star : MonoBehaviour
{
    //GameManager _gameManager;

    void Awake()
    {
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interaction()
    {
        //_gameManager.AddStar();
        AudioManager.instance.ReproduceSound(AudioManager.instance._starSFX);
        GameManager.instance.AddStar();
        Destroy(gameObject);
    }
}
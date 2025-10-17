using UnityEngine;

public class Star : MonoBehaviour
{
    void Awake()
    {
    }

    public void StarInteraction()
    {
        AudioManager.instance.ReproduceSound(AudioManager.instance._starSFX);
        GameManager.instance.AddStar();
        Destroy(gameObject);
    }
}
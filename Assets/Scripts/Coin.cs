using System.Xml.Serialization;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Awake()
    {

    }

    /*public void CoinInteraction()
    {
        GameManager.instance.AddCoin();
        Destroy(gameObject);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.ReproduceSound(AudioManager.instance._coinSFX);
            GameManager.instance.AddCoin();
            Destroy(gameObject);
        }
    }
}

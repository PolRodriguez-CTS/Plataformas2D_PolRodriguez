using UnityEngine;

public class Coin : MonoBehaviour
{
    void Awake()
    {

    }

    public void CoinInteraction()
    {
        GameManager.instance.AddCoin();
        Destroy(gameObject);
    }
}

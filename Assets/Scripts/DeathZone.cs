using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerController _playerScript = collision.gameObject.GetComponent<PlayerController>();
            _playerScript.Death();
        }
    }
}

using UnityEditor.Analytics;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float _heartHealPoints = 3;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.ReproduceSound(AudioManager.instance._heartSFX);
            PlayerController _playerScript = collision.gameObject.GetComponent<PlayerController>();
            _playerScript.Heal(_heartHealPoints);
            Destroy(gameObject);
        }
    }
}

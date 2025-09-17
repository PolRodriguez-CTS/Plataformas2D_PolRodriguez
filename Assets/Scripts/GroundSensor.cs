using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] public bool _isGrounded;

    void Start()
    {
        _isGrounded = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _isGrounded = true;
            Debug.Log(_isGrounded);
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _isGrounded = false;
            Debug.Log(_isGrounded);
        }
    }
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _mimikSpeed = 1.5f;
    private int _mimikDirection = 1;
    [SerializeField] private int _mimikDamage = 1;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = transform.position + new Vector3(_mimikSpeed * _mimikDirection, 0, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.layer == 3)
        {
            _mimikDirection += -1;
                if (collision.gameObject.tag == "Player")
            {
                Debug.Log("te muerdo");
            }
        }
        
    }
}

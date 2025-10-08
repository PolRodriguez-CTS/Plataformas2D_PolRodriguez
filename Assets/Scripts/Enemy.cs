using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _mimikSpeed = 1.5f;
    private int _mimikDirection = 1;
    [SerializeField] private float _mimikDamage = 1;

    [SerializeField] private Transform _enemyAttackHitbox;
    [SerializeField] private Vector2 _attackHitbox = new Vector2(1, 1);

    //vida
    [SerializeField] private float _enemyMaxHealth = 3;
    [SerializeField] private float _enemyCurrentHealth;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _enemyCurrentHealth = _enemyMaxHealth;
    }

    void Update()
    {
        transform.position = transform.position + new Vector3(_mimikSpeed * _mimikDirection, 0, 0) * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_enemyAttackHitbox.position, _attackHitbox);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.layer == 3)
        {
            _mimikDirection *= -1;
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("te muerdo");
                PlayerController _playerScript = collision.gameObject.GetComponent<PlayerController>();
                _playerScript.TakeDamage(_mimikDamage);
            }
        }

        if (collision.gameObject.tag == "Edge")
        {
            Debug.Log("Borde detectado");
            _mimikDirection *= -1;
        }
    }

    public void EnemyTakeDamage(float damage)
    {
        _enemyCurrentHealth -= damage;
        Debug.Log(_enemyCurrentHealth);
        if (_enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
}

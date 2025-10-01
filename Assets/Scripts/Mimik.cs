using UnityEngine;

public class Mimik : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _mimikSpeed = 1.5f;
    private int _mimikDirection = 1;

    [SerializeField] private Transform _edgeSensorPosition;
    [SerializeField] private Vector2 _edgeSensorSize = new Vector2(1,1);
    [SerializeField] private Transform _enemyAttackHitbox;
    [SerializeField] private Vector2 _attackHitbox = new Vector2(1,1);

    public Vector3[] limites;
    private int _mimikDamage = 1;

    private PlayerController _playerController;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!IsGrounded() || PlayerDetection())
        {
            _mimikDirection *= -1;
        }
        transform.position = transform.position + new Vector3(_mimikSpeed * _mimikDirection, 0, 0) * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_edgeSensorPosition.position + limites[0], _edgeSensorSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_edgeSensorPosition.position + limites[1], _edgeSensorSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_enemyAttackHitbox.position, _attackHitbox);
    }

    bool IsGrounded()
    {
        Collider2D[] ground = Physics2D.OverlapBoxAll(_edgeSensorPosition.position + limites[0], _edgeSensorSize, 0);

        foreach (Collider2D item in ground)
        {
            if (item.gameObject.layer == 3)
            {
                return true;
            }
        }
        return false;
    }

    bool PlayerDetection()
    {
        Collider2D[] hitbox = Physics2D.OverlapBoxAll(_enemyAttackHitbox.position, _attackHitbox, 0);

        foreach(Collider2D item in hitbox)
        {
            if(item.gameObject.tag == "Player")
            {
                /*PlayerController _playerScript = item.gameObject.GetComponent<PlayerController>();
                _playerScript.TakeDamage(_mimikDamage);*/
                return true;
            }
        }
        return false;
    }
}

using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector2 _backgroundVelocity;
    [SerializeField] private Vector2 _offset;
    private SpriteRenderer _spriteRenderer;
    //Rigidbody2D _playerRb;
    //[SerializeField] private float _playerSpeedOffset = 0.2f;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_playerRb = _player.GetComponentInParent<Rigidbody2D>();

    }

    void Update()
    {
        _offset = /*(_playerRb.linearVelocity.x * _playerSpeedOffset) * */ _backgroundVelocity * Time.deltaTime;
        _spriteRenderer.material.mainTextureOffset += _offset;
    }
}

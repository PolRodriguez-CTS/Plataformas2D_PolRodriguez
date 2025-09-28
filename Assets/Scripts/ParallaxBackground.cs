using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector2 _backgroundVelocity;
    [SerializeField] private Vector2 _offset;
    private SpriteRenderer _spriteRenderer;
    Rigidbody2D _playerRb;
    [SerializeField] private float _playerSpeedOffset = 0.2f;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerRb = _player.GetComponentInParent<Rigidbody2D>();

    }

    void Update()
    {
        //_playerRb.linearVelocity.x * _playerSpeedOffset --> esto hace que el parallax funcione según el movimiento del jugador en vez de una velocidad fija
        _offset = (_playerRb.linearVelocity.x * _playerSpeedOffset) * _backgroundVelocity * Time.deltaTime;
        //esto hace que al offset del material se le sume el vector offset, lo que determina la velocidad a la que se mueve el fondo respecto al jugador 
        _spriteRenderer.material.mainTextureOffset += _offset;
    }
}

using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //--> variables de componentes
    Collider2D _collider2D;
    Rigidbody2D _rigidbody;
    Animator _animator;
    //private Mimik _mimik;

    //  --Groun sensor antiguo--
    //GroundSensor _groundSensor;
    public Transform _playerSpawn;

    //--> variables de input
    //--> botones
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _interactAction;

    //--> vector de movimiento
    private Vector2 _moveInput;
    [SerializeField] private float _playerSpeed = 4.5f;
    [SerializeField] private float _jumpHeight = 2;
    //[SerializeField] private float _runAttackDash = 5;

    //private bool _alreadyLanded = true;

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private Vector2 _sensorSize = new Vector2(0.5f, 0.5f);

    [SerializeField] private Transform _interatcPosition;
    [SerializeField] private Vector2 _interactionZone = new Vector2(1, 1);

    [SerializeField] private Transform _attackPosition;
    [SerializeField] private Vector2 _attackHitboxSize = new Vector2(1, 1);

    //----- Vida -------
    [SerializeField] private float _maxHealth = 10;
    [SerializeField] private float _currentHealth;

    //Daño
    [SerializeField] private float _playerDamage = 1;
    [SerializeField] private float _damageMultiplier = 3;
    [SerializeField] private bool _isRunAttacking = false;
    [SerializeField] private bool _isIdleAttacking = false;
    

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        //--> para bindear el input a la variable
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _attackAction = InputSystem.actions["Attack"];
        _interactAction = InputSystem.actions["Interact"];
        //_jumpAction = InputSystem.actions.FindAction("jump"); --> otra manera de bindear el input a la variable

        //             ---Ground sensor antiguo---
        //_groundSensor = GetComponentInChildren<GroundSensor>();
    }

    void Start()
    {
        gameObject.transform.position = _playerSpawn.transform.position;
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        if (_attackAction.WasPressedThisFrame() && !_isRunAttacking)
        {
            _isIdleAttacking = true;
            StartCoroutine(InputCooldown());
            //Debug.Log("Ataque");
            _animator.SetTrigger("hasAttacked");
            
            //Attack();
            //Para hacer que el ataque desactive los inputs del jugador, booleana de control que diga si está atacando o no. al llamar la función es true, se inicia una corrutina que tiene quita los inputs, y solo los activa cuando la booleana devuelve false (cuando acaba de atacar)
        }
        
        if (_attackAction.WasPressedThisFrame() && _isRunAttacking)
        {
            //Debug.Log("Ataque corriendo");
            _animator.SetTrigger("hasAttacked");
        }

        if(_isIdleAttacking)
        {
            return;
        }
        /*if (_playerHealth == 0)
        {
            Destroy(gameObject);
        }*/

        //--> asignamos al vector de movimiento el valor del input (_moveAction)
        _moveInput = _moveAction.ReadValue<Vector2>();

        //--> debug para ver los valores del input
        //Debug.Log(_moveInput);

        //--> al transform del personaje le sumamos un un vector que equivale a el componente x del vector de movimiento * velocidad del jugador * Time.deltatime
        // Time.deltatime --> el intervalo en segundos entre el último frame hasta el actual, de esta manera la velocidad no varía dependiendo de los fps a los que se ejecuta el juego.

        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerSpeed * Time.deltaTime;

        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        //WasPressedThisFrame, hace que nada mas pulsar el botón se cumpla esta condición.
        {
            Jump();
        }

        if (_interactAction.WasPressedThisFrame())
        {
            Interact();
        }

        //función que controla cosas como rotación, animaciones, etc del movimiento
        Movement();

        _animator.SetBool("IsJumping", !IsGrounded());
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_moveInput.x * _playerSpeed, _rigidbody.linearVelocity.y);
    }

    void Movement()
    {

        if (_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsRunning", true);
            _isRunAttacking = true;
        }

        else if (_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("IsRunning", true);
            _isRunAttacking = true;
        }

        else
        {
            _animator.SetBool("IsRunning", false);
            _isRunAttacking = false;
        }
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector2.up * Mathf.Sqrt(_jumpHeight * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);
        //--> formula del mathf =(altura de salto * -2 * gravedad)
        //--> forma sencilla = _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse); siendo jump force la fuerza de salto designada (variable)

    }

    void Interact()
    {
        //Debug.Log("Haciendo cosas");
        Collider2D[] interactables = Physics2D.OverlapBoxAll(transform.position, _interactionZone, 0);
        foreach (Collider2D item in interactables)
        {
            if (item.gameObject.tag == "Star")
            {
                Star starScript = item.gameObject.GetComponent<Star>();

                //para comprobar que se ha accedido correctamente (que la variable sea diferente de nulo) y evitar que en el caso que no, no se ejecuten las lineas posteriores
                if (starScript != null)
                {
                    starScript.StarInteraction();
                }
            }
            if (item.gameObject.tag == "Coin")
            {
                Coin coinScript = item.gameObject.GetComponent<Coin>();
            
                if (coinScript != null)
                {
                    coinScript.CoinInteraction();
                }
            }
        }
    }

    void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(_attackPosition.position, _attackHitboxSize, 0);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                Enemy _enemyScript = enemy.gameObject.GetComponent<Enemy>();
                _enemyScript.EnemyTakeDamage(_playerDamage);

                if (_isRunAttacking)
                {
                    _enemyScript.EnemyTakeDamage(_playerDamage * _damageMultiplier);
                }
            }
        }
    }
    
    IEnumerator InputCooldown(float cooldown = 0.25f)
    {
        yield return new WaitForSeconds(cooldown);
        _isIdleAttacking = false;
    }

    //ground sensor pero BIEN
    bool IsGrounded()
    {
        //el valor devuelve un array de objetos así que la variable debe ser de tipo array
        Collider2D[] ground = Physics2D.OverlapBoxAll(_sensorPosition.position, _sensorSize, 0);
        //el array almacena los objetos que entren en este collider

        foreach (Collider2D item in ground)
        //item es nombre de variable, puede ser cualquiera
        {
            if (item.gameObject.layer == 3)
            {
                return true;
            }
        }
        return false;
    }

    //para dibujar el collider que creamos en IsGrounded
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_interatcPosition.position, _interactionZone);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_attackPosition.position, _attackHitboxSize);
    }



    //_____________Por hacer 2º entrega, animacion de ataque quieto y ataque en movimiento, para el ataque estático hacer que no pueda moverse.________________________________
    //Interacciones con el enemigo

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Mimik _mimikScript = FindWithTag("Enemy");
            _playerHealth -= 
        }
    }*/

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        float health = _currentHealth / _maxHealth;
        //Debug.Log(health);

        GUIManager.Instance.UpdateHealthBar(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GameManager.instance.playerInputs.FindActionMap("Player");
        _animator.SetTrigger("IsDead");
        Debug.Log("Muerto");
    }
}
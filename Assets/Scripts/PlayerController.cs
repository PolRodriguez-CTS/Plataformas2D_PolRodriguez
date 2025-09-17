using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //--> variables de componentes
    Collider2D _collider2D;
    Rigidbody2D _rigidbody;

    //--> variables de input
    //--> botones
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    //--> vector de movimiento
    private Vector2 _moveInput;
    [SerializeField] private float _playerSpeed = 4.5f;
    [SerializeField] private float _jumpForce = 5;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        //--> para bindear el input a la variable
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _attackAction = InputSystem.actions["Attack"];

        //_jumpAction = InputSystem.actions.FindAction("jump"); --> otra manera de bindear el input a la variable
    }

    void Start()
    {}

    void Update()
    {
        //--> asignamos al vector de movimiento el valor del input (_moveAction)
        _moveInput = _moveAction.ReadValue<Vector2>();
        
        //--> debug para ver los valores del input
        Debug.Log(_moveInput);

        //--> al transform del personaje le sumamos un un vector que equivale a el componente x del vector de movimiento * velocidad del jugador * Time.deltatime
        // Time.deltatime --> el intervalo en segundos entre el último frame hasta el actual, de esta manera la velocidad no varía dependiendo de los fps a los que se ejecuta el juego.

        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerSpeed * Time.deltaTime;


        if (_jumpAction.WasPressedThisFrame())
        //WasPressedThisFrame, hace que nada mas pulsar el botón se cumpla esta condición.
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_moveInput.x * _playerSpeed, _rigidbody.linearVelocity.y);
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject gameOverScreen;
    public Image healthBarImage1; 

    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityMultiplier;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask whatIsWalkable;

    float _inputX;
    float _inputZ;
    float _gravityY;
    float _velocityY;
    bool _isJumpPressed;
    bool _isGrounded;

    CharacterController _characterController;
    Camera _mainCamera;
    Vector3 _direction;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
        _gravityY = Physics.gravity.y;
    }

    private void Start()
    {
        _isGrounded = IsGrounded();
        if (!_isGrounded)
        {
            StartCoroutine(WaitForGroundedCoroutine());
        }

        // Ocultar el botón de reinicio y la pantalla de Game Over al inicio del juego
        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    private void Update()
    {
        HandleGravity();
        HandleMovement();

        // Si el jugador está en el aire y se cae fuera de los límites de la pantalla
        if (!_isGrounded && transform.position.y < -10f)
        {
            Die();
        }
    }

    private void HandleGravity()
    {
        if (_isGrounded)
        {
            if (_velocityY < -1.0F)
            {
                _velocityY = -1.0F;
            }

            HandleJump();
            if (_isJumpPressed)
            {
                Jump();
            }
        }
        else
        {
            _velocityY += _gravityY * gravityMultiplier * Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputZ = Input.GetAxisRaw("Vertical");
    }

    private void HandleJump()
    {
        _isJumpPressed = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private bool IsMove()
    {
        return (_inputX != 0.0F || _inputZ != 0.0F);
    }

    private void Move()
    {
        _direction.y = _velocityY;
        _characterController.Move(_direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        if (!IsMove())
        {
            _direction = Vector3.zero;
            return;
        }

        _direction = Quaternion.Euler(0.0F, _mainCamera.transform.eulerAngles.y, 0.0F) * new Vector3(_inputX, 0.0F, _inputZ);
        Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _velocityY = jumpForce;
        _isGrounded = false;
        StartCoroutine(WaitForGroundedCoroutine());
    }

    private bool IsGrounded()
    {
        return _characterController.isGrounded;
    }

    private IEnumerator WaitForGroundedCoroutine()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(() => IsGrounded());
        _isGrounded = true;
    }

    // Método para manejar la muerte del jugador
    private void Die()
    {
        Debug.Log("Player has died.");

        // Detener el tiempo
        Time.timeScale = 0f;

        // Mostrar el Game Over
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Mostrar el botón de reinicio
        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }
    }

    // Método para reiniciar la partida
    public void RestartGame()
    {
        // Reanudar el tiempo
        Time.timeScale = 1f;

        // Colocar al jugador de nuevo en la posición inicial
        transform.position = Vector3.zero;

        // Desactivar el botón de reinicio y la pantalla de Game Over
        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }
}

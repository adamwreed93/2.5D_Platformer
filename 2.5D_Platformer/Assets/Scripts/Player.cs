using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private UIManager _uiManager;
    private CharacterController _controller;

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight = 15.0f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private int _coins;

    private Vector3 _wallSurfaceNormal;
    private Vector3 _direction, _velocity;
    private bool _canWallJump;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    private float _pushPower = 2f;
    private bool _wonGame;

    private bool _isTouchingWall;


    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL."); 
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (_isTouchingWall == false)
        {
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;
        }

        if (_controller.isGrounded == true)
        {
            if (_isTouchingWall == true)
            {
                _direction = new Vector3(horizontalInput, 0, 0);
                _velocity = _direction * _speed;
            }

            _canWallJump = false;
            _isTouchingWall = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false)
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity = _jumpHeight;
                    _canDoubleJump = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == true)
            {
                _yVelocity = _jumpHeight;
                _velocity = _wallSurfaceNormal * _speed;
            }
            
        }
        
        _velocity.y = _yVelocity;
        //Debug.Log(_velocity * Time.deltaTime);
        _controller.Move(_velocity * Time.deltaTime);
        

        if (_wonGame == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_controller.isGrounded == false)
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Moving Box")
        {
            Rigidbody box = hit.collider.GetComponent<Rigidbody>();

            if (box != null)
            {
                Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, 0);

                box.velocity = pushDirection * _pushPower;
            }
        }

        if (_controller.isGrounded == false && hit.transform.tag == "Wall")
        {
            _isTouchingWall = true;

            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;
        }
    }

    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public int CoinCount()
    {
        return _coins;
    }

    public void WinGame()
    {
        _wonGame = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _jumpHight = 15.0f;
    private float _yVelocity;
    private bool _canDoubleJump;
    [SerializeField] private int _coins;
    private UIManager _uiManager;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.Log("UI Manager is NULL!");
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;  
    
        if (_controller.isGrounded)
        {
            _canDoubleJump = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _yVelocity = _jumpHight;
                _canDoubleJump = false;
            }
            _yVelocity -= _gravity;
        }
        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    public void AddCoins(int ID)
    {
        switch (ID)
        {
            case 0:
                _coins += 1;
                break;
            case 1:
                _coins += 5;
                break;
            case 2:
                _coins += 10;
                break;
        }
        _uiManager.UpdateCoinDisplay(_coins);
    }
}

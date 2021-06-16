﻿using System.Collections;
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

    void Start()
    {
        _controller = GetComponent<CharacterController>();
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
}
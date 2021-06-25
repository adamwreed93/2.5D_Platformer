using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform _origin, _target;
    [SerializeField] private float _speed;

    private bool _goingDown = false;


    public void CallElevator()
    {
        _goingDown = !_goingDown;
    }

    private void FixedUpdate()
    {
        if (_goingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
        else if (_goingDown == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _origin.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed = 1.0f;

    private bool _switching = false;
    private Transform _target;
    
    void FixedUpdate()
    {
        if (_switching == false)
        {
            _target = _pointB;
        }
        else if (_switching == true)
        {
            _target = _pointA;
        }

        if (transform.position == _pointB.position)
        {
            _switching = true;
        }
        else if(transform.position == _pointA.position)
        {
            _switching = false;
        }
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
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

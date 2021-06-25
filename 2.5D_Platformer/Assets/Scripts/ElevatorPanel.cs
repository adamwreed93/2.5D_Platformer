using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    private Player _player;
    private Elevator _elevator;

    [SerializeField] private MeshRenderer _callButton;
    [SerializeField] private int _costToRideElevator = 10;
    [SerializeField] private GameObject _yourBrokeText;
    [SerializeField] private GameObject _youGotCoinsText;

    private bool _elevatorCalled = false;
    private bool _canCallElevator = false;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();

        if (_elevator == null)
        {
            Debug.LogError("Elevator is NULL!");
        }

        if (_player == null)
        {
            Debug.LogError("Player is NULL!");
        }
    }

    private void Update()
    {
        ElevatorControl();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _canCallElevator = true;

            if (_player.CoinCount() < _costToRideElevator)
            {
                _yourBrokeText.SetActive(true);
            }
            else if (_player.CoinCount() >= _costToRideElevator)
            {
                _youGotCoinsText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _canCallElevator = false;
            _yourBrokeText.SetActive(false);
            _youGotCoinsText.SetActive(false);
        }
    }

    private void ElevatorControl()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canCallElevator && _player.CoinCount() >= _costToRideElevator)
        {
            if (_elevatorCalled == true)
            {
                _callButton.material.color = Color.red;
                _elevatorCalled = false;
            }
            else
            {
                _callButton.material.color = Color.green;
                _elevatorCalled = true;
            }
            _elevator.CallElevator();
        }
    }
}




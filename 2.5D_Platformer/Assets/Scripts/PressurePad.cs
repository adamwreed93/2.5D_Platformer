using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private Player player;

    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _restartText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Moving Box")
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);

            if (distance < 0.05)
            {
                Rigidbody box = other.GetComponent<Rigidbody>();
                if (box != null)
                {
                    box.isKinematic = true;
                }
                MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();

                if (renderer != null)
                {
                    renderer.material.color = Color.blue;
                }

                _winText.SetActive(true);
                _restartText.SetActive(true);
                player.WinGame();
                Destroy(this);
            }
        }
    }
}

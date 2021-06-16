using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private int _coinID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                switch (_coinID)
                {
                    case 0:
                        player.AddCoins(0);
                        break;
                    case 1:
                        player.AddCoins(1);
                        break;
                    case 2:
                        player.AddCoins(2);
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}

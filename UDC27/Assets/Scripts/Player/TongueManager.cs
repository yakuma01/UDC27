using System;
using Enemy;
using UnityEngine;

namespace Player
{
    public class TongueManager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bees"))
            {
                var commonBee = other.GetComponent<CommonBee>();
                var queenBee = other.GetComponent<QueenBee>();
                if (commonBee != null)
                {
                    commonBee.HitBee();
                }
                else if (queenBee != null)
                {
                    queenBee.HitBee();
                }
            }
        }
        
        
        
    }
}
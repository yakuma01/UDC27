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
                Debug.Log("calling for: " + other.name);
                other.GetComponent<CommonBee>().HitBee();
            }
        }
        
        
        
    }
}
using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Player
{
    public class CustomMovement : MonoBehaviour
    {
        public float moveSpeed = 2.0f;
        private bool _takeInput;
        public GameObject tongue;
        public RayCaster rayCaster;

        private void Start()
        {
            _takeInput = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                tongue.SetActive(true);
                var nullCheck = new Vector3(420, 420, 420);
                var hitpos = rayCaster.GetRayCastPoint(nullCheck);
                if (hitpos != nullCheck)
                {

                    hitpos.y = 0.01f;
                    var tp = tongue.transform.position;
                    Vector2 Point_1 = new Vector2(tp.x,tp.z);
                    Vector2 Point_2 = new Vector2(hitpos.x,hitpos.z);
                    float mappedAngle = Mathf.Atan2(Point_2.y - Point_1.y , Point_2.x-Point_1.x) * Mathf.Rad2Deg;
                    var rotationVector = new Vector3(90, 0, mappedAngle);
                    tongue.transform.rotation = Quaternion.Euler(rotationVector); 
                    
                }
                
            }
            
            if (!_takeInput)
            {
                return;
            }
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(MoveCharacter(Vector3.forward));
                _takeInput = false;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(MoveCharacter(Vector3.back));
                _takeInput = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(MoveCharacter(Vector3.left));
                _takeInput = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(MoveCharacter(Vector3.right));
                _takeInput = false;
            }

            
        }

        IEnumerator MoveCharacter(Vector3 direction, float moveDuration = 1f)
        {
            float startTime = Time.time;
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = startPosition + direction * 2;

            while (Time.time - startTime < moveDuration)
            {
                float t = (Time.time - startTime) / moveDuration;
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            transform.position = targetPosition;
            _takeInput = true;
        }
    }
}
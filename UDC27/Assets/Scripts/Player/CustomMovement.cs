using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Player
{
    public class CustomMovement : MonoBehaviour
    {
        private float moveSpeed = 3.0f;
        private bool _takeInput;
        public GameObject tongue;
        public RayCaster rayCaster;

        private bool facingRight = true;
        private float _inputBlockTime = .5f;
        
        private Animator frogAnimator;

        private Vector3 colliderEnterPosition;
        private bool canMove = true;
        

        private void Start()
        {
            _takeInput = true;
            frogAnimator = GetComponent<Animator>();
        }

        private IEnumerator ShootTongue()
        {

            var nullCheck = new Vector3(420, 420, 420);
            var hitpos = rayCaster.GetRayCastPoint(nullCheck);

            if (hitpos == nullCheck)
            {
                yield break;
            }

            hitpos.y = 0.01f;
            var tp = tongue.transform.position;
            Vector2 Point_1 = new Vector2(tp.x, tp.z);
            Vector2 Point_2 = new Vector2(hitpos.x, hitpos.z);
            float mappedAngle = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * Mathf.Rad2Deg;
            
            Debug.Log(mappedAngle);
            
            if (facingRight)
            {

                if (mappedAngle > 60)
                {
                    mappedAngle = 60;
                }
                else if (mappedAngle < -60)
                {
                    mappedAngle = -60;
                }
                
            }
            else
            {
                /*if (mappedAngle < 120)
                {
                    mappedAngle = 120;
                }
                else if (mappedAngle > -120 )
                {
                    mappedAngle = -120;
                }*/
                
                //Todo: Handle tongue on left side
                
            }
            
            var rotationVector = new Vector3(90, 0, mappedAngle);

            
            frogAnimator.SetTrigger("IsAttack");
            
            yield return new WaitForSeconds(.3f);
            
            tongue.SetActive(true);
            tongue.transform.rotation = Quaternion.Euler(rotationVector);
            
            yield return new WaitForSeconds(.3f);
            
            tongue.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ShootTongue());
                StartCoroutine(BlockInput());
            }
            
            if (!_takeInput)
            {
                return;
            }

            
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(MoveCharacter(Vector3.forward));
                StartCoroutine(BlockInput());
            }
            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(MoveCharacter(Vector3.back));
                StartCoroutine(BlockInput());
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (facingRight)
                {
                    FaceLeft();
                }
                StartCoroutine(MoveCharacter(Vector3.left));
                StartCoroutine(BlockInput());
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (!facingRight)
                {
                    FaceRight();
                }
                StartCoroutine(MoveCharacter(Vector3.right));
                StartCoroutine(BlockInput());
            }

            
        }

        private void FaceLeft()
        {
            var offset = -1;
            transform.position -= Vector3.left*offset;
            
            var dir = new Vector3(-90, -90, -90);
            transform.rotation = Quaternion.Euler(dir);
            facingRight = false;
            //transform.
        }
        private void FaceRight()
        {
            
            var offset = 1;
            transform.position -= Vector3.left*offset;
            
            var dir = new Vector3(90, 0, 0);
            transform.rotation = Quaternion.Euler(dir);
            facingRight = true;
            //transform.
        }

        IEnumerator  MoveCharacter(Vector3 direction, float moveDuration = .5f)
        {
            float startTime = Time.time;
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = startPosition + direction * moveSpeed;

            frogAnimator.SetTrigger("IsHoping");

            yield return new WaitForSeconds(.15f);
            
            while (Time.time - startTime < moveDuration)
            {
                float t = (Time.time - startTime) / moveDuration;
                if (canMove)
                {
                    transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                }
                else
                {
                    yield break; 
                }
                yield return null;
            }

            transform.position = targetPosition;
            _takeInput = true;
        }
        
        private IEnumerator BlockInput()
        {
            _takeInput = false;
            yield return new WaitForSecondsRealtime(_inputBlockTime);
            
            _takeInput = true;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Obstacles"))
            {
                colliderEnterPosition = transform.position;
            }
            
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.transform.CompareTag("Obstacles"))
            {
                canMove = false;
                var dir = (transform.position - colliderEnterPosition).normalized;
                transform.position = colliderEnterPosition - dir*1f;
                Debug.Log("Still Colliding");
            }
            
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.transform.CompareTag("Obstacles"))
            {
                canMove = true;
            }
            
        }
    }
}
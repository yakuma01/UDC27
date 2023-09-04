using System.Collections;
using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {

        private bool _takeInput;
        public float step = 1f;

        private Animator _animation;
        // Start is called before the first frame update
        void Start()
        {
            _takeInput = true;
            _animation = GetComponent<Animator>();
            _animation.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_takeInput)
            {
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _animation.enabled = true;
                Debug.Log("clicked");
                _animation.Play("FrogMovement");
                Debug.Log("played");
                StartCoroutine(BlockInput());
            }
            if (Input.GetKey(KeyCode.A)){
                transform.position += Vector3.left * step;
                StartCoroutine(BlockInput());
            }
            if (Input.GetKey(KeyCode.W)){
                transform.position += Vector3.forward * step ;
                StartCoroutine(BlockInput());
            }
            if (Input.GetKey(KeyCode.S)){
                transform.position += Vector3.back * step;
                StartCoroutine(BlockInput());
            }
            if (Input.GetMouseButtonDown(0)){
                StartCoroutine(ShootTongue());
            }
        }
        bool AnimatorIsPlaying(){
            return _animation.GetCurrentAnimatorStateInfo(0).length >
                   _animation.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        private IEnumerator ShootTongue()
        {
            // todo: implement this after a tongue is there
            yield return null;
        }

        private IEnumerator BlockInput(float time = 1f)
        {
            _takeInput = false;
            yield return new WaitForSecondsRealtime(time);
            while (AnimatorIsPlaying())
            {
                yield return null;
            }

            _animation.enabled = false;
            _takeInput = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private bool _takeInput;
    public float step = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _takeInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_takeInput)
        {
            return;
        }
        if (Input.GetKey(KeyCode.D)){
            transform.position += Vector3.right * step;
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

    private IEnumerator ShootTongue()
    {
        // todo: implement this after a tongue is there
        yield return null;
    }

    private IEnumerator BlockInput(float time = 1f)
    {
        _takeInput = false;
        yield return new WaitForSecondsRealtime(time);
        _takeInput = true;
    }
}

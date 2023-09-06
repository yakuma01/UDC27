using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timeLeft = 10.0f;
    public TextMeshProUGUI startText; // used for showing countdown from 3, 2, 1 


    private void Update()
    {
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft <= 0)
        {
            Debug.Log("Time is over");  
        }
    }
}
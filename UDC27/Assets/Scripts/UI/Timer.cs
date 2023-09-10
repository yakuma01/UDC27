using Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        public float timeLeft = 0f;
        public TextMeshProUGUI startText; // used for showing countdown from 3, 2, 1 
    

        private void Update()
        {
            startText.text = (timeLeft).ToString("0");
            timeLeft += Time.deltaTime;
        }
        public void PauseTimer()
        {
            Time.timeScale = 0;
        }

        public void ResumeTimer()
        {
            Time.timeScale = 1;
        }
    }
}
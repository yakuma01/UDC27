using TMPro;
using UnityEngine;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        private float timeLeft = 60.0f;
        private bool isGameOver = false;
        public TextMeshProUGUI startText; // used for showing countdown from 3, 2, 1 
    

        private void Update()
        {
            startText.text = (timeLeft).ToString("0");
            if (timeLeft <= 0)
            {
                if (!isGameOver)
                {
                    Debug.Log("Time is over");
                    isGameOver = true;
                    GameOver();
                }
            }
            else
            {
                timeLeft -= Time.deltaTime;
            }
        }

        private void GameOver()
        {
            PanelManager.Instance.HidePanel("TimerPanel");
            PanelManager.Instance.ShowPanel("LevelTransitionPanel", PanelShowBehaviour.KEEP_PREVIOUS);
        }
    }
}
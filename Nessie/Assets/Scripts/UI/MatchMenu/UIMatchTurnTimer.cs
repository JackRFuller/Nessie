using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMatchTurnTimer : MonoBehaviour
{
    private TurnManager turnManager;
    private TMP_Text turnTimerText;

    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameManager.Instance.TurnManager;
        turnTimerText = GetComponent<TMP_Text>();
        turnTimerText.enabled = false;
        
        turnManager.NewTurnStarted += ShowTurnTimer;
    }

    private void ShowTurnTimer()
    {
        turnTimerText.enabled = true;
        this.enabled = true;
    }

    private void Update()
    {
        UpdateTurnTimer();
    }

    private void UpdateTurnTimer()
    {
        float timer = turnManager.TurnTimer;

        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");

        string timerText = string.Format("{0}:{1}",minutes,seconds);
        turnTimerText.text = timerText;
    }

    
}

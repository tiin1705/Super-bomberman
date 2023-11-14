using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI timerText;
  [SerializeField] float remainingTime; 
  public GameObject TextPushStart;  
  public GameObject TextGameOver;   
 

  void Update()
  {
    if(remainingTime > 0)
    {
        remainingTime -= Time.deltaTime;
    }
    if (remainingTime<0)
    {
        remainingTime = 0;
        // GaneOver
        timerText.color=Color.red;
    }
    else if(remainingTime<10)
    {
        TextPushStart.SetActive(false); 
        TextGameOver.SetActive(true); 
    }
    
    int minutes = Mathf.FloorToInt(remainingTime / 60);
    int seconds = Mathf.RoundToInt(remainingTime % 60);
    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
  }
}

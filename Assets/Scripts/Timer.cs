using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI timerText;
  [SerializeField] float remainingTime; 
  public GameObject player1;  
  public GameObject player2;  
  public GameObject player3;  
  public string SceneName;
  void Update()
  {
    if(remainingTime > 0)
    {
        remainingTime -= Time.deltaTime;
    }
    if (remainingTime < 0)
    {
        remainingTime = 0;
        // GaneOver
        timerText.color=Color.red;
        SceneManager.LoadScene(SceneName);
    }
    else if(!player1.activeSelf && !player2.activeSelf && !player3.activeSelf)
    {
      SceneManager.LoadScene(SceneName);
    }
    else if(remainingTime<10)
    {
        timerText.color=Color.red;
    }
    
    int minutes = Mathf.FloorToInt(remainingTime / 60);
    int seconds = Mathf.RoundToInt(remainingTime % 60);
    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
  }
}

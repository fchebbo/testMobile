using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RingDodgerLevelScript : MonoBehaviour
{
    public Text timeText;
    private float timeRemaining = 45f;
    private float timeRemainingTotal = 45f;
    public GameObject poof;
    public AudioSource swooshSource;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = timeRemainingTotal;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanvasMaster.instance.getGameOver() && timeRemaining >0 && !CanvasMaster.instance.getVictory()) {
            timeRemaining -= Time.deltaTime;
            timeText.text = "Time Remaining: " + timeRemaining.ToString("0.0");
        }
    
        if (timeRemaining<=0 && !CanvasMaster.instance.isGameOver && !CanvasMaster.instance.isVictory)
        {
           timeText.text = "Time Remaining: <color=green> 0.0 </color>";
           CanvasMaster.instance.stopAllMusic();
           GameObject[] ringOfDeaths = GameObject.FindGameObjectsWithTag("RingOfDeath");
           foreach (GameObject RingOfDeath in ringOfDeaths)
            {
                Instantiate(poof, RingOfDeath.transform.position, Quaternion.identity);
                RingOfDeath.SetActive(false);
            }
           swooshSource.Play();
           CanvasMaster.instance.doVictory();
        }
    }


}

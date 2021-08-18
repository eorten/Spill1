using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BeaconScript : MonoBehaviour
{
    [SerializeField]
    private Slider deathTimerSlider;
    [SerializeField]
    private TextMeshProUGUI beaconPosTxt;
    [SerializeField]
    private TextMeshProUGUI scoreTxt;
    [SerializeField]
    private TextMeshProUGUI scoreMultiplierTxt;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI difficultyText;
    private int time = 0;
    private int difficultyMultiplier = 10;

    private int score = 0;
    private int scoreMultiplier = 1;

    private int heightOverGround = 0;

    private int minDistance = 1000; //200
    private int maxDistance = 1500; //700

    private float timeToDeath;
    private float maxTimeToDeath = 60f;

    // Start is called before the first frame update
    void Start()
    {
        deathTimerSlider.maxValue = maxTimeToDeath;
        timeToDeath = maxTimeToDeath;
        InvokeRepeating("IncreaseTime", 2, 1);
        InvokeRepeating("IncreaseDifficulty", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        timeToDeath -= Time.deltaTime;
        deathTimerSlider.value = timeToDeath;
        if (timeToDeath<0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //IF HIT PLAYER
        {
            Reposition();
            scoreMultiplier += (int)(maxTimeToDeath)/10;
            score += ((scoreMultiplier/10) + 1) * 100;
            timeToDeath = maxTimeToDeath - (6*(difficultyMultiplier / 10)); //3 sek mindre for hver gang den går opp
            deathTimerSlider.maxValue = maxTimeToDeath;
            scoreMultiplierTxt.text = "Multiplier: " + scoreMultiplier;
            scoreTxt.text = "Score: "+score;
        }
    }
    void Reposition()
    {

        Vector3 newPos = new Vector3(transform.position.x+RandomNumber(), heightOverGround, transform.position.z + RandomNumber());
        CheckIfEmpty(newPos);
        
    }
    private void IncreaseDifficulty()
    {
        difficultyMultiplier += 1;
        difficultyText.text = "Difficulty: " + (float)difficultyMultiplier/10 + "x";
    }
    private void CheckIfEmpty(Vector3 pos)
    {
        float radius = 2f;
        float distance = 1f;
        gameObject.transform.position = pos;
        /*
        RaycastHit hit;
        if (!(Physics.SphereCast(pos, radius, transform.forward, out hit, distance)))
        {
            gameObject.transform.position = pos;
        }
        else
        {
            RaycastHit hit2;
            if (!(Physics.SphereCast(pos + new Vector3(0,10,0), radius, transform.forward, out hit2, distance)))
            {
                gameObject.transform.position = pos;
            }
            else
            {
                RaycastHit hit3;
                if (!(Physics.SphereCast(pos + new Vector3(0, 10, 0), radius, transform.forward, out hit3, distance)))
                {
                    gameObject.transform.position = pos;
                }
                else
                {
                    Reposition();
                }
            }
        }*/
        SetPos();
    }

    private void IncreaseTime()
    {
        time++;
        int min = (int)(time / 60);
        int sec = time - (60 * min);
        if (sec < 10)
        {
            timerText.text = min + " : " + "0" + sec;
        }
        else
        {
            timerText.text = min + " : " + sec;
        }

    }
    private void SetPos()
    {
        beaconPosTxt.text = "Beacon pos: " + "\n" + "[" + transform.position.x + "/" + transform.position.z + "]";
    }
    private int RandomNumber()
    {
        if (Random.value > 0.5)
        {
            int num = Random.Range(minDistance, maxDistance);
            return num;
        }
        else
        {
            int num = Random.Range(-minDistance, -maxDistance);
            return num;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartNewWave : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownTimer;
    private bool isCoroutineRunning = true;
    Coroutine coroutine = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnemySpawner.Instance.goToGreenZone.gameObject.SetActive(false);
            coroutine = StartCoroutine(WaitForNewWave(3));
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            StopCoroutine(coroutine);
            countDownTimer.gameObject.SetActive(false);
            EnemySpawner.Instance.goToGreenZone.gameObject.SetActive(true);
        }
        
        
    }
    private IEnumerator WaitForNewWave(int secondsAmount)
    {
        countDownTimer.gameObject.SetActive(true);
        int timeLeft = secondsAmount;
        countDownTimer.text = timeLeft.ToString();
        for (int i = 0; i < secondsAmount; i++)
        {   
                yield return new WaitForSeconds(1f);
                timeLeft--;
                countDownTimer.text = timeLeft.ToString();
    
        }
        countDownTimer.gameObject.SetActive(false);
        
        EnemySpawner.Instance.StartNewWave();
       
    }

    
}

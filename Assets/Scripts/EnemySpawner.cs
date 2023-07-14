using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;


public class EnemySpawner : MonoBehaviour 
{
    static public EnemySpawner Instance { get; private set; }
    private List<GameObject> enemySpawnPositions;
    public GameObject[] zombieTypes;
    public int wave { get; private set; } = 1;
    public int enemiesAmount = 0;
    [SerializeField] GameObject greenZone;
    [SerializeField] TextMeshProUGUI waveFinishedtext;
    [SerializeField] TextMeshProUGUI startedNewWave;
    public TextMeshProUGUI goToGreenZone;
    [SerializeField] GameObject shop;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawnPositions = new List<GameObject>();
        foreach(Transform spawner in transform)
        {
            enemySpawnPositions.Add(spawner.gameObject);
        }
        StartCoroutine(WaitToSetAuthority());
    }
    IEnumerator WaitToSetAuthority()
    {
        yield return new WaitForSeconds(2);
          StartNewWave();
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
        public async void StartNewWave()
    {
        StartCoroutine(WaitForNewWaveText());
        greenZone.SetActive(false);
        enemiesAmount = 0;
        GameObject currentZombie = null;
        
        for (int i = 0; i < wave * 3; i++)
        {
            int zombieType = Random.Range(0, zombieTypes.Length);
            currentZombie = Instantiate(zombieTypes[zombieType]);
            Debug.LogError(zombieType);
            //Transform spawningPosition = ;
            GameObject enemySpawned = currentZombie;
            Transform newEnemyPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)].transform;
             
            enemySpawned.transform.position = newEnemyPosition.TransformPoint(new Vector3(0,0,0));
            
            enemySpawned.GetComponent<CharacterController>().enabled = true;
            enemiesAmount++;
        }
        wave++;
    }
    public void WaveDefeated()
    {
        StartCoroutine(WaitForWaveDefeatedText());
        greenZone.SetActive(true);
    }
   public IEnumerator WaitForNewWaveText()
    {
        shop.SetActive(false);
        startedNewWave.text = $"WAVE {wave} BEGINS";
        startedNewWave.gameObject.SetActive(true);
        
        Debug.Log("0");
        yield return new WaitForSeconds(3f);
        Debug.Log("1");

        startedNewWave.gameObject.SetActive(false);
    }
    private IEnumerator WaitForSpawn(GameObject enemy)
    {
        yield return new WaitForSeconds(1f);
        enemy.transform.localPosition = new Vector3(0, 0, 0);
    }

    private IEnumerator WaitForWaveDefeatedText()
    {
        shop.SetActive(true);
        goToGreenZone.gameObject.SetActive(true);
        waveFinishedtext.text = $"YOU SURVIVED WAVE {wave - 1}";
        waveFinishedtext.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        waveFinishedtext.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

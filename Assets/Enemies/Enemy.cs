
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;


public class Enemy : MonoBehaviour
{
    public int health = 3;
    private int previousHealth;

    public Rigidbody[] rbs;
    public Animator anim;
    private bool isAlive = true;
    Transform player;
    [SerializeField] private float hittingRange;
    private float gravity = -9.8f;
    private Vector3 playerVelocity;
    [SerializeField] GameObject deathEffectPrefab;
    [SerializeField] GameObject deathEffectSmoke;
    public bool isAttacking;
    [SerializeField] CharacterController enemyController;
    [SerializeField] NavMeshAgent navMeshAgent;

    public UnityEvent onHealthChange;
    // Start is called before the first frame update
    void Start()
    {
        previousHealth = health;
        player = CharController_Motor.Instance.transform;
        StartCoroutine(RefindPlayer());
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (onHealthChange == null)
            onHealthChange = new UnityEvent();
    }

    

    private IEnumerator RefindPlayer()
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(RefindPlayer());
        player = CharController_Motor.Instance.transform;

    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {

            navMeshAgent.isStopped = true;
            return;
        }
        if (enemyController.isGrounded)
        {
            if (playerVelocity.y < -0.5f)
            {
                playerVelocity.y = -0.5f;
            }

        }
        playerVelocity.y += gravity * Time.deltaTime;
        Vector3 movement = new Vector3(0, playerVelocity.y, 0);
        enemyController.Move(movement * Time.deltaTime);

        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

        float currentDistance = Vector3.Distance(player.transform.position, transform.position);
        if (currentDistance > hittingRange)
        {
          
            anim.SetBool("isStaying", false);
        }
        else{
            anim.SetBool("isStaying", true);
        }
        
        navMeshAgent.destination = player.transform.position;


        if (currentDistance > hittingRange)
        {
           
        }
        else
        {
           
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("ZombieAttack") == false)
            {
                StartCoroutine(WaitForHit());
                anim.SetTrigger("EnterAttack");
            }
            
        }
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("ZombieAttack") == false)
        {
            isAttacking = false;
        }
    }

    public void DecreaseEnemyHealth(int damageAmount = 1)
    {
        health -= damageAmount;
        DestroyEnemyIfDead();
    }
    private void DestroyEnemyIfDead()
    {
        if (health <= 0)
        {
            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = false;
            }
            if (isAlive)
            {
                CharController_Motor.Instance.ChangeMoneyAmount(Random.Range(2, 10));
            }
            isAlive = false;
            anim.enabled = false;
            StartCoroutine(WaitForDestroy());
            
        }
    }
    private void CheckIfEnemiesLeft()
    {
        Debug.LogError("enemy amount:" + EnemySpawner.Instance.enemiesAmount);
        EnemySpawner.Instance.enemiesAmount--;
        if(EnemySpawner.Instance.enemiesAmount <= 0)
        {
            EnemySpawner.Instance.WaveDefeated();
        }
    }
    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(4);
        CheckIfEnemiesLeft();
        GameObject effect = Instantiate(deathEffectPrefab);
        effect.transform.position = transform.position;
        GameObject smoke = Instantiate(deathEffectSmoke);
        smoke.transform.position = transform.position;
        Destroy(gameObject);
    }
    private IEnumerator WaitForHit()
    {
        yield return new WaitForSeconds(0.3f);
        isAttacking = true;
    }
}

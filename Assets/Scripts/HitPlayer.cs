using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public Enemy enemy;
    public int damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            
            if (enemy.isAttacking)
            {
                if (enemy.anim.GetCurrentAnimatorStateInfo(1).IsName("ZombieAttack"))
                {
                    other.GetComponent<CharController_Motor>().DecreaseHealth(damage);
                    enemy.isAttacking = false;
                    CameraShake.Shake(0.25f, 0.2f);
                    Debug.Log("Hitting");
                }
            }
        }
    }
}

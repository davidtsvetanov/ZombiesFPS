using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.collider.transform.root.tag == "Enemy")
        {
            Debug.Log("udrq li");
            collision.collider.transform.GetComponentInParent<Enemy>().DecreaseEnemyHealth();
            GameObject effect = Instantiate(hitEffect);
            effect.transform.position = transform.position;
            
        }
        Destroy(gameObject);
    }
}

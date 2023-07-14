using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletHoleManager : MonoBehaviour
{
    static public BulletHoleManager Instance { get; private set; }
    List<GameObject> bulletHoles;
    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] private int maxHolesAmount;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        bulletHoles = new List<GameObject>();
    }

    public void SpawnHole(Vector3 hitPosition, Vector3 hitNormal , Transform hitGameobject)
    {
        GameObject newHole = Instantiate(bulletHolePrefab, hitPosition + hitNormal*0.0001f,Quaternion.LookRotation(hitNormal,Vector3.up)*bulletHolePrefab.transform.rotation, hitGameobject);
        bulletHoles.Add(newHole);
        if (bulletHoles.Count > maxHolesAmount)
        {
            Destroy(bulletHoles.FirstOrDefault());
        }
    }
}

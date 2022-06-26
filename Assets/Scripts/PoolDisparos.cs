using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDisparos : MonoBehaviour
{
    public static PoolDisparos sharedInstance;
    public List<GameObject> poolBullets;
    public GameObject bulletToPool;
    public int cantPoolBullets;
    // Start is called before the first frame update
    void Start()
    {
        poolBullets = new List<GameObject>();
        for (int i = 0; i < cantPoolBullets; i++)
        {
            GameObject obj = (GameObject)Instantiate(bulletToPool);
            obj.SetActive(false);
            poolBullets.Add(obj);
            obj.transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        sharedInstance = this;
    }

    public GameObject GetPool()
    {
        for (int i = 0; i < poolBullets.Count; i++)
        {
            if (!poolBullets[i].activeInHierarchy)
            {
                return poolBullets[i];
            }
        }

        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolZombies : MonoBehaviour
{
    public static PoolZombies sharedInstance;
    public List<GameObject> poolZombies;
    public GameObject zombieToPool;
    public int cantPoolZombies;
    // Start is called before the first frame update
    void Start()
    {
        poolZombies = new List<GameObject>();
        for (int i = 0; i < cantPoolZombies; i++)
        {
            GameObject obj = (GameObject)Instantiate(zombieToPool);
            obj.SetActive(false);
            poolZombies.Add(obj);
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
        for (int i = 0; i < poolZombies.Count; i++)
        {
            if (!poolZombies[i].activeInHierarchy)
            {
                return poolZombies[i];
            }
        }

        return null;
    }

}

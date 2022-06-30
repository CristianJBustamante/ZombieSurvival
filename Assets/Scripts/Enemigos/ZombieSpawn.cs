using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("spawnearZombies");

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator spawnearZombies() {
        while (true) { 
        spawnewarZombie();
        yield return new WaitForSeconds(Random.Range(8f, 10f));
        }
        
    }

    private void spawnewarZombie() {
        GameObject zombieASpawn = PoolZombies.sharedInstance.GetPool();
        if (zombieASpawn != null)
        {
            zombieASpawn.transform.position = this.transform.position;
            zombieASpawn.transform.rotation = Quaternion.identity;
            zombieASpawn.SetActive(true);
            zombieASpawn.GetComponent<Zombie>().spawnear();
            Debug.Log("entra");
        }
        else
        {
            Debug.Log("No encunetro el zombie");
        }
    }
}

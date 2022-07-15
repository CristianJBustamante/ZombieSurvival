using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawn : MonoBehaviour
{

    public ParticleSystem spawn;

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
        yield return new WaitForSeconds(Random.Range(8f, 10f));
        spawnewarZombie();
        }
        
    }

    private void spawnewarZombie() {
        GameObject zombieASpawn = PoolZombies.sharedInstance.GetPool();
        if (zombieASpawn != null)
        {
            zombieASpawn.transform.position = this.transform.position;
            zombieASpawn.transform.rotation = Quaternion.identity;
            spawn.Play();
            zombieASpawn.SetActive(true);
            zombieASpawn.GetComponent<NavMeshAgent>().enabled = true;
            zombieASpawn.transform.GetChild(2).gameObject.SetActive(false);
            zombieASpawn.GetComponent<Zombie>().spawnear();
            
        }
        else
        {
            //Debug.Log("No encunetro el zombie");
        }
    }
}

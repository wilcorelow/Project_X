using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : CharacterStats
{
    Pooler enemyPool;

    Pooler bloodPool;

    [SerializeField] Transform bloodPoint;

    [SerializeField] float _deathtime;
    
    WaitForSeconds deathTime;
    
    [SerializeField] float experience;
    

    public Stat movementSpeed;

    PlayerLevelManager playerLevel;

    WaveManager waveManager;
    
    List<Collider> RagdollParts = new List<Collider>();

    void Start()
    {
        SetRagdollParts();

        playerLevel = GameManager.instance.GetComponent<PlayerLevelManager>();

        waveManager = GameManager.instance.GetComponent<WaveManager>();

        bloodPool = GameManager.instance.GetComponent<PoolManager>().parentPool.GetComponentInChildren<Pooler>();
        
        enemyPool = GetComponentInParent<Pooler>();
        
        deathTime = new WaitForSeconds(_deathtime);
    }

    public override void Die()
    {
        base.Die();

        currentHealth = _maxHealth.GetValue();

        playerLevel.GainExp(experience);

        waveManager.totalEnemyCount--;

        Ragdoll(true);

        StartCoroutine(ReturnThePool());
    }

    IEnumerator ReturnThePool()
    {
        yield return deathTime;
        Ragdoll(false);
        enemyPool.ReturnObject(gameObject);
    }

    void SetRagdollParts()
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.gameObject != gameObject)
            {
                c.isTrigger = true;
                c.attachedRigidbody.isKinematic = true;
                RagdollParts.Add(c);
            }
        }
    }

    public void Ragdoll(bool state)
    {
        GetComponent<NavMeshAgent>().enabled = !state;
        GetComponent<Enemy>().enabled = !state;
        GetComponent<Collider>().enabled = !state;
        GetComponentInChildren<Animator>().enabled = !state;

        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = !state;
            
            c.attachedRigidbody.isKinematic = !state;
            c.attachedRigidbody.velocity = Vector3.zero;
        }

        if (state)
        {
            RagdollParts[2].GetComponent<Rigidbody>().AddRelativeForce(2500f * Vector3.back); //Headshot

            GameObject blood = bloodPool.GetObject();
            blood.transform.position = bloodPoint.transform.position;
            blood.transform.rotation = bloodPoint.transform.rotation;
            blood.SetActive(true);
        }
            

        if(state)
        {
            foreach (Collider c in RagdollParts)
            {
                c.attachedRigidbody.useGravity = !state;
                c.attachedRigidbody.AddForce(2000f * Vector3.up);
            }
        }
        else
        {
            foreach (Collider c in RagdollParts)
            {
                c.attachedRigidbody.useGravity = !state;
            }
        }
    }
}

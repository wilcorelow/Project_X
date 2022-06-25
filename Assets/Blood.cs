using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    Pooler BloodPool;

    void Start()
    {
        BloodPool = GameManager.instance.GetComponent<PoolManager>().parentPool.GetComponentInChildren<Pooler>();
    }

    void OnEnable()
    {
        Invoke("Return", 5f);
    }

    void Return()
    {
        BloodPool.ReturnObject(gameObject);
    }
}

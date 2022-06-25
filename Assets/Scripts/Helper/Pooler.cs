using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    [SerializeField] int poolSize;
    [SerializeField] bool expandable;

    private List<GameObject> freeList;
    private List<GameObject> usedList;

    void Awake()
    {
        freeList = new List<GameObject>();
        usedList = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GenerateNewObject();
        }
    }
    //Get an object from the pool
    public GameObject GetObject()
    {
        int totalFree = freeList.Count;
        if (totalFree == 0 && !expandable) return null;
        else if (totalFree == 0) 
        {
            GenerateNewObject();
            totalFree = freeList.Count;
        }
        
        GameObject g = freeList[totalFree -1];
        freeList.RemoveAt(totalFree - 1);
        usedList.Add(g);
        return g;
    }

    //Return an object to the pool
    public void ReturnObject(GameObject obj)
    {
        Debug.Assert(usedList.Contains(obj));
        obj.SetActive(false);
        usedList.Remove(obj);
        freeList.Add(obj);
    }

    //Instantiate GameObject
    void GenerateNewObject()
    {
        GameObject g = Instantiate(prefab);
        g.transform.parent = transform;
        g.SetActive(false);
        freeList.Add(g);
    }
}

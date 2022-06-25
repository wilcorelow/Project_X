using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public delegate IEnumerator IWaves();
    public IWaves Iwaves;
    System.Delegate[] waveArray;

    Vector3 SpawnPoint;

    //Spawn Area
    [HideInInspector] public float height, width;
    
    //Junction Points
    [HideInInspector] public float northRight;
    [HideInInspector] public float eastUp;

    #region isSpawnFinished
    bool isSpawn1Finished = false;
    bool isSpawn2Finished = false;
    bool isSpawn3Finished = false;
    bool isSpawn4Finished = false;
    bool isSpawn5Finished = false;
    bool isSpawn6Finished = false;
    bool isSpawn7Finished = false;
    bool isSpawn8Finished = false;
    bool isSpawn9Finished = false;
    bool isSpawn10Finished = false;
    #endregion

    public int waveLevel = 1;

    public int totalEnemyCount;

    int waveEnemyCount;
    int currentEnemyCount;
    float waitSec;

    [SerializeField] GameObject enemyPoolParent;
    #region Enemy Pool
    [SerializeField] Pooler kamikazeLvl1;
    [SerializeField] Pooler kamikazeLvl2;
    [SerializeField] Pooler kamikazeLvl3;
    [SerializeField] Pooler kamikazeLvl4;
    [SerializeField] Pooler kamikazeLvl5;
    [SerializeField] Pooler rangedLvl1;
    [SerializeField] Pooler rangedLvl2;
    [SerializeField] Pooler rangedLvl3;
    [SerializeField] Pooler rangedLvl4;
    [SerializeField] Pooler rangedLvl5;
    #endregion
    void Start()
    {
        #region Waves
        Iwaves += IEnemyWave1;
        Iwaves += IEnemyWave2;
        Iwaves += IEnemyWave3;
        Iwaves += IEnemyWave4;
        Iwaves += IEnemyWave5;
        Iwaves += IEnemyWave6;
        Iwaves += IEnemyWave7;
        Iwaves += IEnemyWave8;
        Iwaves += IEnemyWave9;
        Iwaves += IEnemyWave10;
        #endregion

        waveArray = Iwaves.GetInvocationList();
        
        StartCoroutine(IWaveHandler(0));
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(new Vector3(-width, 0, height), new Vector3(-northRight, 0, height), Color.green);
        Debug.DrawLine(new Vector3(-northRight, 0, height), new Vector3(northRight, 0, height), Color.blue);
        Debug.DrawLine(new Vector3(northRight, 0, height), new Vector3(width, 0, height), Color.red);

        Debug.DrawLine(new Vector3(width, 0, height), new Vector3(width, 0, eastUp), Color.green);
        Debug.DrawLine(new Vector3(width, 0, eastUp), new Vector3(width, 0, -eastUp), Color.blue);
        Debug.DrawLine(new Vector3(width, 0, -eastUp), new Vector3(width, 0, -height), Color.red);

        Debug.DrawLine(new Vector3(-width, 0, -height), new Vector3(-northRight, 0, -height), Color.red);
        Debug.DrawLine(new Vector3(-northRight, 0, -height), new Vector3(northRight, 0, -height), Color.blue);
        Debug.DrawLine(new Vector3(northRight, 0, -height), new Vector3(width, 0, -height), Color.green);

        Debug.DrawLine(new Vector3(-width, 0, height), new Vector3(-width, 0, eastUp), Color.red);
        Debug.DrawLine(new Vector3(-width, 0, eastUp), new Vector3(-width, 0, -eastUp), Color.blue);
        Debug.DrawLine(new Vector3(-width, 0, -eastUp), new Vector3(-width, 0, -height), Color.green);

        //height 25
        //width 49
        //northRight 30
        //eastUp 16
    }

    Vector3 GetShuffledDirection(int waveDirectionIndex)
    {
        if (waveDirectionIndex < 3)                                
        {
            SpawnPoint.z = height;  //north
        }
        else if (waveDirectionIndex > 2 && waveDirectionIndex < 6) 
        {
            SpawnPoint.x = width;   //east
        }
        else if (waveDirectionIndex > 5 && waveDirectionIndex < 9) 
        {
            SpawnPoint.z = -height; //south
        }
        else if (waveDirectionIndex > 8 && waveDirectionIndex < 12)
        {
            SpawnPoint.x = -width;  //west
        }

        switch (waveDirectionIndex)
        {
            case 0:
                SpawnPoint.x = Random.Range(-width, -northRight);
                return SpawnPoint;
            case 1:
                SpawnPoint.x = Random.Range(-northRight, northRight);
                return SpawnPoint;
            case 2:
                SpawnPoint.x = Random.Range(northRight, width);
                return SpawnPoint;
            case 3:
                SpawnPoint.z = Random.Range(eastUp, height);
                return SpawnPoint;
            case 4:
                SpawnPoint.z = Random.Range(-eastUp, eastUp);
                return SpawnPoint;
            case 5:
                SpawnPoint.z = Random.Range(-height, -eastUp);
                return SpawnPoint;
            case 6:
                SpawnPoint.x = Random.Range(-width, -northRight);
                return SpawnPoint;
            case 7:
                SpawnPoint.x = Random.Range(-northRight, northRight);
                return SpawnPoint;
            case 8:
                SpawnPoint.x = Random.Range(northRight, width);
                return SpawnPoint;
            case 9:
                SpawnPoint.z = Random.Range(eastUp, height);
                return SpawnPoint;
            case 10:
                SpawnPoint.z = Random.Range(-eastUp, eastUp);
                return SpawnPoint;
            case 11:
                SpawnPoint.z = Random.Range(-height, -eastUp);
                return SpawnPoint;
            default:
                Debug.LogWarning("Shuffle cases are not appropriate!");
                return SpawnPoint;
        }
    }

    List<int> NumberBag = new List<int>();
    int GetShuffledRNG()
    {
        if (NumberBag.Count == 0)
        {
            //NumberBag.TrimExcess();
            for (int i = 0; i < 12; i++)
            {
                NumberBag.Add(i);
            }
        }

        int generatedNumber = NumberBag[Random.Range(0, NumberBag.Count)];
        NumberBag.Remove(generatedNumber);
        return generatedNumber;
    }

    int CreateEnemy(int enemyCount, Pooler enemyPool)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = enemyPool.GetObject();
            
            enemy.transform.position = GetShuffledDirection(GetShuffledRNG());
            enemy.SetActive(true);
            totalEnemyCount++;
        }
        return enemyCount;
    }

    IEnumerator IWaveHandler(float startAt)
    {
        yield return new WaitForSeconds(startAt);

        StartCoroutine(((IWaves)waveArray[0]).Invoke());
        yield return new WaitUntil(() => isSpawn1Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        StopCoroutine(((IWaves)waveArray[0]).Invoke());

        StartCoroutine(((IWaves)waveArray[1]).Invoke());
        yield return new WaitUntil(() => isSpawn2Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        StopCoroutine(((IWaves)waveArray[1]).Invoke());

        StartCoroutine(((IWaves)waveArray[2]).Invoke());
        yield return new WaitUntil(() => isSpawn3Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        StopCoroutine(((IWaves)waveArray[2]).Invoke());

        StartCoroutine(((IWaves)waveArray[3]).Invoke());
        yield return new WaitUntil(() => isSpawn4Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        StopCoroutine(((IWaves)waveArray[3]).Invoke());

        StartCoroutine(((IWaves)waveArray[4]).Invoke());
        yield return new WaitUntil(() => isSpawn5Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        StopCoroutine(((IWaves)waveArray[4]).Invoke());

        StartCoroutine(((IWaves)waveArray[5]).Invoke());
        yield return new WaitUntil(() => isSpawn6Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        StopCoroutine(((IWaves)waveArray[5]).Invoke());

        StartCoroutine(((IWaves)waveArray[6]).Invoke());
        yield return new WaitUntil(() => isSpawn7Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        StopCoroutine(((IWaves)waveArray[6]).Invoke());

        StartCoroutine(((IWaves)waveArray[7]).Invoke());
        yield return new WaitUntil(() => isSpawn8Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        StopCoroutine(((IWaves)waveArray[7]).Invoke());

        StartCoroutine(((IWaves)waveArray[8]).Invoke());
        yield return new WaitUntil(() => isSpawn9Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        StopCoroutine(((IWaves)waveArray[8]).Invoke());

        StartCoroutine(((IWaves)waveArray[9]).Invoke());
        yield return new WaitUntil(() => isSpawn10Finished);
        yield return new WaitUntil(() => !isThereEnemy());
        Debug.Log("FIN");
    }

    public bool isThereEnemy()
    {
        if (totalEnemyCount <= 0)
        {
            return false;
        }
        else return true;
    }

    #region EnemyWaves

    IEnumerator IEnemyWave1()
    {
        //Debug.Log("Spawn 1 Started");
        waveEnemyCount = 4;
        currentEnemyCount = 0;
        waitSec = 3;
        while (!isSpawn1Finished)
        {
            currentEnemyCount += CreateEnemy(1, kamikazeLvl1);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 1 Finished");
                isSpawn1Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    IEnumerator IEnemyWave2()
    {
        //Debug.Log("Spawn 2 Started");
        waveEnemyCount = 8;
        currentEnemyCount = 0;
        waitSec = 4;
        while (!isSpawn2Finished)
        {
            currentEnemyCount += CreateEnemy(2, kamikazeLvl1);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 2 Finished");
                isSpawn2Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    IEnumerator IEnemyWave3()
    {
        //Debug.Log("Spawn 3 Started");
        waveEnemyCount = 4;
        currentEnemyCount = 0;
        waitSec = 4;
        while (!isSpawn3Finished)
        {
            currentEnemyCount += CreateEnemy(1, kamikazeLvl2);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 3 Finished");
                isSpawn3Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    IEnumerator IEnemyWave4()
    {
        //Debug.Log("Spawn 4 Started");
        waveEnemyCount = 6;
        currentEnemyCount = 0;
        waitSec = 7;
        while (!isSpawn4Finished)
        {
            currentEnemyCount += CreateEnemy(1, rangedLvl1);
            currentEnemyCount += CreateEnemy(2, kamikazeLvl1);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 4 Finished");
                isSpawn4Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    IEnumerator IEnemyWave5()
    {
        //Debug.Log("Spawn 5 Started");
        waveEnemyCount = 3;
        currentEnemyCount = 0;
        waitSec = 5;
        while (!isSpawn5Finished)
        {
            currentEnemyCount += CreateEnemy(1, rangedLvl2);
            currentEnemyCount += CreateEnemy(2, kamikazeLvl2);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 5 Finished");
                isSpawn5Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    IEnumerator IEnemyWave6()
    {
        //Debug.Log("Spawn 6 Started");
        waveEnemyCount = 6;
        currentEnemyCount = 0;
        waitSec = 5;
        while (!isSpawn6Finished)
        {
            currentEnemyCount += CreateEnemy(2, kamikazeLvl3);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 6 Finished");
                isSpawn6Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    IEnumerator IEnemyWave7()
    {
        //Debug.Log("Spawn 7 Started");
        waveEnemyCount = 9;
        currentEnemyCount = 0;
        waitSec = 8;
        while (!isSpawn7Finished)
        {
            currentEnemyCount += CreateEnemy(1, rangedLvl4);
            currentEnemyCount += CreateEnemy(2, kamikazeLvl4);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 7 Finished");
                isSpawn7Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    IEnumerator IEnemyWave8()
    {
        //Debug.Log("Spawn 8 Started");
        waveEnemyCount = 6;
        currentEnemyCount = 0;
        waitSec = 5;
        while (!isSpawn8Finished)
        {
            currentEnemyCount += CreateEnemy(2, rangedLvl4);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 8 Finished");
                isSpawn8Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    IEnumerator IEnemyWave9()
    {
        //Debug.Log("Spawn 9 Started");
        waveEnemyCount = 10;
        currentEnemyCount = 0;
        waitSec = 6;
        while (!isSpawn9Finished)
        {
            currentEnemyCount += CreateEnemy(1, kamikazeLvl5);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 9 Finished");
                isSpawn9Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    IEnumerator IEnemyWave10()
    {
        //Debug.Log("Spawn 10 Started");
        waveEnemyCount = 30;
        currentEnemyCount = 0;
        waitSec = 8;
        while (!isSpawn10Finished)
        {
            currentEnemyCount += CreateEnemy(1, rangedLvl5);
            currentEnemyCount += CreateEnemy(2, kamikazeLvl5);
            
            if (currentEnemyCount >= waveEnemyCount)
            {
                //Debug.Log("Spawn 10 Finished");
                isSpawn10Finished = true;
                break;
            }
            yield return new WaitForSeconds(waitSec);
        }
    }

    #endregion
}


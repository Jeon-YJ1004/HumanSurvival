using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ��������� ������ ����.
    List<GameObject>[] enemyPools; // Ǯ ����� �ϴ� ����Ʈ��

    GameObject[] targetPrefab;
    List<GameObject>[] targetPool;

    public GameObject[] expPrefabs;
    List<GameObject>[] expPools;
    public GameObject[] coinPrefabs;
    List<GameObject>[] coinPools;
    public GameObject[] heartPrefabs;
    List<GameObject>[] heartPools;

    void Awake()
    {
        enemyPools = new List<GameObject>[enemyPrefabs.Length];
        expPools = new List<GameObject>[expPrefabs.Length];
        coinPools = new List<GameObject>[coinPrefabs.Length];
        heartPools = new List<GameObject>[heartPrefabs.Length];

        // �ν����Ϳ��� �ʱ�ȭ
        for (int index = 0; index < enemyPools.Length; index++)
            enemyPools[index] = new List<GameObject>();
        for (int index = 0; index < expPools.Length; index++)
            expPools[index] = new List<GameObject>();
        for (int index = 0; index < coinPools.Length; index++)
            coinPools[index] = new List<GameObject>();
        for (int index = 0; index < heartPools.Length; index++)
            heartPools[index] = new List<GameObject>();
    }
    public GameObject Get(string type,int index) //���� ������Ʈ ��ȯ �Լ�
    {
        switch (type)
        {
            case "enemy":
                targetPool = enemyPools;
                targetPrefab = enemyPrefabs;
                break;
            case "exp":
                targetPool = expPools;
                targetPrefab = expPrefabs;
                break;

            case "heart":
                targetPool = heartPools;
                targetPrefab = heartPrefabs;
                Debug.Log("heart pooling");
                break;

            case "coin":
                targetPool = coinPools;
                targetPrefab = coinPrefabs;
                Debug.Log("coin pooling");
                break;
        }
        GameObject select = null;
        //������ Ǯ�� ��Ȱ��ȭ �� ���� ������Ʈ ����.
        // �߰��ϸ� select ������ �Ҵ�// ������ ���� ������� 
        
        foreach (GameObject item in targetPool[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ��ã���� ���Ӱ� ������ �Ҵ�// ��� ���� ���� �ʰ� �������
        if (!select)
        {
            select = Instantiate(targetPrefab[index], transform);
            targetPool[index].Add(select);
        }
        return select;
    }
    public void Clear(int index)
    {
        foreach (GameObject item in enemyPools[index])
            item.SetActive(false);
    }

    public void ClearAll()
    {
        for (int index = 0; index < enemyPools.Length; index++)
            foreach (GameObject item in enemyPools[index])
                item.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    public GameObject dropsObj;
    [System.Serializable]
    public class Drops
    {
        public string name;
        public int prefabsIndex;
        public float dropRate;
    }
    
    public List<Drops> drops;
     public void OnDrop(Vector2 pos)
    {
        //���� 1. ���� ������ ���� �ѹ� ����(������ Ȯ��)
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        dropsObj = GameObject.Find("--DropObj--");
        //���� 3.
        List<Drops> posibleDrops = new List<Drops>();
        foreach(Drops rate in drops)
        {   
            //���� 2
            if (randomNumber <= rate.dropRate) posibleDrops.Add(rate);
        }       
        //drop possible ���� Ȯ��
        if (posibleDrops.Count > 0)
        {   
            //���� 4.
            Drops drops = posibleDrops[UnityEngine.Random.Range(0, posibleDrops.Count)];
            dropsObj.GetComponent<DropSpawner>().Spawn(transform.position,drops.name, drops.prefabsIndex);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour,IDamageable
{
    public void TakeDamage(float damage)
    {
        //position ��ġ�� Drop ����
        gameObject.GetComponent<DropSystem>().OnDrop(gameObject.transform.position);
        Destroy(gameObject);
    }
}

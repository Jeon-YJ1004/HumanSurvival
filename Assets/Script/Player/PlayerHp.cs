using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;
using System;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] Slider HpBar;
    private float damage = 0.1f;    //���� ������
    void Start()
    {
        //TODO: ĳ���� ������ �ִ� ü�� ��������
        HpBar.maxValue = (int)Enums.Stat.MaxHealth;
        HpBar.value = (int)Enums.Stat.MaxHealth;
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Monster"){
            HpBar.value -= damage;
        }
    }
}
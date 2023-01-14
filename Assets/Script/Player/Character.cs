using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //ĳ������ ��������
    //���ø� ���� ���� �������� ����
    private int damage = 10;              //���ط�
    private int projectileSpeed = 1;     //����ü �ӵ�
    private int duration = 3;            //���� �ð�
    private int attackRange = 1;         //���ݹ���
    private int cooldown = 5;            //��Ÿ��
    private int numberOfProjectiles = 1;     //����ü ��

    //ĳ���� ������Ʈ ��������
    public GameObject character;
    //Get,Set�Լ� �ڵ� ����
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }
    public int Duration
    {
        get { return duration; }
        set { duration = value; }
    }
    public int AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public int Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }
    public int NumberOfProjectiles
    {
        get { return numberOfProjectiles; }
        set { numberOfProjectiles = value; }
    }
}

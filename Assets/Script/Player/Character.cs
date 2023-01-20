using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.System;

public class Character : MonoBehaviour
{
    //ĳ������ ��������
    //���ø� ���� ���� �������� ����
    private int damage = 10;              //���ط�
    private int projectileSpeed = 1;     //����ü �ӵ�
    private int duration = 3;            //���� �ð�
    private int attackRange = 1;         //���ݹ���
    private int cooldown = 3;            //��Ÿ��
    private int numberOfProjectiles = 1;     //����ü ��

    private int _level;
    private int _exp;
    private int _maxExp;
    private int _dExp;

    public Stat ChracterStat;

    void Start()
    {
        _level = 1;
        _exp = 0;
        _maxExp = 100;
        _dExp = 10;

        // TODO user�� ���� ȭ�鿡�� ��ȭ�س��� ���ȵ��� �⺻������ �޾ƿ���

    }

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


    public void GetExp(int exp)
    {
        _exp += exp;
        while (_exp >= _maxExp)
        {
            _exp -= _maxExp;
            _maxExp += _dExp;
            LevelUp();
        }
    }
    public void LevelUp()
    {
        _level++;
        // TODO: ��ų ���� ���, luck�� ��͵� �ݿ�
    }
}

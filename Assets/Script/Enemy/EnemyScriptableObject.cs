using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField]
    int spriteType;
    public int SpriteType{get=>spriteType; private set => spriteType=value;}
    [SerializeField]
    float spawnTime;
    public float SpawnTime { get => spawnTime; private set => spawnTime = value; }
    [SerializeField]
    public int speed; //������ �̵� �ӵ�
    public int Speed { get => speed; private set => speed = value; }
    [SerializeField]
    public float power; //������ ���ݷ�
    public float Power { get => power; private set => power = value; }
    [SerializeField]
    public float knockback; //���� �ǰ� �� �˹�(�и���) ������ ���� ��ġ
    public float Knockback { get => knockback; private set => knockback = value; }
    [SerializeField]
    public float maxKnockback; //���� �˹� ������ ������ �� �ִµ� �� ������ ����
    public float MaxKnockback { get => maxKnockback; private set => maxKnockback = value; }
    [SerializeField]
    public float deathKB; //���� ��� �� �˹�(�и���) ������ ���� ��ġ
    public float DeathKB { get => deathKB; private set => deathKB = value; }
    [SerializeField]
    public int xp; //����Ǵ� ����ġ�� ��(��ġ) �̴�.
    public int Xp { get => xp; private set => xp = value; }
    [SerializeField]
    public int end; //���� �� ���Ѽ�
    public int End { get => end; private set => end = value; }
    [SerializeField]
    public int level; //�ʱ� ���� ��ġ
    public int Level { get => level; private set => level = value; }
    [SerializeField]
    public float maxHP;
    public float MaxHP { get => maxHP; private set => maxHP = value; }
}
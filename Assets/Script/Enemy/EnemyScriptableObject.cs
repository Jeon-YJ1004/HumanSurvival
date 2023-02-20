using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public int spriteType;
    public float spawnTime;
    public int speed; //������ �̵� �ӵ�
    public float power; //������ ���ݷ�
    public float knockback; //���� �ǰ� �� �˹�(�и���) ������ ���� ��ġ
    public float maxKnockback; //���� �˹� ������ ������ �� �ִµ� �� ������ ����
    public float deathKB; //���� ��� �� �˹�(�и���) ������ ���� ��ġ
    public int xp; //����Ǵ� ����ġ�� ��(��ġ) �̴�.
    public int end; //���� �� ���Ѽ�
    public int level; //�ʱ� ���� ��ġ
    public float maxHP;
}
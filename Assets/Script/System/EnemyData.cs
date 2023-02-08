using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class Monster
{
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

[Serializable]
public class MonsterList
{
    public Dictionary<string, Monster> monsters;
}


public class EnemyData : MonoBehaviour
{
    private void Start()
    {
        Dictionary<string, Monster> monsterDic = new Dictionary<string, Monster>();

        Monster bat = new Monster();
        bat.level = 1;
        bat.maxHP = 0.5f;
        bat.speed = 140;
        bat.power = 5;
        bat.knockback = 1;
        bat.maxKnockback = 3;
        bat.deathKB = 2;
        bat.xp = 1;
        bat.end = 29;
        


        Monster skeleton = new Monster();
        skeleton.level = 1;
        skeleton.maxHP = 1.5f;
        skeleton.speed = 100;
        skeleton.power = 10;
        skeleton.knockback = 1;
        skeleton.maxKnockback = 3;
        skeleton.deathKB = 5;
        skeleton.xp = 2;
        skeleton.end = 18;

        Monster ghoul = new Monster();
        ghoul.level = 1;
        ghoul.maxHP = 1.0f;
        ghoul.speed = 100;
        ghoul.power = 10;
        ghoul.knockback = 0.8f;
        ghoul.maxKnockback = 3;
        ghoul.deathKB = 4;
        ghoul.xp = 1;
        ghoul.end = 20;




        monsterDic["Bat"] = bat;
        monsterDic["Skeleton"] = skeleton;
        monsterDic["Ghoul"] = ghoul;

        MonsterList Monster = new MonsterList();
        Monster.monsters = monsterDic;

        //ToJson �κ�
        //string jsonData = DictionaryJsonUtility.ToJson(monsterDic, true);

        string path = Application.dataPath + "/Data";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        //File.WriteAllText(path + "/MonsterData.txt", jsonData);

        //FromJson �κ�
        string fromJsonData = File.ReadAllText(path + "/MonsterData.txt");

        MonsterList MonsterFromJson = new MonsterList();
       // MonsterFromJson.monsters = DictionaryJsonUtility.FromJson<string, Monster>(fromJsonData);
        print(MonsterFromJson.monsters);
    }

}
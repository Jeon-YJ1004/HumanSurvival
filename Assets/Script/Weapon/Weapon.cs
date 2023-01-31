using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

public class Weapon : MonoBehaviour
{
    //������ ���� ����
    //���ø� ���� ���� �������� ����
    public int WeaponIndex;
    public int WeaponLevel;
    public int WeaponMaxLevel;

    private int damage = 10;                //���ط�
    private int projectileSpeed = 1;        //����ü �ӵ�
    private int duration = 3;               //���� �ð�
    private int attackRange = 1;            //���ݹ���
    private int cooldown = 3;               //��Ÿ��
    private int numberOfProjectiles = 1;    //����ü ��
    private int totalspeed;                 //�� �ӵ�

    public float[] WeaponStats;
    public static float[][] defaultWeaponStats;
    public static List<List<Tuple<int, float>>>[] WeaponUpgrade;

    static Weapon()
    {
        WeaponUpgrade = new List<List<Tuple<int, float>>>[13];
    }
    public Weapon(int weaponIndex = 0)
    {
        this.WeaponIndex = weaponIndex;
        this.WeaponStats = defaultWeaponStats[weaponIndex];

        WeaponStats = new float[7];
        WeaponLevel = 0;
        WeaponMaxLevel = WeaponUpgrade[weaponIndex].Count + 1;
    }

    private void Start()
    {
        levelOneWeaponPreprocessing();
        weaponUpgradePreprocessing();
    }

    private void Update()
    {
        transform.position = transform.position + Vector3.right * totalspeed * Time.deltaTime;
    }
    public void Shoot(int speed)
    {
        totalspeed = speed;
    }
    public void Upgrade()
    {
        foreach ((var statIndex, var data) in WeaponUpgrade[WeaponIndex][WeaponLevel])
        {
            WeaponStats[statIndex] += data;
        }
        WeaponLevel++;
    }
    public bool IsMaster()
    {
        return WeaponLevel == WeaponMaxLevel ? true : false;
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

    private void levelOneWeaponPreprocessing()
    {
        defaultWeaponStats = new float[13][];
        defaultWeaponStats[(int)Enums.Weapon.Whip] = new float[7] { 10, 1.35f, Constants.X, Constants.X, 1, 30, Constants.INF };
        defaultWeaponStats[(int)Enums.Weapon.MagicWand] = new float[7] { 10, 1.20f, 1, Constants.X, 1, 60, 1 };
        defaultWeaponStats[(int)Enums.Weapon.Knife] = new float[7] { 6.5f, 1.00f, 1, Constants.X, 1, 70, 1 };
        defaultWeaponStats[(int)Enums.Weapon.Axe] = new float[7] { 20, 4.00f, 1, Constants.X, 1, 70, 3 };
        defaultWeaponStats[(int)Enums.Weapon.Cross] = new float[7] { 5, 2.00f, 1, Constants.X, 1, 30, Constants.INF };
        defaultWeaponStats[(int)Enums.Weapon.KingBible] = new float[7] { 10, 3.00f, 1, 3, 1, 50, Constants.INF };
        defaultWeaponStats[(int)Enums.Weapon.FireWand] = new float[7] { 20, 3.00f, 1, Constants.X, 3, 30, 1 };
        defaultWeaponStats[(int)Enums.Weapon.Garlic] = new float[7] { 5, 1.00f, Constants.X, Constants.X, Constants.X, Constants.X, Constants.INF };
        defaultWeaponStats[(int)Enums.Weapon.SantaWater] = new float[7] { 10, 4.50f, Constants.X, 2, 1, 20, Constants.INF };
        defaultWeaponStats[(int)Enums.Weapon.Peachone] = new float[7] { 10, 1.00f, 0.80f, 4, 4, 60, Constants.INF };
        defaultWeaponStats[(int)Enums.Weapon.EbonyWings] = new float[7] { 10, 1.00f, 0.80f, 4, 4, 60, Constants.INF };
        defaultWeaponStats[(int)Enums.Weapon.Runetracer] = new float[7] { 10, 3.00f, 1.00f, 2.25f, 1, 25, Constants.INF };
        defaultWeaponStats[(int)Enums.Weapon.LightningRing] = new float[7] { 15, 4.50f, Constants.X, Constants.X, 2, 50, Constants.INF };
    }

    private void weaponUpgradePreprocessing()
    {
        // Whip
        WeaponUpgrade[0] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });

        // MagicWand
        WeaponUpgrade[1] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.2f)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 1)
        });
        WeaponUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });

        // Knife
        WeaponUpgrade[2] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5)
        });
        WeaponUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 1)
        });

        // Axe
        WeaponUpgrade[3] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 2)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Piercing, 2)
        });
        WeaponUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });

        // Cross
        WeaponUpgrade[4] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.25f),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.25f),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.10f)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });

        //KingBible
        WeaponUpgrade[5] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.30f),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.25f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.5f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.30f),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.25f)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.5f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });

        // FireWand
        WeaponUpgrade[6] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });

        // Garlic
        WeaponUpgrade[7] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 2)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.1f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 1)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 1)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.1f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 2)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 1)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.1f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 1)
        });
        WeaponUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 2)
        });

        // SantaWater
        WeaponUpgrade[8] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.5f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.3f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.3f)
        });
        WeaponUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.20f)
        });

        // Peachone
        WeaponUpgrade[9] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });

        // EbonyWings
        WeaponUpgrade[10] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Cooldown, -0.3f)
        });
        WeaponUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 0.40f)
        });

        // Runetracer
        WeaponUpgrade[11] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.3f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.ProjectileSpeed, 0.20f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 5),
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.3f)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Duration, 0.5f)
        });

        // LightningRing
        WeaponUpgrade[12] = new List<List<Tuple<int, float>>>();
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 10)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Area, 1),
            new Tuple<int, float>((int)Enums.WeaponStat.Might, 20)
        });
        WeaponUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.WeaponStat.Amount, 1)
        });
    }
}

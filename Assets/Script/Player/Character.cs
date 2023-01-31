using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Enums;
using Rito;
using static UnityEngine.Rendering.DebugUI.Table;
using static Constants;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

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

    private int mLevel;
    private int mExp;
    private int mMaxExp;
    private int mdExp;
    private int mMaxWeaponNumber = 6;
    private int mMaxAccessoryNumber = 6;
    private int mCompleteWeaponNumber = 0;
    private int mCompleteAccessoryNumber = 0;

    public float[] CharacterStats;
    private int[] mWeaponRarity;
    private int[] mAccessoryRarity;
    private WeightedRandomPicker<int> mWeaponPicker;
    private WeightedRandomPicker<int> mAccessoryPicker;
    public List<Weapon> Weapons;
    public List<Tuple<int, int>> Accessorys;  // tuple< Accessory_index, now_Accessory_level >
    public static List<List<Tuple<int, float>>>[] AccessoryUpgrade;

    private int[] mTransWeaponIndex; // �ش� index�� weapon�� ���� �������� Weapons�� �� ��° index�� �ִ��� ��ȯ�ϴ� �迭
    private bool[] mHasAccessoryIndex; // �ش� Accessory�� ���������� ǥ�� (0, 1)
    static Character()
    {
        AccessoryUpgrade = new List<List<Tuple<int, float>>>[21];
    }
    void Start()
    {
        mLevel = 1;
        mExp = 0;
        mMaxExp = 100;
        mdExp = 10;

        CharacterStats = new float[20]; // TODO user�� ���� ȭ�鿡�� ��ȭ�س��� ���ȵ��� �⺻������ �޾ƿ���
        AccessoryUpgradePreprocessing();

        mWeaponRarity = new int[13];
        mAccessoryRarity = new int[21]; 

        mTransWeaponIndex = Enumerable.Repeat<int>(-1, 13).ToArray<int>();
        mHasAccessoryIndex = Enumerable.Repeat<bool>(false, 21).ToArray<bool>();
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
        // TODO: stat�� growth �����Ͽ� ����ġ ȹ��

        mExp += exp;
        while (mExp >= mMaxExp)
        {
            mExp -= mMaxExp;
            mMaxExp += mdExp;
            LevelUp();
        }
    }
    public void LevelUp()
    {
        mLevel++;
        // ���� �Ͻ�����

        // TODO: ��ų ���� ���
        // 

        // ���� �� ���� �簳
    }
    public void UpdateLuck(int luck)
    {
        CharacterStats[(int)Enums.Stat.Luck] = luck;
        mWeaponPicker = new WeightedRandomPicker<int>();
        for (int i = 0; i < mWeaponRarity.Length; i++)
        {
            mWeaponPicker.Add(i, (mWeaponRarity[i] + luck) / (double)mWeaponRarity[i]);
        }

        mAccessoryPicker = new WeightedRandomPicker<int>();
        for (int i = 0; i < mAccessoryRarity.Length; i++)
        {
            mAccessoryPicker.Add(i, (mAccessoryRarity[i] + luck) / (double)mAccessoryRarity[i]);
        }
    }
    public void RandomPickUp(int n)
    {
        int possibleNewWeaponNumber = mMaxWeaponNumber - Weapons.Count;
        int incompleteWeaponNumber = Weapons.Count - mCompleteWeaponNumber;
        int maxWeaponChoice = possibleNewWeaponNumber + incompleteWeaponNumber;

        int possibleNewAccessoryNumber = mMaxAccessoryNumber - Accessorys.Count;
        int incompleteAccessoryNumber = Accessorys.Count - mCompleteAccessoryNumber;
        int maxAccessoryChoice = possibleNewAccessoryNumber + incompleteAccessoryNumber;
        int maxChoice = System.Math.Min(getChoice(), maxWeaponChoice + maxAccessoryChoice);

        if (maxChoice == 0)
        {

        }
        else if (possibleNewWeaponNumber == 0)
        {

        }

        // TODO: ���� ��ų���� ������ �� �ְ� ǥ��
        // TODO: ���õ� ��ų�� ������ �����ֱ�


    }

    private int getChoice()
    {
        if (UnityEngine.Random.Range(0, 101) <= CharacterStats[(int)Enums.Stat.Luck])
        {
            return 4;
        }
        else
        {
            return 3;
        }
    }

    private void getWeapon(int weaponIndex)
    {
        mTransWeaponIndex[weaponIndex] = Weapons.Count;
        Weapons.Add(new Weapon(weaponIndex));
    }

    private void upgradeWeapon(int weaponIndex)
    {
        Weapons[mTransWeaponIndex[weaponIndex]].Upgrade();
    }
    private void getAccessory(int AccessoryIndex)
    {
        mHasAccessoryIndex[AccessoryIndex] = true;
    }

    private void AccessoryUpgradePreprocessing()
    {
        // Spinach
        AccessoryUpgrade[0] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>()
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });
        AccessoryUpgrade[0].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.1f)
        });

        // Armor
        AccessoryUpgrade[1] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });
        AccessoryUpgrade[1].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Armor, 1),
            new Tuple<int, float>((int)Enums.AccessoryStat.ReflectionPer, 0.1f)
        });

        // HollowHeart
        AccessoryUpgrade[2] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });
        AccessoryUpgrade[2].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.2f)
        });

        // Pummarola
        AccessoryUpgrade[3] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });
        AccessoryUpgrade[3].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f)
        });

        // EmptyTome
        AccessoryUpgrade[4] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });
        AccessoryUpgrade[4].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CooldownPer, -0.8f)
        });

        // Candelabrador
        AccessoryUpgrade[5] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });
        AccessoryUpgrade[5].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.10f)
        });

        // Bracer
        AccessoryUpgrade[6] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });
        AccessoryUpgrade[6].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.1f)
        });

        // Spellbinder
        AccessoryUpgrade[7] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });
        AccessoryUpgrade[7].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.1f)
        });

        // Duplicator
        AccessoryUpgrade[8] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Amount, 1)
        });
        AccessoryUpgrade[8].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Amount, 1)
        });

        // Wings
        AccessoryUpgrade[9] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });
        AccessoryUpgrade[9].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MoveSpeedPer, 10)
        });

        // Attractorb
        AccessoryUpgrade[10] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 33)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 33)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 25)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 20)
        });
        AccessoryUpgrade[10].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MagnetPer, 33)
        });

        // Clover
        AccessoryUpgrade[11] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });
        AccessoryUpgrade[11].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.LuckPer, 10)
        });

        // Crown
        AccessoryUpgrade[12] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });
        AccessoryUpgrade[12].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GrowthPer, 8)
        });

        // StoneMask
        AccessoryUpgrade[13] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });
        AccessoryUpgrade[13].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.GreedPer, 10)
        });

        // Tiragisu
        AccessoryUpgrade[14] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Revival, 1)
        });
        AccessoryUpgrade[14].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Revival, 1)
        });

        // Skull
        AccessoryUpgrade[15] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });
        AccessoryUpgrade[15].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 10)
        });

        // SilverRing
        AccessoryUpgrade[16] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });
        AccessoryUpgrade[16].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.05f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.05f)
        });

        // GoldRing
        AccessoryUpgrade[17] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[17].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });

        // MetaglioLeft
        AccessoryUpgrade[18] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });
        AccessoryUpgrade[18].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.Recovery, 0.1f),
            new Tuple<int, float>((int)Enums.AccessoryStat.MaxHealthPer, 0.05f)
        });

        // MetaglioRight
        AccessoryUpgrade[19] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });
        AccessoryUpgrade[19].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 5)
        });

        // TorronaBox
        AccessoryUpgrade[20] = new List<List<Tuple<int, float>>>();
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.MightPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.ProjectileSpeedPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.DurationPer, 0.03f),
            new Tuple<int, float>((int)Enums.AccessoryStat.AreaPer, 0.03f)
        });
        AccessoryUpgrade[20].Add(new List<Tuple<int, float>>
        {
            new Tuple<int, float>((int)Enums.AccessoryStat.CursePer, 100)
        });
    }
}

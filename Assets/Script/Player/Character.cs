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
    public GameObject LevepUpUI;

    private int mDamage = 10;              //���ط�
    private int mProjectileSpeed = 1;     //����ü �ӵ�
    private int mDuration = 3;            //���� �ð�
    private int mAttackRange = 1;         //���ݹ���
    private int mCooldown = 3;            //��Ÿ��
    private int mNumberOfProjectiles = 1;     //����ü ��

    private int mLevel;
    private int mExp;
    private int mMaxExp;
    private int mdExp;

    public float[] CharacterStats;
    public List<Weapon> Weapons;
    public List<Accessory> Accessories;
    public List<int> MasteredWeapons;
    public List<int> MasteredAccessories;
    public RandomPickUpSystem RandomPickUpSystem;

    public int[] TransWeaponIndex; // �ش� index�� weapon�� ���� �������� Weapons�� �� ��° index�� �ִ��� ��ȯ�ϴ� �迭, ���ٸ� -1 ��ȯ
    public int[] TransAccessoryIndex; // ���� ������ Accessory�� �ش�
    void Start()
    {
        mLevel = 1;
        mExp = 0;
        mMaxExp = 100;
        mdExp = 10;

        // TODO: user�� ���� ȭ�鿡�� ��ȭ�س��� ���ȵ��� �⺻������ �޾ƿ���
        CharacterStats = new float[21] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 70, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        Weapons = new List<Weapon>();
        Accessories = new List<Accessory>();

        TransWeaponIndex = Enumerable.Repeat<int>(-1, 13).ToArray<int>();
        TransAccessoryIndex = Enumerable.Repeat<int>(-1, 21).ToArray<int>();

        MasteredWeapons = new List<int>();
        MasteredAccessories = new List<int>();

        RandomPickUpSystem = new RandomPickUpSystem();
        UpdateLuck(CharacterStats[(int)Enums.Stat.Luck]);

        // �ӽ�
        GetWeapon(0);
        GetWeapon(1);
        GetAccessory(0);
        GetAccessory(1);

    }
    //��ư ����� ���� ����
    public void TempLoad()
    {
        LevelUp();
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

        var pickUps = RandomPickUpSystem.RandomPickUp(this);
        LevepUpUI.GetComponent<LevelUpUIManager>().LoadLevelUpUI(CharacterStats, pickUps, Weapons, Accessories);
        // ���� �簳

    }
    public void UpdateLuck(float luck)
    {
        RandomPickUpSystem.UpdateWeaponPickUpList(luck, this);
        RandomPickUpSystem.UpdateAccessoryPickUpList(luck, this);
    }
    
    public void ApplyItem(Tuple<int, int, int> pickUp)
    {
        switch ((Enums.PickUpType)pickUp.Item1)
        {
            case Enums.PickUpType.Weapon:
                applyWeapon(pickUp.Item2, pickUp.Item3);
                break;
            case Enums.PickUpType.Accessory:
                applyAccessory(pickUp.Item2, pickUp.Item3);
                break;
            default:
                applyEtc(pickUp.Item2);
                break;
        }
    }
    private void applyWeapon(int weaponIndex, int hasWeapon)
    {
        if (hasWeapon == 0)
            GetWeapon(weaponIndex);
        else
            UpgradeWeapon(weaponIndex);
    }
    private void applyAccessory(int accessoryIndex, int hasAccessory)
    {
        if (hasAccessory == 0)
            GetAccessory(accessoryIndex);
        else
            UpgradeAccessory(accessoryIndex);
    }
    private void applyEtc(int etcIndex)
    {
        switch ((Enums.Etc)etcIndex)
        {
            case Enums.Etc.Food:
                // TODO: ü�� ȸ�� �Լ��� ����
                break;
            case Enums.Etc.Money:
                // TODO: ��ȭ ȹ�� �Լ��� ����
                break;
            default:
                break;
        }
    }
    //ToDo: SkillFiringSystem�̶� ���� �� �Լ�
    public void GetWeapon(int weaponIndex)
    {
        TransWeaponIndex[weaponIndex] = Weapons.Count;
        Weapon newWeapon = this.gameObject.AddComponent<Weapon>();
        newWeapon.WeaponSetting(weaponIndex);
        Weapons.Add(newWeapon);
        RandomPickUpSystem.UpdateWeaponPickUpList(CharacterStats[(int)Enums.Stat.Luck], this);
    }
    public void UpgradeWeapon(int weaponIndex)
    {
        Weapons[TransWeaponIndex[weaponIndex]].Upgrade();
        if (Weapons[TransWeaponIndex[weaponIndex]].IsMaster())
        {
            MasteredWeapons.Add(weaponIndex);
            RandomPickUpSystem.UpdateWeaponPickUpList(CharacterStats[(int)Enums.Stat.Luck], this);
        }
    }
    public void GetAccessory(int accessoryIndex)
    {
        TransAccessoryIndex[accessoryIndex] = Accessories.Count;
        Accessories.Add(new Accessory(accessoryIndex));
        UpgradeAccessory(accessoryIndex);
        RandomPickUpSystem.UpdateAccessoryPickUpList(CharacterStats[(int)Enums.Stat.Luck], this);
    }
    public void UpgradeAccessory(int accessoryIndex)
    {
        Accessories[TransAccessoryIndex[accessoryIndex]].Upgrade(this);
        if (Accessories[TransAccessoryIndex[accessoryIndex]].IsMaster())
        {
            MasteredAccessories.Add(accessoryIndex);
            RandomPickUpSystem.UpdateAccessoryPickUpList(CharacterStats[(int)Enums.Stat.Luck], this);
        }
    }

    //Get,Set�Լ� �ڵ� ����
    public int Damage
    {
        get { return mDamage; }
        set { mDamage = value; }
    }
    public int ProjectileSpeed
    {
        get { return mProjectileSpeed; }
        set { mProjectileSpeed = value; }
    }
    public int Duration
    {
        get { return mDuration; }
        set { mDuration = value; }
    }
    public int AttackRange
    {
        get { return mAttackRange; }
        set { mAttackRange = value; }
    }
    public int Cooldown
    {
        get { return mCooldown; }
        set { mCooldown = value; }
    }
    public int NumberOfProjectiles
    {
        get { return mNumberOfProjectiles; }
        set { mNumberOfProjectiles = value; }
    }
}

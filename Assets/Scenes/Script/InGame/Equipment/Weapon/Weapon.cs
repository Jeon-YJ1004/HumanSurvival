using UnityEngine;
using System;
using System.Linq;

public class Weapon : MonoBehaviour
{
    public int WeaponIndex;
    public int WeaponLevel = 1;
    public int WeaponMaxLevel;
    public bool bEvolution = false;
    public float[] weaponTotalStats;//Might,Cooldown,ProjectileSpeed, Duration, Amount,AmountLimit,Piercing,Area,MaxLevel

    private float[] WeaponStats;

    public void WeaponDefalutSetting(int weaponIndex=0)
    {
        this.WeaponIndex = weaponIndex;
        this.WeaponStats = Enumerable.Range(0, EquipmentData.defaultWeaponStats.GetLength(1)).Select(x => EquipmentData.defaultWeaponStats[weaponIndex, x]).ToArray();
        WeaponLevel = 1;
        WeaponMaxLevel = (int)WeaponStats[(int)Enums.WeaponStat.MaxLevel];
        weaponTotalStats = WeaponStats;
        AttackCalculation();
    }
    public void Upgrade()
    {
        WeaponLevel++;
        foreach ((var statIndex, var data) in EquipmentData.WeaponUpgrade[WeaponIndex][WeaponLevel])
        {
            WeaponStats[statIndex] += data;
        }
        evolution();
    }
    public bool IsMaster()
    {
        return WeaponLevel == WeaponMaxLevel;
    }
    public bool isEvoluction()
    {
        return bEvolution;
    }
    private void evolution()
    {
        if (!IsMaster())
            return;
        var equipManageSys = GameManager.instance.equipManageSys;
        int evoPairAccIndex = EquipmentData.EvoWeaponNeedAccIndex[WeaponIndex];
        if (evoPairAccIndex < 0)    // 짝이 되는 악세서리의 index = -1 -> 짝이 무기인 경우
            evolutionException(equipManageSys);
        else if (equipManageSys.HasAcc(evoPairAccIndex))
            bEvolution = true;

        if (bEvolution)
            EvolutionProcess();
    }
    private void evolutionException(EquipmentManagementSystem equipManageSys)     // 진화에 필요한 짝이 악세서리가 아닌 무기인 경우(예시 - 비둘기, 흑비둘기)
    {
        var evoPairWeaponIndex = EquipmentData.EvoWeaponNeedWeaponIndex[WeaponIndex];
        if (equipManageSys.HasWeapon(evoPairWeaponIndex) && equipManageSys.Weapons[equipManageSys.TransWeaponIndex[evoPairWeaponIndex]].IsMaster())
            bEvolution = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[evoPairWeaponIndex]].bEvolution = true;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DestructibleObj"))
        {
            if (col.gameObject.TryGetComponent(out DestructibleObject destructible))
            {
                destructible.TakeDamage(weaponTotalStats[(int)Enums.WeaponStat.Might], WeaponIndex);
            }
        }
        if (col.gameObject.tag == "Monster")
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(weaponTotalStats[(int)Enums.WeaponStat.Might], WeaponIndex);
            if(WeaponIndex == 6 && bEvolution)
            {
                GameManager.instance.character.RestoreHealth(1);
                GameManager.instance.EvoGralicRestoreCount++;
                if(GameManager.instance.EvoGralicRestoreCount == 60)
                {
                    GameManager.instance.EvoGralicRestoreCount = 0;
                    weaponTotalStats[((int)Enums.WeaponStat.Might)] += 1;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boundary")
        {
            Destroy(this.gameObject);
        }
    }

    //아래 계산을 한번에 하기
    //ToDo: 레벨업 할때마다 갱신하는 것으로 변경
    private void AttackCalculation()
    {
        DamageCalculation();
        ProjectileSpeedCalculation();
        DurationCalculation();
        AttackRangeCalculation();
        CooldownCalculation();
        CalculateNumberOfProjectiles();
    }
    private void DamageCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Might)] = WeaponStats[((int)Enums.WeaponStat.Might)] * GameManager.instance.CharacterStats[(int)Enums.Stat.Might];
    }
    private void ProjectileSpeedCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)] = WeaponStats[((int)Enums.WeaponStat.ProjectileSpeed)] * GameManager.instance.CharacterStats[(int)Enums.Stat.ProjectileSpeed];
    }
    private void DurationCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Duration)] = WeaponStats[((int)Enums.WeaponStat.Duration)] * GameManager.instance.CharacterStats[(int)Enums.Stat.Duration];
    }
    private void AttackRangeCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Area)] = WeaponStats[((int)Enums.WeaponStat.Area)] * GameManager.instance.CharacterStats[(int)Enums.Stat.Area];
    }
    private void CooldownCalculation()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Cooldown)] = WeaponStats[((int)Enums.WeaponStat.Cooldown)] * GameManager.instance.CharacterStats[(int)Enums.Stat.Cooldown];
    }
    private void CalculateNumberOfProjectiles()
    {
        weaponTotalStats[((int)Enums.WeaponStat.Amount)] = ((int)WeaponStats[((int)Enums.WeaponStat.Amount)]) + GameManager.instance.CharacterStats[(int)Enums.Stat.Amount];
    }
    public float[] WeaponTotalStats { get { return weaponTotalStats; } }
    public virtual void EvolutionProcess() { }
    public virtual void Attack() { }
}

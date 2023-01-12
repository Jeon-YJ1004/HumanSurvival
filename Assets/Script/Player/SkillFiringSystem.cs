using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    //ToDO: ĳ������ ������ ����, ���� ������ �������� ������ ����
    //ĳ������ ������ ��������
    public int damage = 10;              //���ط�
    public int projectileSpeed = 1;     //����ü �ӵ�
    public int duration = 3;            //���� �ð�
    public int attackRange = 1;         //���ݹ���
    public int cooldown = 5;            //��Ÿ��
    public int numberOfProjectiles = 1; //����ü ��
    //���� �±� ��������
    public GameObject monster;
    //ToDo: ���� ����Ʈ ��������� �ٲٱ�
    //���� ��������
    public TestWeapon weapon;
    //����Ʈ ȿ�� ��������
    public GameObject impact;

    private void Start()
    {
        AttackCalculation();
    }
    private void Update()
    {
        Attack();
    }
    //�����ϱ�
    private void Attack() { }
    private void Impact() {
        GameObject obj = Resources.Load<GameObject>("Object/Capsule");
    }
    //�Ʒ� ����� �ѹ��� �ϱ�
    private void AttackCalculation() {
        DamageCalculation();
        ProjectileSpeedCalculation();
        DurationCalculation();
        AttackRangeCalculation();
        CooldownCalculation();
        CalculateNumberOfProjectiles();
    }
    //������ ���
    private void DamageCalculation()
    {
        damage = weapon.Damage * (1 + damage / 100);
    }
    //����ü �ӵ� ���
    private void ProjectileSpeedCalculation()
    {
        //ToDo: ���� �ٲٱ�
        projectileSpeed = weapon.ProjectileSpeed * (1 + projectileSpeed / 100);
    }
    //���ӽð� ���
    private void DurationCalculation()
    {
        //ToDo: ���� �ٲٱ�
        duration = weapon.Duration * (1 + duration / 100);
    }
    //���ݹ��� ���
    private void AttackRangeCalculation()
    {
        //ToDo: ���� �ٲٱ�
        attackRange = weapon.AttackRange * (1 + attackRange / 100);
    }
    //��Ÿ�� ���
    private void CooldownCalculation()
    {
        //ToDo: ���� �ٲٱ�
        cooldown = weapon.Cooldown * (1 + cooldown / 100);
    }
    //����ü �� ���
    private void CalculateNumberOfProjectiles()
    {
        numberOfProjectiles = weapon.NumberOfProjectiles + numberOfProjectiles;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    private int damage;              //���ط�
    private int projectileSpeed;     //����ü �ӵ�
    private int duration;            //���� �ð�
    private int attackRange;         //���ݹ���
    private int cooldown;            //��Ÿ��
    private int numberOfProjectiles; //����ü ��

    public GameObject weapon;    //���� ��������

    float timer = 0;    //�ð�
    void Update()
    {
        Attack();
    }

    //�����ϱ�
    private void Attack() 
    {
        AttackCalculation();    //���� ���� ���
        for (int i = 0; i <= numberOfProjectiles; i++)  //����ü ����ŭ �߻��ϱ�
        {
            FireWeapon();
        }
    }
    //���� �߻�
    //ToDo: attackRange�� �����ϱ�
    private void FireWeapon()
    {
        float timediff = cooldown;  //��Ÿ��
        timer += Time.deltaTime;    //�ð� ����
        if (timer > timediff)   //��Ÿ�� ���� ��
        {
            GameObject newobs = Instantiate(weapon);  //���� �ε�
            newobs.transform.position = GameManager.instance.player.transform.position;  //ĳ���� ��ġ�� ����
            newobs.transform.parent = transform;
            newobs.GetComponent<Weapon>().Shoot(projectileSpeed);  //������ ���ͷ� ���ư�
            timer = 0;  //�ð� �ʱ�ȭ
            Destroy(newobs, duration);  //���� �ð� ������ ����
        }
    }
    //�Ʒ� ����� �ѹ��� �ϱ�
    private void AttackCalculation()
    {
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
        damage = weapon.GetComponent<Weapon>().Damage * (1 + GameManager.instance.player.GetComponent<Character>().Damage / 100);
    }
    //����ü �ӵ� ���
    private void ProjectileSpeedCalculation()
    {
        projectileSpeed = weapon.GetComponent<Weapon>().ProjectileSpeed * (1 + GameManager.instance.player.GetComponent<Character>().ProjectileSpeed / 100);
    }
    //���ӽð� ���
    private void DurationCalculation()
    {
        duration = weapon.GetComponent<Weapon>().Duration * (1 + GameManager.instance.player.GetComponent<Character>().Duration / 100);
    }
    //���ݹ��� ���
    private void AttackRangeCalculation()
    {
        attackRange = weapon.GetComponent<Weapon>().AttackRange * (1 + GameManager.instance.player.GetComponent<Character>().AttackRange / 100);
    }
    //��Ÿ�� ���
    private void CooldownCalculation()
    {
        cooldown = weapon.GetComponent<Weapon>().Cooldown * (1 + GameManager.instance.player.GetComponent<Character>().Cooldown / 100);
    }
    //����ü �� ���
    private void CalculateNumberOfProjectiles()
    {
        numberOfProjectiles = weapon.GetComponent<Weapon>().NumberOfProjectiles + GameManager.instance.player.GetComponent<Character>().NumberOfProjectiles;
    }
}
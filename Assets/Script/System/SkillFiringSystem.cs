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

    public GameObject character;    //ĳ���� ���Ȱ� ��ġ ��������
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

        GameObject monster = GameObject.FindWithTag("Monster");

        if (timer > timediff)   //��Ÿ�� ���� ��
        {
            GameObject newobs = Instantiate(weapon);  //���� �ε�
            newobs.transform.position = character.transform.position;  //ĳ���� ��ġ�� ����
            newobs.GetComponent<Weapon>().Shoot(projectileSpeed);  //������ ���ͷ� ���ư�
            if (OnTriggerEnter2D(weapon.GetComponent<Collider2D>()))    //���Ⱑ ���Ϳ� �ε��� ����
            {
                monster.GetComponent<Monster>().Health -= damage;   //�� ���
                Destroy(monster, 0);    //���� ����
                if (monster.GetComponent<Monster>().Health <= 0)    //���Ͱ� �״´ٸ�
                {
                    GameObject obj = Resources.Load<GameObject>("Object/Capsule");  //����Ʈ ����
                    Destroy(obj, 1);    //����Ʈ ���� �ð�
                }
            }
            timer = 0;  //�ð� �ʱ�ȭ
            Destroy(newobs, duration);  //���� �ð� ������ ����
        }
    }
    private bool OnTriggerEnter2D(Collider2D weapon)
    //rigidBody�� ���𰡿� �浹�Ҷ� ȣ��Ǵ� �Լ��� Collider2D other�� �ε��� ��ü�� �޾ƿɴϴ�.
    {
        if (weapon.gameObject.tag.Equals("Monster")) //�ε��� ��ü�� �±׸� ���ؼ� ������ �Ǵ��մϴ�.
        { return true; }
        else { return false; }
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
        damage = weapon.GetComponent<Weapon>().Damage * (1 + character.GetComponent<Character>().Damage / 100);
    }
    //����ü �ӵ� ���
    private void ProjectileSpeedCalculation()
    {
        projectileSpeed = weapon.GetComponent<Weapon>().ProjectileSpeed * (1 + character.GetComponent<Character>().ProjectileSpeed / 100);
    }
    //���ӽð� ���
    private void DurationCalculation()
    {
        duration = weapon.GetComponent<Weapon>().Duration * (1 + character.GetComponent<Character>().Duration / 100);
    }
    //���ݹ��� ���
    private void AttackRangeCalculation()
    {
        attackRange = weapon.GetComponent<Weapon>().AttackRange * (1 + character.GetComponent<Character>().AttackRange / 100);
    }
    //��Ÿ�� ���
    private void CooldownCalculation()
    {
        cooldown = weapon.GetComponent<Weapon>().Cooldown * (1 + character.GetComponent<Character>().Cooldown / 100);
    }
    //����ü �� ���
    private void CalculateNumberOfProjectiles()
    {
        numberOfProjectiles = weapon.GetComponent<Weapon>().NumberOfProjectiles + character.GetComponent<Character>().NumberOfProjectiles;
    }
}
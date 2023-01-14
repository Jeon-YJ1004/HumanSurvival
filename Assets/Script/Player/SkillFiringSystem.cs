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
    //ToDo: ���� ����Ʈ ��������� �ٲٱ�
    public GameObject weapon;    //���� ��������
    public GameObject monster;    //���� �±� ��������

    private void Update()
    {
        Attack();
    }
    //�����ϱ�
    private void Attack() 
    {
        AttackCalculation();
        for (int i = 0; i <= numberOfProjectiles; i++)
        {
            FireWeapon();
        }
    }
    //���� �߻�
    private void FireWeapon()
    {
        float timer = 0;    //�ð�
        float timediff = cooldown;  //��Ÿ��

        timer += Time.deltaTime;    //�ð� ����
        if (timer > timediff)   //��Ÿ�� ���� ��
        {
            GameObject newobs = Instantiate(weapon.GetComponent<Weapon>().weaponType);  //���� �ε�

            newobs.transform.position = new Vector2(character.GetComponent<PlayerMovement>().Movement.x, character.GetComponent<PlayerMovement>().Movement.y);  //ĳ���� ��ġ�� ����
            transform.position = Vector2.right * projectileSpeed * Time.deltaTime;
            timer = 0;
            Destroy(newobs, duration);
        }
        if (OnTriggerEnter2D(weapon.GetComponent<Collider2D>()))    //���Ⱑ ���Ϳ� �ε��� ����
        {
            monster.GetComponent<Monster>().Health -= damage;
            if (true)    //���Ͱ� �״´ٸ�
            {
                GameObject obj = Resources.Load<GameObject>("Object/Capsule");  //�Ӻ�Ʈ ����
            }
        }
    }

    bool OnTriggerEnter2D(Collider2D other)
    //rigidBody�� ���𰡿� �浹�Ҷ� ȣ��Ǵ� �Լ��� Collider2D other�� �ε��� ��ü�� �޾ƿɴϴ�.
    {
        if (other.gameObject.tag.Equals("Monster")) //�ε��� ��ü�� �±׸� ���ؼ� ������ �Ǵ��մϴ�.
        {
            //�� ��� �� ����
            return false;
        }
        else { return true; }
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
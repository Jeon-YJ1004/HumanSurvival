using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterInfo : MonoBehaviour, IPointerEnterHandler
{
    public int character;   //ĳ���� ���� ��ȣ
    public Image infoImage; //����� �̹���
    public Image buttonIamge;   //��ư �̹���
    public TMP_Text characterName;  //ĳ���� �̸�
    public TMP_Text characterName1;  //ĳ���� �̸�
    public TMP_Text characterExplain;   //ĳ���� ����
    public TMP_Text characterMaxStamina;    //ĳ���� �ִ� ü��
    public TMP_Text characterRecovery;  //ĳ���� ȸ��
    public TMP_Text characterDefense;   //ĳ���� ����
    public TMP_Text characterSpeed; //ĳ���� �̵� �ӵ�
    public TMP_Text characterDamage;    //ĳ���� ������
    public TMP_Text characterProjectileSpeed;   //ĳ���� ����ü �ӵ�
    public TMP_Text characterDuration;  //ĳ���� ���� �ð�
    public TMP_Text characterAttackRange;   //ĳ���� ���� ����
    public TMP_Text characterCooldown;  //ĳ���� ��Ÿ��
    public TMP_Text characterNumberOfProjectiles;   //ĳ���� ����ü ��
    public TMP_Text characterMagnet;    //ĳ���� �ڼ�
    public TMP_Text characterLuck;  //ĳ���� ���
    public TMP_Text characterGrowth;    //ĳ���� ����


    string Name;    //ĳ���� �̸� ��
    string explain; //ĳ���� ���� ��
    float maxStamina;  //ĳ���� �ִ� ü��
    float recovery;    //ĳ���� ȸ��
    float defense; //ĳ���� ����
    float speed;   //ĳ���� �̵� �ӵ�
    float damage;  //ĳ���� ������
    float projectileSpeed; //ĳ���� ����ü �ӵ�
    float duration;    //ĳ���� ���� �ð�
    float attackRange; //ĳ���� ���� ����
    float cooldown;    //ĳ���� ��Ÿ��
    int numberOfProjectiles; //ĳ���� ����ü ��
    float magnet;   //ĳ���� �ڼ�
    float luck;    //ĳ���� ���
    float growth;  //ĳ���� ����

    void Start()
    {
        switch (character)
        {
            case 1:
                this.Name = "ĳ���� �̸�1";
                this.explain = "ĳ���� ����1";
                this.maxStamina = 101;
                this.recovery = (float)0.1;
                this.defense = 1;
                this.speed = 11;
                this.damage = 11;
                this.projectileSpeed = 11;
                this.duration = 11;
                this.attackRange = 11;
                this.cooldown = -1;
                this.numberOfProjectiles = 1;
                this.magnet = 1;
                this.luck = 11;
                this.growth = 11;
                break;

            case 2:
                this.Name = "ĳ���� �̸�2";
                this.explain = "ĳ���� ����2";
                this.maxStamina = 102;
                this.recovery = (float)0.2;
                this.defense = 2;
                this.speed = 12;
                this.damage = 12;
                this.projectileSpeed = 12;
                this.duration = 12;
                this.attackRange = 12;
                this.cooldown = -2;
                this.numberOfProjectiles = 2;
                this.magnet = 2;
                this.luck = 12;
                this.growth = 12;
                break;

            case 3:

                this.Name = "ĳ���� �̸�3";
                this.explain = "ĳ���� ����3";
                this.maxStamina = 103;
                this.recovery = (float)0.3;
                this.defense = 3;
                this.speed = 13;
                this.damage = 13;
                this.projectileSpeed = 13;
                this.duration = 13;
                this.attackRange = 13;
                this.cooldown = -3;
                this.numberOfProjectiles = 3;
                this.magnet = 3;
                this.luck = 13;
                this.growth = 13;
                break;

            case 4:
                this.Name = "ĳ���� �̸�4";
                this.explain = "ĳ���� ����4";
                this.maxStamina = 104;
                this.recovery = (float)0.4;
                this.defense = 4;
                this.speed = 14;
                this.damage = 14;
                this.projectileSpeed = 14;
                this.duration = 14;
                this.attackRange = 14;
                this.cooldown = -4;
                this.numberOfProjectiles = 4;
                this.magnet = 4;
                this.luck = 14;
                this.growth = 14;
                break;

            case 5:
                this.Name = "ĳ���� �̸�5";
                this.explain = "ĳ���� ����5";
                this.maxStamina = 105;
                this.recovery = (float)0.5;
                this.defense = 5;
                this.speed = 15;
                this.damage = 15;
                this.projectileSpeed = 15;
                this.duration = 15;
                this.attackRange = 15;
                this.cooldown = -5;
                this.numberOfProjectiles = 5;
                this.magnet = 5;
                this.luck = 15;
                this.growth = 15;
                break;

            case 6:
                this.Name = "ĳ���� �̸�6";
                this.explain = "ĳ���� ����6";
                this.maxStamina = 106;
                this.recovery = (float)0.6;
                this.defense = 6;
                this.speed = 16;
                this.damage = 16;
                this.projectileSpeed = 16;
                this.duration = 16;
                this.attackRange = 16;
                this.cooldown = -6;
                this.numberOfProjectiles = 6;
                this.magnet = 6;
                this.luck = 16;
                this.growth = 16;
                break;

            case 7:
                this.Name = "ĳ���� �̸�7";
                this.explain = "ĳ���� ����7";
                this.maxStamina = 107;
                this.recovery = (float)0.7;
                this.defense = 7;
                this.speed = 17;
                this.damage = 17;
                this.projectileSpeed = 17;
                this.duration = 17;
                this.attackRange = 17;
                this.cooldown = -7;
                this.numberOfProjectiles = 7;
                this.magnet = 7;
                this.luck = 17;
                this.growth = 17;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //������ �ؽ�Ʈ ����
        characterName.text = this.Name;
        characterName1.text = this.Name;
        characterExplain.text = this.explain;
        characterMaxStamina.text = "Max stamina" + this.maxStamina.ToString();
        characterRecovery.text = "Recovery" + this.recovery.ToString();
        characterDefense.text = "Defense" + this.defense.ToString();
        characterSpeed.text = "Speed" + this.speed.ToString() + "%";
        characterDamage.text = "Damage" + this.damage.ToString() + "%";
        characterProjectileSpeed.text = "Projectile Speed" + this.projectileSpeed.ToString() + "%";
        characterDuration.text = "Duration" + this.duration.ToString() + "%";
        characterAttackRange.text = "AttackRange" + this.attackRange.ToString() + "%";
        characterCooldown.text = "Cooldown" + this.cooldown.ToString() + "%";
        characterNumberOfProjectiles.text = "Number of projectiles" + this.numberOfProjectiles.ToString();
        characterMagnet.text = "Magnet" + this.magnet.ToString() + "%";
        characterLuck.text = "Luck" + this.luck.ToString() + "%";
        characterGrowth.text = "Growth" + this.growth.ToString() + "%";
        infoImage.GetComponent<Image>().sprite = buttonIamge.GetComponent<Image>().sprite;  //�̹��� ����
    }

}


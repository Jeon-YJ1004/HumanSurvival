using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StageInfo : MonoBehaviour, IPointerEnterHandler
{
    public int stage;   //ĳ���� ���� ��ȣ
    public TMP_Text stageName;  //�������� �̸�
    public TMP_Text stageTime;   //�������� �÷��� �ð�
    public TMP_Text stageDoubleSpeed;   //�������� ���
    public TMP_Text stageGoldCoinBonus;   //�������� ��� ���ʽ�
    public TMP_Text stageLuckBonus;   //�������� ��� ���ʽ�
    public TMP_Text stageExperienceBonus;   //�������� ����ġ ���ʽ�

    string Name;    //�������� �̸� ��
    string time;    //�÷��� �ð�
    int doubleSpeed;    //�ð� ���
    int goldCoinBonus;  //���ʽ� ���
    int luckBonus;  //���ʽ� ���
    int experienceBonus;    //���ʽ� ����ġ

    void Start()
    {
        switch (stage)
        {
            case 1:
                this.Name = "Stage 1";
                this.time = "30:00";
                this.doubleSpeed = 1;
                this.goldCoinBonus = 1;
                this.luckBonus = 1;
                this.experienceBonus = 1;
                break;

            case 2:
                this.Name = "Stage 2";
                this.time = "30:00";
                this.doubleSpeed = 1;
                this.goldCoinBonus = 1;
                this.luckBonus = 1;
                this.experienceBonus = 1;
                break;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        stageName.text = this.Name;
        stageTime.text = "Play time" + this.time;
        stageDoubleSpeed.text = "Gold Coin Bonus" + this.doubleSpeed.ToString();
        stageGoldCoinBonus.text = "Gold Coin Bonus" + this.goldCoinBonus.ToString();
        stageLuckBonus.text = "Luck Bonus" + this.luckBonus.ToString();
        stageExperienceBonus.text = " Experience Bonus" + this.experienceBonus.ToString();
    }
}

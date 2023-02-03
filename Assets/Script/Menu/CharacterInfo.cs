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
    public TMP_Text characterExplain;   //ĳ���� ����

    string Name;    //ĳ���� �̸� ��
    string explain; //ĳ���� ���� ��

    void Start()
    {
        switch (character)
        {
            case 1:
                this.Name = "ĳ���� �̸�1";
                this.explain = "ĳ���� ����1";
                break;

            case 2:
                this.Name = "ĳ���� �̸�2";
                this.explain = "ĳ���� ����2";
                break;

            case 3:

                this.Name = "ĳ���� �̸�3";
                this.explain = "ĳ���� ����3";
                break;

            case 4:
                this.Name = "ĳ���� �̸�4";
                this.explain = "ĳ���� ����4";
                break;

            case 5:
                this.Name = "ĳ���� �̸�5";
                this.explain = "ĳ���� ����5";
                break;

            case 6:
                this.Name = "ĳ���� �̸�6";
                this.explain = "ĳ���� ����6";
                break;

            case 7:
                this.Name = "ĳ���� �̸�7";
                this.explain = "ĳ���� ����7";
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        characterName.text = this.Name;
        characterExplain.text = this.explain;
        infoImage.GetComponent<Image>().sprite = buttonIamge.GetComponent<Image>().sprite;
    }

}


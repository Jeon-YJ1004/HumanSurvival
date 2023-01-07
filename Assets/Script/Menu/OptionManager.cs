using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject DefaultPanel; //OptionBackground
    public GameObject FirstPanel;   //OptionMenu_1Page
    public GameObject SecondPanel;  //OptionMenu_2Page
    public GameObject ThirdPanel;   //DataRecovery
    public Text buttontext; //DataRecovery�ؽ�Ʈ
    //Panel�ʱ�ȭ
    private void Awake()
    {
        DefaultPanel.SetActive(true);
        FirstPanel.SetActive(true);
        SecondPanel.SetActive(false);
        ThirdPanel.SetActive(false);
    }

    //�ڷΰ��� ��ư
    public void ClickBackButton()
    {
        //TODO: �� ���¿��� back ��ư�� ������ ����ȭ������ �����Ѵ�.
        /*
        if (FirstPanel.activeSelf == true & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == false) { }
        */
        //�ι�° ���������� ù��° �������� �̵�
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == true & ThirdPanel.activeSelf == false)
        {
            SecondPanel.SetActive(false);
            FirstPanel.SetActive(true);
        }
        //DataRecovery���� ù��° �������� �̵�
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == true)
        {
            ThirdPanel.SetActive(false);
            FirstPanel.SetActive(true);
            buttontext.GetComponent<Text>().text = "data recovery";
        }
        //�׿ܴ� button�� onClick���� ���
    }
}

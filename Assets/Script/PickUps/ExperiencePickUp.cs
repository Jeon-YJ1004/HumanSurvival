using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickUp : MonoBehaviour,ICollectible
{
    public float expGranted;

    public void Collect()
    {
        //��ũ��Ʈ ������ ������Ʈ ã��
        Character character = GameManager.instance.character;
        //Todo : character grouth stat 
        character.GetExp(expGranted);
        gameObject.SetActive(false);

        ;
    }
}

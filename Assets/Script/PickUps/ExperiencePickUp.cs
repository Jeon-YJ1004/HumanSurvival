using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickUp : MonoBehaviour,ICollectible
{
    public int expGranted;

    public void Collect()
    {
        //��ũ��Ʈ ������ ������Ʈ ã��
        Character character = FindObjectOfType<Character>();
        character.GetExp(expGranted);
;    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int health = 100; //���� ü��
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}

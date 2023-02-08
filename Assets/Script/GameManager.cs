using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f; //20��

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int coin;
    //public int[] nextExp = { };// �������ϴ� ����ġ

    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerMovement player;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {

        gameTime += Time.deltaTime;
        if (gameTime >maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

}

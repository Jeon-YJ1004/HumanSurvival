using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionTile : MonoBehaviour
{
    public int x;   //Ÿ�� ���� ũ��
    public int y;   //Ÿ�� ���� ũ��
    // ��ũ Area���� �浹���� ����� ���� �ҷ����� �Լ�
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) { return; }  //���� ����

        Vector3 playerPos = GameManager.instance.player.transform.position; //���ΰ� ��ġ
        Vector3 myPos = transform.position; //���� Tilemap ��ġ
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.movement;  //���ΰ� �̵����� ����
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)  //x������ ���� �̵���
                {
                    transform.Translate(Vector3.right * dirX * x * 2); //���ΰ� �̵� ���� �տ� tilemap�� ���� ���� x*2 ��ŭ �̵�
                }
                else if (diffX < diffY) //y������ ���� �̵���
                {
                    transform.Translate(Vector3.up * dirY * y * 2); //���ΰ� �̵� ���� �տ� tilemap�� ���� ���� y*2 ��ŭ �̵�
                }
                break;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;
    bool isLive = true;

    Rigidbody2D rb;
    SpriteRenderer spriter;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        //���Ͱ� ��� ���� ���� �����̵��� 
        if (!isLive) return;

        Vector2 direction = (target.position - rb.position).normalized;
        Vector2 nextVec = direction * speed * Time.fixedDeltaTime; ;

        //�÷��̾��� Ű�Է� ���� ���� �̵�=������ ���� ���� ���� �̵�
        rb.MovePosition(rb.position + nextVec);

        //���� �ӵ��� �̵��� ������ ���� �ʵ��� �ӵ� ����
        rb.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        //Ÿ���� x��� ���Ͽ� sprite flip 
        spriter.flipX = target.position.x < rb.position.x;
    }
    private void OnEnable()
    {
        //prefeb�� scene�� object�� ������ �� ����=> ������ ������ ������ �ʱ�ȭ�ϱ�
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

}

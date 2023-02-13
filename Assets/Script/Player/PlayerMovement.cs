using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;    //�Է°�
    private Vector2 clickTarget;    //���콺 Ŭ��
    private float moveSpeed = 8f;   //�ӵ�
    bool moving;

    [SerializeField] Rigidbody2D rb;    //�������ٵ�
    [SerializeField] SpriteRenderer spriter;    //��������Ʈ
    [SerializeField] Animator animator;  //�ִϸ��̼�
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        clickTarget = transform.position;
    }
    void Update()
    {
        // click �̺�Ʈ
        if (Input.GetMouseButtonDown(0))
        {
            clickTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animator.SetBool("Moving", true);
            moving = true;
        }
    }
    private void FixedUpdate()//���� ��� �� �� ���
    {
        //movement ����
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);  //���� �� ������ ���� �ð�

        //click �� movement �ڵ�
        if (moving && rb.position != clickTarget)
        {
            float step = moveSpeed * Time.fixedDeltaTime;
            rb.position = Vector2.MoveTowards(rb.position, clickTarget, step);
        }
        else
        {
            animator.SetBool("Moving", false);
            moving = false;
        }
    }

    private void LateUpdate()   //��� Update �Լ��� ȣ��� ��, ���������� ȣ��Ǵ� �Լ�
    {
        //Ű����� ������ Ȯ��
        if (movement.magnitude != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        animator.SetFloat("Speed", movement.magnitude);

        if (movement.x != 0)    //x�� �Է°��� �ִ� ���
        {
            spriter.flipX = movement.x < 0; //���� ������
        }
    }
    private void OnMove(InputValue value)   //InputSystem���� Ű�Է��� �޴� �Լ�
    {
        movement = value.Get<Vector2>();
    }
    public Vector2 Movement {
        get { return movement; }
    }
}

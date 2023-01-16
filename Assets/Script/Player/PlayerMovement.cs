using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 8f;
    private Vector2 clickTarget;
    private Vector2 relativePos;
    private Vector2 movement;
    private float horizontal;
    private float vertical;
    bool moving;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriter;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        clickTarget = transform.position;
    }
    void Update()
    {
        //Ű input �ڵ�

        movement.x = Input.GetAxisRaw("Horizontal"); //Ű �Է�
        movement.y = Input.GetAxisRaw("Vertical"); 
        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Speed", movement.sqrMagnitude); //���� üũ��
        if (horizontal != 0||vertical !=0||moving) animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);
        // ���콺 click �ڵ�
        if (Input.GetMouseButtonDown(0))
        {
            clickTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }
        
        relativePos = new Vector2(
             clickTarget.x - rb.position.x,
             clickTarget.y - rb.position.y);
    }
    private void FixedUpdate()//���� ��� �� �� ���
    {
        //movement �ڵ�
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);//���� �� ������ ���� �ð�

        //click �� movement �ڵ�
        if (moving && (Vector2)rb.position != clickTarget)
        {
            animator.SetBool("Moving", true);
            float step = moveSpeed * Time.fixedDeltaTime;
            rb.position = Vector2.MoveTowards(rb.position, clickTarget, step);
        }
        else
        {
            animator.SetBool("Moving", false);
            moving = false;
        }
        
       
    }
    private void LateUpdate()
    {
        
        if (horizontal != 0||relativePos.x<0)
        {
            spriter.flipX = horizontal < 0;
        }
    }
}


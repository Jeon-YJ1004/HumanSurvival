using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float enemyDamage;
    public int exp;
    public RuntimeAnimatorController[] animcon;
    public Rigidbody2D target;
    //GameObject targetGameobject;

    bool isLive ;

    Rigidbody2D rb;
    Collider2D coll;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();

    }
    void FixedUpdate()
    {
        //몬스터가 살아 있을 때만 움직이도록 
        if (!isLive||anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 direction = (target.position - rb.position).normalized;
        Vector2 nextVec = direction * speed * Time.fixedDeltaTime; ;

        //플레이어의 키입력 값을 더한 이동=몬스터의 방향 값을 더한 이동
        rb.MovePosition(rb.position + nextVec);

        //물리 속도가 이동에 영향을 주지 않도록 속도 제거
        rb.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        //타겟의 x축과 비교하여 sprite flip 
        spriter.flipX = target.position.x < rb.position.x;
    }
    private void OnEnable()
    {
        //prefeb은 scene의 object에 접근할 수 없다=> 생성될 때마다 변수를 초기화하기
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();

        //활성화 될때 변수 초기화
        isLive = true;
        health = maxHealth;
        coll.enabled=true;
        rb.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);

    }

    public void Init(SpawnData data)  //각각의 몬스터 데이터 설정 함수
    {
        anim.runtimeAnimatorController = animcon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    void Dead()
    {
        // object 비활성화
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!collision.CompareTag("Weapon")) return;
        health -= 10;
        //health -= collision.GetComponent<Weapon>().damage;
        StartCoroutine(KnockBack());
        if (health > 0)
        {
            anim.SetTrigger("Hit");
            Debug.Log("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rb.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.exp+=exp;
            Dead();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rb.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
}

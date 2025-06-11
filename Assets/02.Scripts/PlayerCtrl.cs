using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    private float jumpForce = 700f;
    private int jumpCount = 0;
    private bool isDead = false;
    private bool isGrounded = false;

    private readonly string dieAnim = "Die";
    private readonly string deadTag = "Dead";
    private readonly int hashIsGround = Animator.StringToHash("IsGround");
    void Start()
    {
        source = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        deathClip = Resources.Load("Sounds/die") as AudioClip;
    }


    void Update()
    {
        if (isDead) return;
        if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && jumpCount <2)
        {
            jumpCount++;
            rb2d.velocity = Vector2.zero; // 점프 직전에 속도를 순간적으로 0으로 초기화
            rb2d.AddForce(new Vector2(0, jumpForce)); // 위쪽으로 힘을 가함
            source.Play(); // 점프 사운드 재생
        }
        else if(Input.GetMouseButtonUp(0) && rb2d.velocity.y > 0)
        {
            // 왼쪽 마우스 클릭 버튼을 떼면서 속도의 y값이 양수라면(위로 올라가는 중이라면) 
            // y값을 줄여서 속도를 반으로 줄임 -> 왼쪽 마우스 클릭을 짧게 누르면 절반만 뛴다는 의미 
            rb2d.velocity = rb2d.velocity * 0.5f; // 속도를 반으로 줄임
        }
        animator.SetBool(hashIsGround, isGrounded);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7) // 표면의 노말벡터 위쪽으로 향하는지 확인 
        {
            isGrounded = true;
            jumpCount = 0; // 땅에 닿으면 점프 횟수 초기화
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(deadTag) && !isDead)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetTrigger(dieAnim);
        source.clip = deathClip;
        source.Play();
        rb2d.velocity = Vector2.zero; // 죽었을 때 속도를 0으로 초기화
        isDead = true;
        GameManager.instance.OnPlayerDead(); // 게임 매니저에 플레이어 죽음 알림
    }
}


 



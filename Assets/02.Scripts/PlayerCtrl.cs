using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    private float jumpForce = 400f;
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

        switch(Application.platform)
        {
            // ������ �����Ϳ��� ���콺 Ŭ�� �Ǵ� �����̽��� �Է�
            case RuntimePlatform.WindowsEditor:
                WindowTest();
                break;
            // ����� �÷������� ��ġ �Է�
            case RuntimePlatform.Android:
                AndroidInput();
                break;
        }
    }

    private void AndroidInput()
    {
        // ����� ��ġ �Է�
        if (Input.touchCount > 0)
        {
            // ù��° ��ġ ����(��ġ�� ����)�� ������
            Touch touch = Input.GetTouch(0);
            // ��ġ�� ���۵Ǿ���, ���� Ƚ���� 2 �̸��� ���
            if (touch.phase == TouchPhase.Began && jumpCount < 2)
            {
                jumpCount++;
                rb2d.velocity = Vector2.zero; // ���� ������ �ӵ��� ���������� 0���� �ʱ�ȭ
                rb2d.AddForce(new Vector2(0, jumpForce)); // �������� ���� ����
                source.Play(); // ���� ���� ���
            }

            else if (touch.phase == TouchPhase.Ended && rb2d.velocity.y > 0)
            {
                // ��ġ�� �����鼭 �ӵ��� y���� ������(���� �ö󰡴� ���̶��) 
                // y���� �ٿ��� �ӵ��� ������ ���� -> ª�� ������ ���ݸ� �ڴٴ� �ǹ� 
                rb2d.velocity = rb2d.velocity * 0.5f; // �ӵ��� ������ ����
            }
        }
    }

    private void WindowTest()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && jumpCount < 2)
        {
            jumpCount++;
            rb2d.velocity = Vector2.zero; // ���� ������ �ӵ��� ���������� 0���� �ʱ�ȭ
            rb2d.AddForce(new Vector2(0, jumpForce)); // �������� ���� ����
            source.Play(); // ���� ���� ���
        }
        else if (Input.GetMouseButtonUp(0) && rb2d.velocity.y > 0)
        {
            // ���� ���콺 Ŭ�� ��ư�� ���鼭 �ӵ��� y���� ������(���� �ö󰡴� ���̶��) 
            // y���� �ٿ��� �ӵ��� ������ ���� -> ���� ���콺 Ŭ���� ª�� ������ ���ݸ� �ڴٴ� �ǹ� 
            rb2d.velocity = rb2d.velocity * 0.5f; // �ӵ��� ������ ����
        }
        animator.SetBool(hashIsGround, isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7) // ǥ���� �븻���� �������� ���ϴ��� Ȯ�� 
        {
            isGrounded = true;
            jumpCount = 0; // ���� ������ ���� Ƚ�� �ʱ�ȭ
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
        rb2d.velocity = Vector2.zero; // �׾��� �� �ӵ��� 0���� �ʱ�ȭ
        isDead = true;
        GameManager.instance.score = 0; // ���� �ʱ�ȭ
        GameManager.instance.OnPlayerDead(); // ���� �Ŵ����� �÷��̾� ���� �˸�
    }
}


 



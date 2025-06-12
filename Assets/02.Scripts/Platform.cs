using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles = new GameObject[3]; // ��ֹ� ������Ʈ �迭, 3���� ��ֹ��� ����
    private bool isStepped; // �÷��̾ ������ ��Ҵ��� ���� ����

    private readonly string playerTag = "Player"; // �÷��̾� �±�
    private void OnEnable() // ������Ʈ�� Ȱ��ȭ �ɶ� ȣ�� // ���۽� Awake -> OnEnable -> start ������ ȣ��
    {                       // Ȱ��ȭ ��Ȱ��ȭ �ݺ��Ҷ����� ȣ�� 
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i] = transform.GetChild(i).gameObject; // �ڽ� ������Ʈ�� ��ֹ��� ����
         
        }
        isStepped = false;
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true); // 1/3 Ȯ���� ��ֹ� Ȱ��ȭ
           
            }
            else
            {
                obstacles[i].SetActive(false); // ������ 2/3 Ȯ���� ��ֹ� ��Ȱ��ȭ
            }
        }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(playerTag) && !isStepped)
        {
            isStepped = true;
            GameManager.instance.AddScore(1); // �÷��̾ ������ ����� �� ���� �߰�
        }
    }
}

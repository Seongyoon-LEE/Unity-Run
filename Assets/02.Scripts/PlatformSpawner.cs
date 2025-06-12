using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1.����������
// 2.���������� 3��
// 3.��ġ �ð� ���� ��ġ �ð� ���� �ּҰ� �ִ밪
// 4.������ �������� ������ �ּ� Y��ǥ, �ִ� Y��ǥ, X��ǥ
// 5.������ ������ x��ǥ
// 6.�̸� ������ ���ǵ�
// 7.���� Ȱ��ȭ�� ������ �ε���
// 8.Ǯ ��ġ
// 9.������ ���� ���� �ð�
public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // ���� ������
    public int count = 3; // ������ ������ ����
    public float timeBetSpawnMin = 1.25f; // ���� ���� ���� �ּ� �ð�
    public float timeBetSpawnMax = 2.25f; // ���� ���� ���� �ּ� �ð�
    private float timeBetSpawn; // ���� ���� ���� �ð�

    public float yMin = -3.5f; 
    public float yMax = 1.5f; // ���� ���� ��ġ�� Y�� ����
    private float xPos = 20f; // ���� ���� ��ġ�� X�� ��ǥ
    private GameObject[] platforms; // �̸� ������ ���ǵ�
    private int currentIndex = 0; // ���� Ȱ��ȭ�� ���� �ε���
    private Vector2 poolPosition = new Vector2(0f, -25f); // 
    private float lastSpawnTime; // ������ ���� ���� �ð�

  
    void Start()
    {
        platformPrefab = Resources.Load("Prefabs/Platform") as GameObject; // ���� ������ �ε�
        platforms = new GameObject[count];
        for(int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity); // ���� ����
        }
        lastSpawnTime = 0f; // ������ ��ġ ���� �ʱ�ȭ
        timeBetSpawn = 0; // ������ ��ġ������ �ð� ������ 0���� �ʱ�ȭ
    }


    void Update()
    {
        if (GameManager.instance.isGameOver) return;
        if (Time.time >= lastSpawnTime + timeBetSpawn) // ���� �ð��� ������ ��ġ ������ ���̰� ���� ��ġ �ð����� ũ��
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); // ���� ��ġ �ð� ������ �������� ����
            float yPos = Random.Range(yMin, yMax); // Y��ǥ�� �������� ����
            platforms[currentIndex].SetActive(false); // ������Ʈ ���� OnDisable ȣ��
            platforms[currentIndex].SetActive(true); // �ٽ� �Ѹ� OnEnable ȣ��
            
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos); // ���� ��ġ ����
            currentIndex++;
            if (currentIndex >= count)
                currentIndex = 0;
        }

    }
}

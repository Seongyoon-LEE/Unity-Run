using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1.발판프리팹
// 2.발판프리팹 3개
// 3.배치 시간 다음 배치 시간 간격 최소값 최대값
// 4.발판이 랜덤으로 생성될 최소 Y좌표, 최대 Y좌표, X좌표
// 5.발판이 생성될 x좌표
// 6.미리 생성한 발판들
// 7.현재 활성화된 발판의 인덱스
// 8.풀 위치
// 9.마지막 발판 생성 시간
public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // 발판 프리팹
    public int count = 3; // 생성할 발판의 개수
    public float timeBetSpawnMin = 1.25f; // 다음 발판 생성 최소 시간
    public float timeBetSpawnMax = 2.25f; // 다음 발판 생성 최소 시간
    private float timeBetSpawn; // 다음 발판 생성 시간

    public float yMin = -3.5f; 
    public float yMax = 1.5f; // 발판 생성 위치의 Y축 범위
    private float xPos = 20f; // 발판 생성 위치의 X축 좌표
    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 현재 활성화된 발판 인덱스
    private Vector2 poolPosition = new Vector2(0f, -25f); // 
    private float lastSpawnTime; // 마지막 발판 생성 시간

  
    void Start()
    {
        platformPrefab = Resources.Load("Prefabs/Platform") as GameObject; // 발판 프리팹 로드
        platforms = new GameObject[count];
        for(int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity); // 발판 생성
        }
        lastSpawnTime = 0f; // 마지막 배치 시점 초기화
        timeBetSpawn = 0; // 다음번 배치까지의 시간 간격을 0으로 초기화
    }


    void Update()
    {
        if (GameManager.instance.isGameOver) return;
        if (Time.time >= lastSpawnTime + timeBetSpawn) // 현재 시간과 마지막 배치 시점의 차이가 다음 배치 시간보다 크면
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); // 다음 배치 시간 간격을 랜덤으로 설정
            float yPos = Random.Range(yMin, yMax); // Y좌표를 랜덤으로 설정
            platforms[currentIndex].SetActive(false); // 오브젝트 끄면 OnDisable 호출
            platforms[currentIndex].SetActive(true); // 다시 켜면 OnEnable 호출
            
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos); // 발판 위치 설정
            currentIndex++;
            if (currentIndex >= count)
                currentIndex = 0;
        }

    }
}

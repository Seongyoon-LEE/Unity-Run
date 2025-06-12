using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles = new GameObject[3]; // 장애물 오브젝트 배열, 3개의 장애물을 가짐
    private bool isStepped; // 플레이어가 발판을 밟았는지 여부 저장

    private readonly string playerTag = "Player"; // 플레이어 태그
    private void OnEnable() // 오브젝트가 활성화 될때 호출 // 시작시 Awake -> OnEnable -> start 순으로 호출
    {                       // 활성화 비활성화 반복할때마다 호출 
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i] = transform.GetChild(i).gameObject; // 자식 오브젝트를 장애물로 설정
         
        }
        isStepped = false;
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true); // 1/3 확률로 장애물 활성화
           
            }
            else
            {
                obstacles[i].SetActive(false); // 나머지 2/3 확률로 장애물 비활성화
            }
        }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(playerTag) && !isStepped)
        {
            isStepped = true;
            GameManager.instance.AddScore(1); // 플레이어가 발판을 밟았을 때 점수 추가
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// 싱글톤 패턴을 사용
// 1.단일 오브젝트: 객체 생성은 한번만 하고
// 2.전역 접근: 다른 스크립트에서 쉽게 접근할 수 있도록
// 3. 전역 상태 유지: 게임이 끝나도 오브젝트가 파괴되지 않도록
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText; // 점수 표시를 위한 UI 텍스트
    public GameObject gameOverUI; // 게임 오버 UI 오브젝트
    public bool isGameOver = false; // 게임 오버 상태를 나타내는 변수
    public int score = 0; // 현재 점수

    private void Awake() // Start() 함수보다 빠르게 실행
    {
        if (instance == null)
        {
            instance = this; // instance에 현재 오브젝트를 할당 해서 생성
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 시마다 호출됨
        }
        else if (instance != this) // instnace가 null이 아니고, 현재 오브젝트가 instance와 다르다면 
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 오브젝트를 파괴
        }
    }

    private void OnDestroy()
    {
        // 씬 로드 이벤트 제거 (메모리 누수 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 로드될 때마다 다시 UI 찾기
        var canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            scoreText = canvas.transform.GetChild(0).GetComponent<Text>();
            gameOverUI = canvas.transform.GetChild(1).gameObject;
        }
    }
    void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0)) // 게임 오버 상태에서 마우스 클릭시
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬을 다시 로드하여 게임을 재시작

        }    
        if(Input.GetKeyDown(KeyCode.Escape)) // ESC 키를 누르면
        {
            QuitGame(); // 게임 종료
        }

    }
    public void AddScore(int newScore)
    {
         if(!isGameOver)
        {
            score += newScore; // 점수 추가
            scoreText.text = $"Score : {score}"; // UI 텍스트 업데이트
        }

    }
    public void OnPlayerDead()
    {
        isGameOver = true; // 게임 오버 상태로 변경
        gameOverUI.SetActive(true); // 게임 오버 UI 활성화
    }
    public void QuitGame()
    {
        Application.Quit(); // 게임 종료
       
    }

}

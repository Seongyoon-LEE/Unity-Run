using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// �̱��� ������ ���
// 1.���� ������Ʈ: ��ü ������ �ѹ��� �ϰ�
// 2.���� ����: �ٸ� ��ũ��Ʈ���� ���� ������ �� �ֵ���
// 3. ���� ���� ����: ������ ������ ������Ʈ�� �ı����� �ʵ���
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText; // ���� ǥ�ø� ���� UI �ؽ�Ʈ
    public GameObject gameOverUI; // ���� ���� UI ������Ʈ
    public bool isGameOver = false; // ���� ���� ���¸� ��Ÿ���� ����
    private int score = 0; // ���� ����
    private void Awake() // Start() �Լ����� ������ ����
    {
        if (instance == null)
        {
            instance = this; // instance�� ���� ������Ʈ�� �Ҵ� �ؼ� ����
            DontDestroyOnLoad(gameObject); // �ٸ� ������ �̵� �ص� ���� ������Ʈ�� �ı����� �ʵ��� ����
        }
        else if (instance != this) // instnace�� null�� �ƴϰ�, ���� ������Ʈ�� instance�� �ٸ��ٸ� 
        {
            Debug.LogWarning("���� �ΰ� �̻��� ���ӸŴ����� ���� �մϴ�!");
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ�� �ı�
        }
    }
    void Start()
    {
        
    }


    void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0)) // ���� ���� ���¿��� ���콺 Ŭ����
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� ���� �ٽ� �ε��Ͽ� ������ �����
        }    
    }
    public void AddScore(int newScore)
    {
         if(!isGameOver)
        {
            score += newScore; // ���� �߰�
            scoreText.text = $"Score : {score}"; // UI �ؽ�Ʈ ������Ʈ
        }

    }
    public void OnPlayerDead()
    {
        isGameOver = true; // ���� ���� ���·� ����
        gameOverUI.SetActive(true); // ���� ���� UI Ȱ��ȭ
    }

}

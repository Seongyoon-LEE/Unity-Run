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
    public int score = 0; // ���� ����

    private void Awake() // Start() �Լ����� ������ ����
    {
        if (instance == null)
        {
            instance = this; // instance�� ���� ������Ʈ�� �Ҵ� �ؼ� ����
            SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �ø��� ȣ���
        }
        else if (instance != this) // instnace�� null�� �ƴϰ�, ���� ������Ʈ�� instance�� �ٸ��ٸ� 
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ�� �ı�
        }
    }

    private void OnDestroy()
    {
        // �� �ε� �̺�Ʈ ���� (�޸� ���� ����)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� �ε�� ������ �ٽ� UI ã��
        var canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            scoreText = canvas.transform.GetChild(0).GetComponent<Text>();
            gameOverUI = canvas.transform.GetChild(1).gameObject;
        }
    }
    void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0)) // ���� ���� ���¿��� ���콺 Ŭ����
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� ���� �ٽ� �ε��Ͽ� ������ �����

        }    
        if(Input.GetKeyDown(KeyCode.Escape)) // ESC Ű�� ������
        {
            QuitGame(); // ���� ����
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
    public void QuitGame()
    {
        Application.Quit(); // ���� ����
       
    }

}

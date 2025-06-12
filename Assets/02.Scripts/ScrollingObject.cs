using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 10f;

    void OnEnable()
    {
        GameManager.instance.isGameOver = false; // ���� ���� �� �ʱ�ȭ
        StartCoroutine(UpdateMoving());
    }

    private void Update()
    {
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    IEnumerator UpdateMoving()
    {
        while (!GameManager.instance.isGameOver)
        {
            yield return new WaitForSeconds(0.0003f); // 0.0003�ʸ��� �ݺ�
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}

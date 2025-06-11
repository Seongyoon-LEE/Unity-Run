using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 10f;

    private void Start()
    {
        StartCoroutine(UpdateMoving());
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

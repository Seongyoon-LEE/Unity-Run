using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    public float width;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        width = boxCollider.size.x;

        StartCoroutine(UpdateBackGroundMoving());
    }

    IEnumerator UpdateBackGroundMoving()
    {
        while (!GameManager.instance.isGameOver)
        {
            yield return new WaitForSeconds(0.0003f); // 0.0003�ʸ��� �ݺ�
            if (transform.position.x < -width)
            {
                Reposition();
            }
        }
    }

    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}

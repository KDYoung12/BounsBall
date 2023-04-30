using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float jumpPower;
    public float speed;
    public int jumpCount;
    public int starCount;

    public Transform star;
    void Awake()
    {
        starCount = 0;    
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.Translate(new Vector3(h, 0, 0));

        // Transform���� ������ �޾ƾ� childCount�� �� �� ����
        // Transform(���� �̸�).childCount�� �ϸ� ���� ��ü���� ������ �� �� ����
        if(starCount == star.childCount)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);

        if (collision.gameObject.CompareTag("HiddenGround"))
        {
            collision.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            collision.gameObject.SetActive(false);
            starCount++;
        }
    }
}

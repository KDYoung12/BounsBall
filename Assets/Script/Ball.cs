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

    bool isJump;
    public Transform star;
    void Awake()
    {
        isJump = false;
        starCount = 0;    
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.Translate(new Vector3(h, 0, 0));

        // Transform으로 변수를 받아야 childCount를 알 수 있음
        // Transform(변수 이름).childCount를 하면 하위 개체들의 개수를 알 수 있음
        if(starCount == star.childCount)
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.Space) && isJump)
        {
            jumpCount++;
        }
        if(jumpCount >= 2)
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();

            rigid.AddForce(new Vector2(0, jumpPower * 2), ForceMode2D.Impulse);

            this.GetComponent<Renderer>().material.color = Color.yellow;
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
        if (collision.gameObject.CompareTag("twoJump"))
        {
            this.GetComponent<Renderer>().material.color = Color.black;
            isJump = true;
            collision.gameObject.SetActive(false);
        }
    }
}

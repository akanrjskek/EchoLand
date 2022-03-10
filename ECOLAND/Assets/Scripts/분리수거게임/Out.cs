using UnityEngine;
using UnityEngine.UI;

public class Out : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 mousePos;
    private Vector3 mouseVector;

    private DragController DC;
    private GameObject gameresult;

    private Vector2 lastPos;
    private Vector2 newPos;
    private Vector2 result;

    private Canvas playgame;
    private bool isthrew;

    // Use this for initialization
    void Start()
    {
        DC = GameObject.Find("Main Camera").GetComponent<DragController>();
        rb = GetComponent<Rigidbody2D>();
        playgame = GameObject.Find("게임진행창").GetComponent<Canvas>();
        isthrew = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DC.started) {
            mousePos = Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f);
            mouseVector = new Vector3(mousePos.x, mousePos.y);
            if (DC.timecount <= 0) Destroy(this.gameObject);
            if (!isthrew) this.gameObject.transform.position += new Vector3(2, 0);
        }
    }

    void OnMouseDown()
    {
        if (DC.started)
        {
            lastPos = rb.position;
            isthrew = true;
        }
    }

    void OnMouseDrag()
    {
        if (DC.started)
        {
            this.transform.position = new Vector3(mouseVector.x / playgame.scaleFactor, mouseVector.y / playgame.scaleFactor, 100);
        }
    }

    void OnMouseUp()
    {
        if (DC.started)
        {
            this.transform.position = new Vector3(mouseVector.x / playgame.scaleFactor, mouseVector.y / playgame.scaleFactor, 100);
            newPos = rb.position;
            result = newPos - lastPos;
            rb.velocity = new Vector2(result.x * 5, result.y * 5);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "쓰레기")
        {
            GameObject.Find("게임진행창").GetComponent<AudioSource>().Play();
            if (collision.gameObject.name == this.gameObject.tag)
            {
                GameObject.Find("정답").GetComponent<Text>().text = "정답!";
                DC.point += 5;
            }
            else
            {
                GameObject.Find("정답").GetComponent<Text>().text = "오답!";
            }
            Destroy(this.gameObject);
        }
    }
}
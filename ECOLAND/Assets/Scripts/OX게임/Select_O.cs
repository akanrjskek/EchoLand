using System.Collections;
using UnityEngine;

public class Select_O : MonoBehaviour
{
    private QuizController Quiz;
    private Vector3 mousePos;
    private Vector3 mouseVector;
    private Vector3 First_O_Position;
    private Vector3 First_O_Scale;
    private Vector2 newPos;
    private Vector2 result;

    private Canvas playgame;

    private bool flag;

    // Use this for initialization
    void Start()
    {
        Quiz = GameObject.Find("Main Camera").GetComponent<QuizController>();
        playgame = GameObject.Find("게임진행창").GetComponent<Canvas>();

        First_O_Position = this.transform.position;
        First_O_Scale = this.transform.localScale;

        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition-new Vector3(Screen.width/2f,Screen.height/2f);
        mouseVector = new Vector3(mousePos.x, 0, First_O_Position.z);
    }

    void OnMouseDrag()
    {
        if (flag && Quiz.started)
        {
            this.transform.position = new Vector3(mouseVector.x / playgame.scaleFactor, mouseVector.y / playgame.scaleFactor, First_O_Position.z);
            this.transform.localScale = new Vector3(0.2f *(this.transform.position.x - First_O_Position.x), 0.2f * (this.transform.position.x - First_O_Position.x), First_O_Position.z);
            newPos = this.transform.position.normalized;
            if (newPos.x > 0)
            {
                Quiz.stopped = true;
                if (Quiz.answer == "O")
                {
                    GetComponent<AudioSource>().Play();
                    Quiz.ToSetProblem("정답입니다!!");
                    Quiz.setText(true);
                }
                else
                {
                    Quiz.ToSetProblem("오답입니다!!");
                    Quiz.setText(false);
                }
                AudioSource audio = Quiz.gameObject.GetComponent<AudioSource>();
                audio.Stop();

                StartCoroutine("delayTime");
            }
        }
    }

    private void OnMouseUp()
    {
        this.transform.position = First_O_Position;
        this.transform.localScale = First_O_Scale;
    }

    IEnumerator delayTime()
    {
        flag = false;
        yield return new WaitForSeconds(1);
        init();
        flag = true;
        Quiz.stopped = false;
    }

    private void init()
    {
        this.transform.position = First_O_Position;
        this.transform.localScale = First_O_Scale;

        if (Quiz.problem_num == 0)
        {
            Quiz.SetResult();
        }
        else
        {
            Quiz.NextProblem();
        }
    }
}
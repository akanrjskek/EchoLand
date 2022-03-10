using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{

    public string problem, answer;
    public Text ResultText, Number, FinalResult, timeLabel;
    public int num, problem_num;
    public GameObject result;
    public bool flag, started, stopped, finished, left;
    public float TimeCount, steptime;
    ForGlobalInformation GI;

    // Use this for initialization
    void Start()
    {
        GI = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>();
        problem_num = 5;
        NextProblem();

        num = 0;
        flag = false;
        started = false;
        stopped = false;
        finished = false;
        left = true;
        steptime = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (TimeCount < 0)
            {
                NextProblem();
            }
            if (!stopped)
            {
                if (TimeCount > 0)
                {
                    TimeCount -= Time.deltaTime;
                    timeLabel.text = string.Format("{0:N2}", TimeCount);
                }
            }
        }
        if (finished)
        {
            if (steptime > 0)
            {
                steptime -= Time.deltaTime;
            }
            else
            {
                if (left)
                {
                    GameObject.Find("나무판").GetComponent<Image>().sprite = GI.sprite_ETC["점수창1"];
                    left = false;
                }
                else
                {
                    GameObject.Find("나무판").GetComponent<Image>().sprite = GI.sprite_ETC["점수창2"];
                    left = true;
                }
                steptime = 0.4f;
            }
        }
    }

    public void NextProblem()
    {
        if (problem_num == 0)
        {
            SetResult();
        }
        else
        {
            AudioSource audio = GetComponent<AudioSource>();
            problem_num--;
            setText(false);
            var ds = new DataService("existing.db");
            var OX = ds.GetOX();
            ToSetProblem(OX.Problem);
            Debug.Log("Set problem : " + OX.Problem);
            ToSetAnswer(OX.Answer);
            Debug.Log("Set answer : " + OX.Answer);
            TimeCount = 5;
            audio.Play();
        }
    }

    private void ToSetProblem(IEnumerable<OX> Problems)
    {
        foreach (var problem in Problems)
        {
            ToSetProblem(problem.Problem);
            ToSetAnswer(problem.Answer);
        }
    }

    public void ToSetProblem(string msg)
    {
        problem = msg;
        ResultText.text = problem;
    }

    private void ToSetAnswer(string msg)
    {
        answer = msg;
    }

    public void GotoMain()
    {
        AudioSource audio = GameObject.Find("결과표시창").GetComponent<AudioSource>();
        audio.Stop();
        SceneManager.LoadScene("메인화면");
    }

    public void setText(bool right)
    {
        if (right)
        {
            num++;
        }
        Number.text = "남은 문제 : " + problem_num + " / 5\n맞은 갯수: " + num;
    }

    public void SetResult()
    {
        started = false;
        Destroy(GameObject.Find("sel_O"));
        Destroy(GameObject.Find("sel_X"));
        FinalResult.text = "+" + num * 10 + " P";
        GI.updatepoint(true, num*10);
        result = GameObject.Find("결과표시창");
        result.GetComponent<Canvas>().sortingLayerName = "result";
        GameObject.Find("결과배경").GetComponent<Canvas>().sortingLayerName = "Unit";
        finished = true;
    }

    public void Start_Game()
    {
        GI.sub_heart();
        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.clip = GI.ClockSound;
        audio.Play();

        GameObject gb = GameObject.Find("문제설명창");
        gb.GetComponent<Canvas>().sortingLayerName = "Default";
        started = true;
    }
}
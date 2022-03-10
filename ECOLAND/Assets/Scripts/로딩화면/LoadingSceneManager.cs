using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider slider;
    float fTime = 0f, steptime;
    bool left;

    void Start()
    {
        left = true;
        steptime = 0.5f;
    }

    void Update()
    {
        fTime += Time.deltaTime;
        slider.value = fTime;
        if (fTime >= 5)
        {
            SceneManager.LoadScene("시작화면");
        }
        if (steptime > 0)
        {
            steptime -= Time.deltaTime;
        }
        else
        {
            if (left)
            {
                GameObject.Find("Handle").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(GameObject.Find("Handle").GetComponent<SpriteRenderer>().sprite.name.Replace("1", "2"));
                left = false;
            }
            else
            {
                GameObject.Find("Handle").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(GameObject.Find("Handle").GetComponent<SpriteRenderer>().sprite.name.Replace("2", "1"));
                left = true;
            }
            steptime = 0.3f;
        }
    }
}



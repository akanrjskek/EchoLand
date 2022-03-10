using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {
    float time, a;
    bool started;

	// Use this for initialization
	void Start () {
        time = 1.5f;
        started = false;
        a = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (started)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("메인화면");
            }
        }
        else
        {
            if (time > 0)
            {
                
                time -= Time.deltaTime;
                a += Time.deltaTime * 0.7f;
                GetComponent<Image>().material.color = new Color(gameObject.GetComponent<Image>().color.r, gameObject.GetComponent<Image>().color.g, gameObject.GetComponent<Image>().color.b, a);
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("시작 화면+나무판");
                time = 1.8f;
                GetComponent<Image>().material.color = new Color(gameObject.GetComponent<Image>().color.r, gameObject.GetComponent<Image>().color.g, gameObject.GetComponent<Image>().color.b, 1);
                started = true;
            }
        }
	}
}

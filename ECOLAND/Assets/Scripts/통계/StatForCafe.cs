using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatForCafe : MonoBehaviour {
    stat statScript;
    public Text txt;
    Image ratio;
    float cafeRatio;

	// Use this for initialization
	void Start () {
        statScript = GameObject.Find("Main Camera").GetComponent<stat>();
        ratio = GameObject.Find("원그래프(비율)").GetComponent<Image>();
        txt = GameObject.Find("그래프텍스트").GetComponent<Text>();
        if (statScript.totCafe != 0)
        {
            cafeRatio = (float)statScript.corCafe / statScript.totCafe;
        }
        else
        {
            cafeRatio = 0;
        }
        txt.text = "일회용품 미사용\n" + (cafeRatio*100).ToString("0.00") + "%";
    }
	
	// Update is called once per frame
	void Update () {
        if (ratio.fillAmount < cafeRatio)
        {
            ratio.fillAmount += 0.01f;
        }
        else
        {
            ratio.fillAmount = cafeRatio;
            Destroy(this);
        }
	}
}

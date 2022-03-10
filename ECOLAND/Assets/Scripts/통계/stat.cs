using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class stat : MonoBehaviour {
    public List<STATINFO> stats;
    GameObject LineGraph;

    public int totCafe, corCafe, count7, maxWALK;

	// Use this for initialization
	void Awake ()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        stats = new List<STATINFO>();
        totCafe = 0;
        corCafe = 0;
        count7 = 0;
        maxWALK = 0;
        
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["DEVICE"] = SystemInfo.deviceUniqueIdentifier;

            var response = client.UploadValues("http://13.124.169.44:3000/getstat", values);

            var responseString = Encoding.Default.GetString(response);
            JsonData data = JsonMapper.ToObject(responseString);

            if (data["resultCode"].ToString() == "200")
            {
                foreach (JsonData obj in data["STATS"])
                {
                    STATINFO tmp = new STATINFO();
                    tmp.FLAG_DATE = obj["FLAG_DATE"].ToString().Substring(5, 5);
                    tmp.DAILYWALK = Int32.Parse(obj["DAILYWALK"].ToString());
                    tmp.CAFE_O = Int32.Parse(obj["CAFE_O"].ToString());
                    tmp.CAFE_X = Int32.Parse(obj["CAFE_X"].ToString());
                    corCafe += tmp.CAFE_O;
                    totCafe += tmp.CAFE_O + tmp.CAFE_X;

                    if (count7 < 7)
                    {
                        if (maxWALK < tmp.DAILYWALK)
                        {
                            maxWALK = tmp.DAILYWALK;
                        }
                        count7++;
                    }
                    stats.Add(tmp);
                }
            }
            else
            {
                Debug.Log("Error 발생");
            }
        }
    }

    public void OnClickElements()
    {
        GameObject.Find("동물도감리스트").transform.SetParent(GameObject.Find("도감").transform);
        GameObject.Find("식물도감리스트").transform.SetParent(GameObject.Find("카페통계").transform);
        GameObject.Find("원그래프(비율)").GetComponent<Image>().fillAmount = 0;
        AudioSource audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audio.Play();
        if (GameObject.Find("걸음수") != null)
        {
            Destroy(GameObject.Find("걸음수"));
        }
        GameObject.Find("걸음수통계").GetComponent<Canvas>().sortingLayerName = "Default";
        GameObject.Find("카페통계").GetComponent<Canvas>().sortingLayerName = "Default";
    }
    
    public void OnClickWalking()
    {
        GameObject.Find("원그래프(비율)").GetComponent<Image>().fillAmount = 0;
        AudioSource audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audio.Play();
        if (GameObject.Find("걸음수") != null)
        {
            Destroy(GameObject.Find("걸음수"));
        }
        GameObject.Find("걸음수통계").AddComponent<StatForWalking>();
        GameObject.Find("걸음수통계").GetComponent<Canvas>().sortingLayerName = "Unit";
        GameObject.Find("카페통계").GetComponent<Canvas>().sortingLayerName = "Default";
    }

    public void OnClickCafe()
    {
        GameObject.Find("식물도감리스트").transform.SetParent(GameObject.Find("도감").transform);
        GameObject.Find("동물도감리스트").transform.SetParent(GameObject.Find("도감").transform);
        GameObject.Find("원그래프(비율)").GetComponent<Image>().fillAmount = 0;
        AudioSource audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audio.Play();
        if (GameObject.Find("걸음수") != null)
        {
            Destroy(GameObject.Find("걸음수"));
        }
        GameObject.Find("카페통계").AddComponent<StatForCafe>();
        GameObject.Find("걸음수통계").GetComponent<Canvas>().sortingLayerName = "Default";
        GameObject.Find("카페통계").GetComponent<Canvas>().sortingLayerName = "Unit";
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene("메인화면");
    }
}

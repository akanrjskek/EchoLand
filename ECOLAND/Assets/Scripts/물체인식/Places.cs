using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using LitJson;
using UnityEngine.UI;

namespace PedometerU
{
    public class Places : MonoBehaviour
    {
        LocationInfo currentGPSPosition;
        LocationInfo previousGPSPosition;

        int radarRadius;
        string radarType, APIkey, radarSensor;
        string googleRespStr;
        public Text txt;
        public bool isok;
        float tracking;

        void Start()
        {
            radarRadius = 10; // 5m
            radarType = "cafe";
            APIkey = "AIzaSyCuF98AhZ8bnflUALD0yaOcglqQSjIqDn0";
            Input.location.Start();
            isok = false;
            tracking = 5;
            previousGPSPosition = new LocationInfo();
        }

        void Update()
        {
            if (tracking > 0)
            {
                tracking -= Time.deltaTime;
            }
            else
            {
                Input.location.Start();
                currentGPSPosition = Input.location.lastData;
                Debug.Log(currentGPSPosition.latitude + "," + currentGPSPosition.longitude);
                tracking = 180;
            }
            if (!currentGPSPosition.Equals(previousGPSPosition))
            {
                starting_S();
                previousGPSPosition = currentGPSPosition;
            }
        }

        IEnumerator Radar()
        {
            googleRespStr = "";
            string radarURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + currentGPSPosition.latitude + "," + currentGPSPosition.longitude + "&radius=" + radarRadius + "&types=" + radarType + "&key=" + APIkey;
            Debug.Log(radarURL);
            WWW googleResp = new WWW(radarURL);
            yield return googleResp;

            JsonData data = JsonMapper.ToObject(googleResp.text);
            Debug.Log(googleResp.text);

            for (int i = 0; i < data["results"].Count; i++)
            {
                googleRespStr += data["results"][i]["name"].ToString() + "\n";
            }

            if(googleRespStr == "")
            {
                GameObject.Find("Point Light").GetComponent<Image>().color = new Color(255, 0, 0);
                isok = false;
            }
            else
            {
                LocalNotification.SendNotification(1, 0, "일회용컵 대신 텀블러를 사용해보세요!", "텀블러 사용을 인증해주시면 포인트를 지급해드립니다.", new Color32(0xff, 0x44, 0x44, 255), true, true, true, "app_icon");
                GameObject.Find("Point Light").GetComponent<Image>().color = new Color(0, 0, 255);
                isok = true;
            }
            Input.location.Stop();
        }

        public void starting_S()
        {
            StartCoroutine("Radar");
        }

        public void GotoDetect()
        {
            AudioSource audio = GameObject.Find("메인화면").GetComponent<AudioSource>();
            audio.Play();
            if (isok && GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().dailycafe < 3)
            {
                SceneManager.LoadScene("물체인식");
            }
        }
    }
}
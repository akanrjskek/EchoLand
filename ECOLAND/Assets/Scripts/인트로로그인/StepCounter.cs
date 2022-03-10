/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU {
    using LitJson;
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;
    using UnityEngine;

    public class StepCounter : MonoBehaviour {

        public string stepText, dailywalkText;
        private Pedometer pedometer;
        public int dailywalk, tmpwalk;

        public void starting()
        {
            Application.runInBackground = true;

            DontDestroyOnLoad(this);

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["DEVICE"] = SystemInfo.deviceUniqueIdentifier;

                var response = client.UploadValues("http://13.124.169.44:3000/getuserinfo", values);

                var responseString = Encoding.Default.GetString(response);
                JsonData data = JsonMapper.ToObject(responseString);

                if (data["resultCode"].ToString() == "200")
                {
                    dailywalk = Int32.Parse(data["dailywalk"].ToString());
                }
                else
                {
                    Debug.Log("Error 발생");
                }
            }
            // Create a new pedometer
            pedometer = new Pedometer(OnStep);
            tmpwalk = 300;
            // Reset UI
            OnStep(0, 0);
        }

        private void Update()
        {
            if (stepText != "$")
            {
                if (Int32.Parse(stepText) > tmpwalk)
                {
                    update_walk();
                    tmpwalk += 300;
                    GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().updatepoint(true, 50);
                }
            }
        }

        public void update_walk()
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["DEVICE"] = SystemInfo.deviceUniqueIdentifier;
                values["curwalk"] = dailywalkText;

                var response = client.UploadValues("http://13.124.169.44:3000/update_walk", values);

                var responseString = Encoding.Default.GetString(response);
                JsonData data = JsonMapper.ToObject(responseString);

                if (data["resultCode"].ToString() == "200")
                {
                    Debug.Log("걸음수 업데이트 성공");
                }
                else
                {
                    Debug.Log("Error 발생");
                }
            }
        }

        private void OnStep (int steps, double distance) {
            // Display the values // Distance in feet
            stepText = steps.ToString();
            dailywalkText = (dailywalk + Int32.Parse(stepText)).ToString();
        }

        private void OnDisable () {
            // Release the pedometer
            pedometer.Dispose();
            pedometer = null;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using LitJson;
using System.Net;
using System.Collections.Specialized;
using System.Text;

public class CameraScript : MonoBehaviour {
    private bool camAvailable;
    private WebCamTexture backCam;
    public RawImage background;
    public AspectRatioFitter fit;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(this);
        WebCamDevice[] devices = WebCamTexture.devices;
        if(devices.Length == 0)
        {
            Debug.Log("No Camera Detected");
            camAvailable = false;
        }

        for(int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if(backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }

        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!camAvailable)
            return;

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    public void GotoMain()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("메인화면");
    }

    public void GotoMainWithPredict()
    {
        Destroy(this.gameObject.GetComponent<AudioListener>());
        SceneManager.LoadScene("메인화면");
    }

    public void Sending()
    {
        StartCoroutine("predict");
    }

    public IEnumerator predict()
    {
        GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().dailycafe++;
        Debug.Log("물체인식시작");

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Prediction-Key", "33b9d3dfca7e47bb993a04d39f6c94fd"); //누나꺼
        headers.Add("Content-Type", "application/octet-stream"); //누나꺼

        Texture2D uploadedPhoto = new Texture2D(backCam.width, backCam.height);
        uploadedPhoto.SetPixels(backCam.GetPixels());
        uploadedPhoto.Apply();

        TextureScale.Bilinear(uploadedPhoto, uploadedPhoto.width / 4, uploadedPhoto.height / 4);
        byte[] img = uploadedPhoto.EncodeToPNG();

        GotoMainWithPredict();

        WWW www = new WWW("https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/90f3b2dc-9ed9-49e2-a49d-10a1a4f3c9e2/image?iterationId=cccc53dd-2ef5-4cbc-8c9b-d9c3e56c8cf9", img, headers); //누나꺼
        yield return www;

        Debug.Log(www.text);

        JsonData data = JsonMapper.ToObject(www.text);
        string temp = data["predictions"][0]["tagName"].ToString();

        if (temp.CompareTo("plastic") != 0)
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["DEVICE"] = SystemInfo.deviceUniqueIdentifier;
                values["CAFE"] = "O";

                var response = client.UploadValues("http://13.124.169.44:3000/update_object", values);

                var responseString = Encoding.Default.GetString(response);
                JsonData ob = JsonMapper.ToObject(responseString);

                if (ob["resultCode"].ToString() == "200")
                {
                    Debug.Log("카페업데이트성공");
                }
                else
                {
                    Debug.Log("Error 발생");
                }
            }
            GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().add_heart();
            GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().update_heart = true;
            temp += "\n 50p 지급";
            GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().updatepoint(true, 50);
        }
        else
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["DEVICE"] = SystemInfo.deviceUniqueIdentifier;
                values["CAFE"] = "X";

                var response = client.UploadValues("http://13.124.169.44:3000/update_object", values);

                var responseString = Encoding.Default.GetString(response);
                JsonData ob = JsonMapper.ToObject(responseString);

                if (ob["resultCode"].ToString() == "200")
                {
                    Debug.Log("카페업데이트성공");
                }
                else
                {
                    Debug.Log("Error 발생");
                }
            }
            temp += "\n 일회용품 사용 포인트 미지급";
        }
        Debug.Log("포인트 업데이트 완료");
        Destroy(this.gameObject);
    }
}

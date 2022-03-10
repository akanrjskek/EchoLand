using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class sellScript : MonoBehaviour {
    GameObject sel_O, sel_X, noti;
    ForGlobalInformation GI;
    PedometerU.Main main;
    public bool cansell; 
    public Dictionary<string,int> animalpoint, plantpoint;
    private void Start()
    {
        cansell = false;
        animalpoint = new Dictionary<string, int>();
        plantpoint = new Dictionary<string, int>();
        animalpoint["치즈고양이1"] = 40;
        animalpoint["토끼1"] = 80;
        animalpoint["얼룩강아지1"] = 160;
        animalpoint["사자1"] = 320;
        animalpoint["백조1"] = 640;
        animalpoint["유니콘1"] = 1280;
        plantpoint["나무1"] = 24;
        plantpoint["나무2"] = 40;
        plantpoint["나무3"] = 56;
        plantpoint["나무4"] = 80;
        plantpoint["보라튤립"] = 120;
        plantpoint["부바르디아"] = 160;
        plantpoint["선인장"] = 240;
        plantpoint["은행나무"] = 400;
        plantpoint["장미"] = 560;
        plantpoint["해바라기"] = 800;
        plantpoint["돛단배"] = 1200;

        GI = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>();
        main = GameObject.Find("메인화면").GetComponent<PedometerU.Main>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => selling());
    }
    private void Update()
    {
        if (!cansell)
        {
            if (noti != null && sel_O != null && sel_X != null)
            {
                Destroy(noti);
                Destroy(sel_O);
                Destroy(sel_X);
            }
        }
        if(noti != null && sel_O != null && sel_X != null)
        {
            noti.transform.position = gameObject.transform.position + new Vector3(35, 30, 0);
            sel_O.transform.position = gameObject.transform.position + new Vector3(-20, 30, 0);
            sel_X.transform.position = gameObject.transform.position + new Vector3(20, 30, 0);
        }
    }

    void selling()
    {
        if (gameObject.GetComponent<Items>().isanimal)
        {
            gameObject.GetComponent<Image>().sprite = GI.sprite_Animals["touch" + gameObject.GetComponent<Image>().sprite.name];
            gameObject.GetComponent<Items>().waiting = true;
            gameObject.GetComponent<Items>().current = new Vector3(0,0);
            gameObject.GetComponent<Items>().num = 4;
            gameObject.GetComponent<Items>().timecount = 3;
        }
        main.get_right(this);
        if (cansell)
        {
            noti = new GameObject();
            noti.name = "가격정보";
            noti.AddComponent<Text>();
            noti.GetComponent<Text>().font = GI.arial_font;
            noti.GetComponent<Text>().fontSize = 20;
            noti.transform.SetParent(GameObject.Find("메인화면").transform);
            noti.transform.position = gameObject.transform.position + new Vector3(50, 30, 0);
            if (animalpoint.ContainsKey(gameObject.name))
            {
                noti.GetComponent<Text>().text = animalpoint[gameObject.name].ToString();
            }
            else
            {
                noti.GetComponent<Text>().text = plantpoint[gameObject.name].ToString();
            }

            sel_O = new GameObject();
            sel_O.name = "O";
            sel_O.AddComponent<Image>().sprite = GI.sprite_ETC["O"];
            sel_O.transform.SetParent(GameObject.Find("메인화면").transform);
            sel_O.transform.position = gameObject.transform.position + new Vector3(-20, 30, 0);
            sel_O.transform.localScale = new Vector3(0.25f, 0.25f, 1);
            sel_O.AddComponent<Button>().onClick.AddListener(() => sellButtonClickListner());

            sel_X = new GameObject();
            sel_X.name = "X";
            sel_X.AddComponent<Image>().sprite = GI.sprite_ETC["X"];
            sel_X.transform.SetParent(GameObject.Find("메인화면").transform);
            sel_X.transform.position = gameObject.transform.position + new Vector3(20, 30, 0);
            sel_X.transform.localScale = new Vector3(0.25f, 0.25f, 1);
            sel_X.AddComponent<Button>().onClick.AddListener(() => cancleButtonClickListner());
        }
    }

    void sellButtonClickListner()
    {
        GI.items.Remove(gameObject.GetComponent<Items>().id);
        Debug.Log(GI.items);
        GI.numofanimals--;
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["DEVICE"] = SystemInfo.deviceUniqueIdentifier;
            values["ITEM"] = gameObject.GetComponent<Items>().info.ITEM;
            values["LOC_X"] = gameObject.GetComponent<Items>().info.LOC_X;
            values["LOC_Y"] = gameObject.GetComponent<Items>().info.LOC_Y;

            var response = client.UploadValues("http://13.124.169.44:3000/delete_item", values);

            var responseString = Encoding.Default.GetString(response);
            JsonData data = JsonMapper.ToObject(responseString);

            if (data["resultCode"].ToString() == "200")
            {
                Debug.Log("아이템삭제성공");
            }
            else
            {
                Debug.Log("Error 발생");
            }
        }

        if (animalpoint.ContainsKey(gameObject.name))
        {
            GI.updatepoint(true, animalpoint[gameObject.name]);
        }
        else
        {
            GI.updatepoint(true, plantpoint[gameObject.name]);
        }
        Destroy(sel_O);
        Destroy(sel_X);
        Destroy(noti);
        Destroy(gameObject);
    }

    void cancleButtonClickListner()
    {
        Destroy(sel_O);
        Destroy(sel_X);
        Destroy(noti);
    }

}

using System.Net;
using System.Collections.Specialized;
using UnityEngine;
using System.Text;
using System;
using LitJson;
using System.Collections;
using System.Collections.Generic;

public class ForGlobalInformation : MonoBehaviour {
    public string deviceID, dailywalk, totwalk;
    public int curpoint, totpoint, dailycafe;
    public ArrayList items;
    private uint exitCountValue = 0;
    bool bPaused = false;
    public int hearts, music_num;
    public AudioClip[] clip_BGM;
    public Sprite[] sprite_Musics;
    public int numofanimals, trashes;
    double time;
    public bool update_heart;

    public Dictionary<string, int> dict_Animals, dict_Plants;
    public Dictionary<string, Sprite> sprite_Animals, sprite_Plants, sprite_Trash, sprite_ETC;
    public AudioClip ClockSound;

    public Font arial_font;
    public List<string> point_queue;

    public System.Random r;
    public string[] str_trash;

    private void Awake()
    {
        point_queue = new List<string>();
        music_num = 0;
        DontDestroyOnLoad(this);
        items = new ArrayList();
        login();
        clip_BGM = new AudioClip[4];
        sprite_Musics = new Sprite[4];

        trashes = 0;

        for(int i = 0; i < 4; i++)
        {
            clip_BGM[i] = Resources.Load<AudioClip>("BGM/" + i.ToString());
            sprite_Musics[i] = Resources.Load<Sprite>(i.ToString());
        }

        sprite_Animals = new Dictionary<string, Sprite>();
        sprite_Plants = new Dictionary<string, Sprite>();

        dict_Animals = new Dictionary<string, int>();
        dict_Animals.Add("left치즈고양이1", 0);
        sprite_Animals.Add("치즈고양이1", Resources.Load<Sprite>("동물들/left치즈고양이1"));
        sprite_Animals.Add("치즈고양이2", Resources.Load<Sprite>("동물들/left치즈고양이2"));
        sprite_Animals.Add("left치즈고양이1", Resources.Load<Sprite>("동물들/left치즈고양이1"));
        sprite_Animals.Add("left치즈고양이2", Resources.Load<Sprite>("동물들/left치즈고양이2"));
        sprite_Animals.Add("right치즈고양이1", Resources.Load<Sprite>("동물들/right치즈고양이1"));
        sprite_Animals.Add("right치즈고양이2", Resources.Load<Sprite>("동물들/right치즈고양이2"));
        sprite_Animals.Add("touchleft치즈고양이1", Resources.Load<Sprite>("동물들/touchleft치즈고양이1"));
        sprite_Animals.Add("touchleft치즈고양이2", Resources.Load<Sprite>("동물들/touchleft치즈고양이2"));
        sprite_Animals.Add("touchright치즈고양이1", Resources.Load<Sprite>("동물들/touchright치즈고양이1"));
        sprite_Animals.Add("touchright치즈고양이2", Resources.Load<Sprite>("동물들/touchright치즈고양이2"));
        dict_Animals.Add("left토끼1", 1);
        sprite_Animals.Add("토끼1", Resources.Load<Sprite>("동물들/left토끼1"));
        sprite_Animals.Add("토끼2", Resources.Load<Sprite>("동물들/left토끼2"));
        sprite_Animals.Add("left토끼1", Resources.Load<Sprite>("동물들/left토끼1"));
        sprite_Animals.Add("left토끼2", Resources.Load<Sprite>("동물들/left토끼2"));
        sprite_Animals.Add("right토끼1", Resources.Load<Sprite>("동물들/right토끼1"));
        sprite_Animals.Add("right토끼2", Resources.Load<Sprite>("동물들/right토끼2"));
        sprite_Animals.Add("touchleft토끼1", Resources.Load<Sprite>("동물들/touchleft토끼1"));
        sprite_Animals.Add("touchleft토끼2", Resources.Load<Sprite>("동물들/touchleft토끼2"));
        sprite_Animals.Add("touchright토끼1", Resources.Load<Sprite>("동물들/touchright토끼1"));
        sprite_Animals.Add("touchright토끼2", Resources.Load<Sprite>("동물들/touchright토끼2"));
        dict_Animals.Add("left얼룩강아지1", 2);
        sprite_Animals.Add("얼룩강아지1", Resources.Load<Sprite>("동물들/left얼룩강아지1"));
        sprite_Animals.Add("얼룩강아지2", Resources.Load<Sprite>("동물들/left얼룩강아지2"));
        sprite_Animals.Add("left얼룩강아지1", Resources.Load<Sprite>("동물들/left얼룩강아지1"));
        sprite_Animals.Add("left얼룩강아지2", Resources.Load<Sprite>("동물들/left얼룩강아지2"));
        sprite_Animals.Add("right얼룩강아지1", Resources.Load<Sprite>("동물들/right얼룩강아지1"));
        sprite_Animals.Add("right얼룩강아지2", Resources.Load<Sprite>("동물들/right얼룩강아지2"));
        sprite_Animals.Add("touchleft얼룩강아지1", Resources.Load<Sprite>("동물들/touchleft얼룩강아지1"));
        sprite_Animals.Add("touchleft얼룩강아지2", Resources.Load<Sprite>("동물들/touchleft얼룩강아지2"));
        sprite_Animals.Add("touchright얼룩강아지1", Resources.Load<Sprite>("동물들/touchright얼룩강아지1"));
        sprite_Animals.Add("touchright얼룩강아지2", Resources.Load<Sprite>("동물들/touchright얼룩강아지2"));
        dict_Animals.Add("left사자1", 3);
        sprite_Animals.Add("사자1", Resources.Load<Sprite>("동물들/left사자1"));
        sprite_Animals.Add("사자2", Resources.Load<Sprite>("동물들/left사자2"));
        sprite_Animals.Add("left사자1", Resources.Load<Sprite>("동물들/left사자1"));
        sprite_Animals.Add("left사자2", Resources.Load<Sprite>("동물들/left사자2"));
        sprite_Animals.Add("right사자1", Resources.Load<Sprite>("동물들/right사자1"));
        sprite_Animals.Add("right사자2", Resources.Load<Sprite>("동물들/right사자2"));
        sprite_Animals.Add("touchleft사자1", Resources.Load<Sprite>("동물들/touchleft사자1"));
        sprite_Animals.Add("touchleft사자2", Resources.Load<Sprite>("동물들/touchleft사자2"));
        sprite_Animals.Add("touchright사자1", Resources.Load<Sprite>("동물들/touchright사자1"));
        sprite_Animals.Add("touchright사자2", Resources.Load<Sprite>("동물들/touchright사자2"));
        dict_Animals.Add("left백조1", 4);
        sprite_Animals.Add("백조1", Resources.Load<Sprite>("동물들/left백조1"));
        sprite_Animals.Add("백조2", Resources.Load<Sprite>("동물들/left백조2"));
        sprite_Animals.Add("left백조1", Resources.Load<Sprite>("동물들/left백조1"));
        sprite_Animals.Add("left백조2", Resources.Load<Sprite>("동물들/left백조2"));
        sprite_Animals.Add("right백조1", Resources.Load<Sprite>("동물들/right백조1"));
        sprite_Animals.Add("right백조2", Resources.Load<Sprite>("동물들/right백조2"));
        sprite_Animals.Add("touchleft백조1", Resources.Load<Sprite>("동물들/touchleft백조1"));
        sprite_Animals.Add("touchleft백조2", Resources.Load<Sprite>("동물들/touchleft백조2"));
        sprite_Animals.Add("touchright백조1", Resources.Load<Sprite>("동물들/touchright백조1"));
        sprite_Animals.Add("touchright백조2", Resources.Load<Sprite>("동물들/touchright백조2"));
        dict_Animals.Add("left유니콘1", 5);
        sprite_Animals.Add("유니콘1", Resources.Load<Sprite>("동물들/left유니콘1"));
        sprite_Animals.Add("유니콘2", Resources.Load<Sprite>("동물들/left유니콘2"));
        sprite_Animals.Add("left유니콘1", Resources.Load<Sprite>("동물들/left유니콘1"));
        sprite_Animals.Add("left유니콘2", Resources.Load<Sprite>("동물들/left유니콘2"));
        sprite_Animals.Add("right유니콘1", Resources.Load<Sprite>("동물들/right유니콘1"));
        sprite_Animals.Add("right유니콘2", Resources.Load<Sprite>("동물들/right유니콘2"));
        sprite_Animals.Add("touchleft유니콘1", Resources.Load<Sprite>("동물들/touchleft유니콘1"));
        sprite_Animals.Add("touchleft유니콘2", Resources.Load<Sprite>("동물들/touchleft유니콘2"));
        sprite_Animals.Add("touchright유니콘1", Resources.Load<Sprite>("동물들/touchright유니콘1"));
        sprite_Animals.Add("touchright유니콘2", Resources.Load<Sprite>("동물들/touchright유니콘2"));

        dict_Plants = new Dictionary<string, int>();
        dict_Plants.Add("나무1", 0);
        sprite_Plants.Add("나무1", Resources.Load<Sprite>("식물들/나무1"));
        dict_Plants.Add("나무2", 1);
        sprite_Plants.Add("나무2", Resources.Load<Sprite>("식물들/나무2"));
        dict_Plants.Add("나무3", 2);
        sprite_Plants.Add("나무3", Resources.Load<Sprite>("식물들/나무3"));
        dict_Plants.Add("나무4", 3);
        sprite_Plants.Add("나무4", Resources.Load<Sprite>("식물들/나무4"));
        dict_Plants.Add("보라튤립", 4);
        sprite_Plants.Add("보라튤립", Resources.Load<Sprite>("식물들/보라튤립"));
        dict_Plants.Add("부바르디아", 5);
        sprite_Plants.Add("부바르디아", Resources.Load<Sprite>("식물들/부바르디아"));
        dict_Plants.Add("선인장", 6);
        sprite_Plants.Add("선인장", Resources.Load<Sprite>("식물들/선인장"));
        dict_Plants.Add("은행나무", 7);
        sprite_Plants.Add("은행나무", Resources.Load<Sprite>("식물들/은행나무"));
        dict_Plants.Add("장미", 8);
        sprite_Plants.Add("장미", Resources.Load<Sprite>("식물들/장미"));
        dict_Plants.Add("해바라기", 9);
        sprite_Plants.Add("해바라기", Resources.Load<Sprite>("식물들/해바라기"));
        dict_Plants.Add("돛단배", 10);
        sprite_Plants.Add("돛단배", Resources.Load<Sprite>("식물들/돛단배"));


        sprite_ETC = new Dictionary<string, Sprite>();

        sprite_ETC.Add("점수창1", Resources.Load<Sprite>("미니게임 점수창 틀1"));
        sprite_ETC.Add("점수창2", Resources.Load<Sprite>("미니게임 점수창 틀2"));
        sprite_ETC.Add("하트", Resources.Load<Sprite>("하트ON"));
        sprite_ETC.Add("체크", Resources.Load<Sprite>("체크 표시"));
        sprite_ETC.Add("트럭", Resources.Load<Sprite>("Truck"));
        sprite_ETC.Add("O", Resources.Load<Sprite>("letter-o"));
        sprite_ETC.Add("X", Resources.Load<Sprite>("letter-x"));

        ClockSound = Resources.Load<AudioClip>("BGM/Clock3");

        sprite_Trash = new Dictionary<string, Sprite>();
        str_trash = new string[] { "can", "귤", "컵", "유리병","달걀껍질","닭뼈","담배","빨대","수정테이프","연필","전구","족발"
        ,"참치캔","커피찌꺼기","헌옷","헌운동화","화이트","의자","사이다","맥주병","맥주캔","참이슬","플라스틱그릇","플라스틱컵","플라스틱숟가락","향수병","스테인리스1","스테인리스2"};
        for(int i=0; i < str_trash.Length; i++)
        {
            sprite_Trash.Add(str_trash[i], Resources.Load<Sprite>("쓰레기들/" + str_trash[i]));
        }

        arial_font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        update_heart = false;
        numofanimals = 0;
        r = new System.Random();
        
        time = r.Next(30, 90) + r.NextDouble();
    }
    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            trashes++;
            time = r.Next(30, 90) + r.NextDouble();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            exitCountValue++;
            if (!IsInvoking("disable_DoubleClick"))
                Invoke("disable_DoubleClick", 0.5f);
        }
        if(exitCountValue == 2)
        {
            CancelInvoke("disable_DoubleClick");
            Application.Quit();
        }
    }

    void disable_DoubleClick()
    {
        exitCountValue = 0;
    }

    public void updatepoint(bool plus, int newpoint)
    {
        curpoint += newpoint;
        if(plus) totpoint += newpoint;
        string temp;
        if (plus)
        {
            temp = "+";
        }
        else
        {
            temp = "";
        }
        point_queue.Add(temp + newpoint.ToString());

        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["DEVICE"] = deviceID;
            values["totpoint"] = totpoint.ToString();
            values["curpoint"] = curpoint.ToString();
            values["heart"] = hearts.ToString();

            var response = client.UploadValues("http://13.124.169.44:3000/update_point", values);

            var responseString = Encoding.Default.GetString(response);
            JsonData data = JsonMapper.ToObject(responseString);

            if (data["resultCode"].ToString() == "200")
            {
                Debug.Log("포인트업데이트성공");
            }
            else
            {
                Debug.Log("Error 발생");
            }
        }
    }

    public void add_heart()
    {
        if (hearts < 5)
        {
            hearts++;
            update_heart = true;
        }
    }

    public bool isokheart()
    {
        if (hearts > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void sub_heart()
    {
         hearts--;
    }

    public void login()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;

        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["DEVICE"] = deviceID;

            var response = client.UploadValues("http://13.124.169.44:3000/login", values);

            var responseString = Encoding.Default.GetString(response);
            JsonData data = JsonMapper.ToObject(responseString);

            if (data["resultCode"].ToString() == "200")
            {
                foreach (JsonData obj in data["ITEMS"])
                {
                    ITEMINFO tmp = new ITEMINFO();
                    tmp.ITEM = obj["ITEM"].ToString();
                    tmp.LOC_X = obj["LOC_X"].ToString();
                    tmp.LOC_Y = obj["LOC_Y"].ToString();
                    items.Add(tmp);
                }
                curpoint = Int32.Parse(data["CURPOINT"].ToString());
                totpoint = Int32.Parse(data["TOTPOINT"].ToString());
                dailycafe = Int32.Parse(data["DAILYCAFE"].ToString());
                dailywalk = data["DAILYWALK"].ToString();
                GameObject.Find("StepCounter").GetComponent<PedometerU.StepCounter>().dailywalkText = dailywalk;
                GameObject.Find("StepCounter").GetComponent<PedometerU.StepCounter>().dailywalk = Int32.Parse(dailywalk);
                totwalk = data["TOTWALK"].ToString();
                hearts = Int32.Parse(data["HEART"].ToString());
                Debug.Log(data["HEART"].ToString());
            }
            else if (data["resultCode"].ToString() == "300")
            {
                register();
            }
            else
            {
                Debug.Log("Error 발생");
            }
        }

        GameObject.Find("StepCounter").GetComponent<PedometerU.StepCounter>().starting();
    }
    
    public void register()
    {
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["DEVICE"] = deviceID;

            var response = client.UploadValues("http://13.124.169.44:3000/register", values);

            var responseString = Encoding.Default.GetString(response);
            JsonData data = JsonMapper.ToObject(responseString);

            if (data["resultCode"].ToString() == "200")
            {
                login();
            }
            else
            {
                Debug.Log("Error 발생");
            }
        }
    }

    private void OnApplicationQuit()
    {
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["DEVICE"] = deviceID;
            values["totpoint"] = totpoint.ToString();
            values["curpoint"] = curpoint.ToString();
            values["heart"] = hearts.ToString();

            var response = client.UploadValues("http://13.124.169.44:3000/update_point", values);

            var responseString = Encoding.Default.GetString(response);
            JsonData data = JsonMapper.ToObject(responseString);

            if (data["resultCode"].ToString() == "200")
            {
                Debug.Log("포인트업데이트성공");
            }
            else
            {
                Debug.Log("Error 발생");
            }
        }

        GameObject.Find("StepCounter").GetComponent<PedometerU.StepCounter>().update_walk();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            bPaused = true;
            Application.runInBackground = true;
            // todo : 어플리케이션을 내리는 순간에 처리할 행동들 /
        }
        else
        {
            if (bPaused)
            {
                bPaused = false;
                //todo : 내려놓은 어플리케이션을 다시 올리는 순간에 처리할 행동들 
            }
        }
    }
}

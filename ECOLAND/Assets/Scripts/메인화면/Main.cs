
namespace PedometerU
{
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;

    public class Main : MonoBehaviour {
        public long point;
        public String step;
        public Text PointText, StepText;
        public bool isclicked, isHeart, stopHeart, firstHeart;
        public bool CheckYn, readyToSell;
        public ArrayList items;
        GameObject main;
        ForGlobalInformation GI;
        int music_num, scale_count;
        private uint enterCountValue = 0;
        sellScript prev;
        public Vector3 scalesize;
        System.Random r;
        float time;

        public Dictionary<string, int> dict1, dict2;

        private void Awake()
        {
            readyToSell = false;
            GI = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>();
            music_num = GI.music_num;
            GameObject.Find("음악").GetComponent<Image>().sprite = GI.sprite_Musics[music_num];
            GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = GI.clip_BGM[music_num];
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
            dict1 = GI.dict_Animals;
            dict2 = GI.dict_Plants;

            main = GameObject.Find("메인화면");
            items = GI.items;

            foreach(ITEMINFO obj in items)
            {
                GameObject Ob1 = new GameObject();
                Ob1.transform.parent = main.transform;
                Ob1.name = obj.ITEM;
                Ob1.transform.position = new Vector3(float.Parse(obj.LOC_X), float.Parse(obj.LOC_Y), 0);
                Ob1.transform.localScale = new Vector3(0.3f, 0.3f);
                Ob1.AddComponent<Image>();

                Ob1.AddComponent<Button>();
                Ob1.AddComponent<sellScript>();
                Ob1.AddComponent<Items>();
                Ob1.GetComponent<Items>().info.ITEM = obj.ITEM;
                Ob1.GetComponent<Items>().info.LOC_X = obj.LOC_X;
                Ob1.GetComponent<Items>().info.LOC_Y = obj.LOC_Y;
                Ob1.GetComponent<Items>().id = GI.numofanimals;
                GI.numofanimals++;

                if (dict1.ContainsKey("left"+obj.ITEM))
                {
                    Ob1.GetComponent<Image>().sprite = GI.sprite_Animals[obj.ITEM];
                    Ob1.GetComponent<Items>().isanimal = true;
                    Ob1.AddComponent<BoxCollider2D>();
                }
                else
                {
                    Ob1.GetComponent<Items>().isanimal = false;
                    Ob1.GetComponent<Image>().sprite = GI.sprite_Plants[obj.ITEM];
                }
            }
            for(int i = 0; i < GI.hearts; i++)
            {
                GameObject.Find("하트" + (i + 1).ToString()).GetComponent<Image>().sprite = GI.sprite_ETC["하트"];
            }
            isHeart = false;
            scalesize = new Vector3(0.01f, 0.01f);
            scale_count = 0;
            stopHeart = false;
            firstHeart = true;

            r = GI.r;
            for(int i= 0; i< GI.trashes; i++)
            {
                GameObject newtrash = new GameObject();
                newtrash.transform.SetParent(main.transform);
                newtrash.transform.position = new Vector3(r.Next(-400, 400) + (float)r.NextDouble(), r.Next(-100, 100) + (float)r.NextDouble());
                newtrash.transform.localScale = new Vector3(0.3f, 0.3f);
                newtrash.AddComponent<Image>();
                string tmp = GI.str_trash[r.Next(0, GI.str_trash.Length)];
                newtrash.GetComponent<Image>().sprite = GI.sprite_Trash[tmp];
                newtrash.name = tmp;
                newtrash.AddComponent<MainTrash>();
            }
            time = r.Next(10,60) + (float)r.NextDouble();
        }

        // Use this for initialization
        void Start () {
            point = GI.curpoint;
            step = GameObject.Find("StepCounter").GetComponent<StepCounter>().dailywalkText;

            PointText = GameObject.Find("포인트").GetComponent<Text>();
            StepText = GameObject.Find("걸음수").GetComponent<Text>();

            PointText.text = string.Format("{0:N0}", point);
            StepText.text = step;
            isclicked = false;
            CheckYn = true;
        }
	
	    // Update is called once per frame
	    void Update () {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                GI.trashes++;
                GameObject newtrash = new GameObject();
                newtrash.transform.SetParent(main.transform);
                newtrash.transform.position = new Vector3(r.Next(-400, 400) + (float)r.NextDouble(), r.Next(-100, 100) + (float)r.NextDouble());
                newtrash.transform.localScale = new Vector3(0.3f, 0.3f);
                newtrash.AddComponent<Image>();
                string tmp = GI.str_trash[r.Next(0, GI.str_trash.Length)];
                newtrash.GetComponent<Image>().sprite = GI.sprite_Trash[tmp];
                newtrash.name = tmp;
                newtrash.AddComponent<MainTrash>();

                time = r.Next(10, 60) + (float)r.NextDouble();
            }

            point = GI.curpoint;
            step = GameObject.Find("StepCounter").GetComponent<StepCounter>().dailywalkText;
            PointText.text = string.Format("{0:N0}", point);
            StepText.text = "걸음수 : " + step;

            if (GI.update_heart)
            {
                if (scale_count < 60 && !stopHeart)
                {
                    if (firstHeart)
                    {
                        GameObject.Find("하트" + GI.hearts.ToString()).GetComponent<Image>().sprite = GI.sprite_ETC["하트"];
                        firstHeart = false;
                    }
                    GameObject.Find("하트" + GI.hearts.ToString()).transform.localScale = GameObject.Find("하트" + GI.hearts.ToString()).transform.localScale + scalesize;
                    scale_count++;
                }
                else
                {
                    stopHeart = true;
                    GameObject.Find("하트" + GI.hearts.ToString()).transform.localScale = GameObject.Find("하트" + GI.hearts.ToString()).transform.localScale - scalesize;
                    scale_count--;
                    if (scale_count == 0)
                    {
                        firstHeart = true;
                        GI.update_heart = false;
                        stopHeart = false;
                    }
                }
            }

            if (GI.point_queue.Count != 0)
            {
                List<string> tmp = new List<string>();
                foreach (string newpoint in GI.point_queue)
                {
                    tmp.Add(newpoint);
                }
                foreach (string newpoint in tmp)
                {
                    GI.point_queue.Remove(newpoint);
                    GameObject Ob1 = new GameObject();
                    Ob1.name = "newpoint";
                    Ob1.transform.position = GameObject.Find("포인트").transform.position + new Vector3(0, -80);
                    Ob1.transform.SetParent(GameObject.Find("메인화면").transform);
                    Ob1.AddComponent<Text>();
                    Ob1.GetComponent<Text>().text = newpoint;
                    Ob1.GetComponent<Text>().font = GI.arial_font;
                    Ob1.GetComponent<Text>().fontSize = 20;
                    Ob1.GetComponent<Text>().color = new Color(0, 0, 0);
                    Ob1.AddComponent<ChangePoint>();
                }
            }

            if (isHeart)
            {
                enterCountValue++;
                if (!IsInvoking("disable_Click"))
                    Invoke("disable_Click", 1f);
                isHeart = false;
            }
            if (enterCountValue == 5)
            {
                CancelInvoke("disable_Click");
                GameObject.Find("Main Camera").GetComponent<Places>().isok = true;
                GameObject.Find("Point Light").GetComponent<Image>().color = new Color(0, 0, 255);
            }
        }

        public void GotoStat()
        {
            SceneManager.LoadScene("통계화면");
        }

        public void GotoMinigames()
        {
            SceneManager.LoadScene("미니게임선택");
        }

        public void OnClickStore()
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            isclicked = !isclicked;
            if (isclicked)
            {
                GameObject.Find("동물상점").GetComponent<Canvas>().sortingLayerName = "result";
            }
            else
            {
                GameObject.Find("동물상점").GetComponent<Canvas>().sortingLayerName = "default";
            }
        }

        public void OnClickclose1()
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            GameObject.Find("동물상점").GetComponent<Canvas>().sortingLayerName = "Default";
            Debug.Log("Clik");
        }
        public void OnClickclose2()
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            GameObject.Find("식물상점").GetComponent<Canvas>().sortingLayerName = "Default";
            Debug.Log("Clik2");

        }
        public void OnClickPlant()
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            GameObject.Find("식물상점").GetComponent<Canvas>().sortingLayerName = "result";
            GameObject.Find("동물상점").GetComponent<Canvas>().sortingLayerName = "Default";
        }
        public void OnClickAnimal()
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            GameObject.Find("동물상점").GetComponent<Canvas>().sortingLayerName = "result";
            GameObject.Find("식물상점").GetComponent<Canvas>().sortingLayerName = "Default";
        }
        public void OnClickMusic()
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            if (music_num != 3)
            {
                music_num++;
            }
            else
            {
                music_num = 0;
            }
            GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = GI.clip_BGM[music_num];
            GameObject.Find("음악").GetComponent<Image>().sprite = GI.sprite_Musics[music_num];
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        }

        public void OnClickHeart()
        {
            isHeart = true;
        }

        void disable_Click()
        {
            enterCountValue = 0;
        }

        public void get_right(sellScript gb)
        {
            if (prev == null) prev = gb;
            prev.cansell = false;
            gb.cansell = true;
            prev = gb;
        }
    }
}
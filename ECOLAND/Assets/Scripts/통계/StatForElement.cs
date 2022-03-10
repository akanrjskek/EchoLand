using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatForElement : MonoBehaviour {
    GameObject dict_animal, dict_plant, dict, cafe;
    ArrayList items;
    Dictionary<string,int> count;
    ForGlobalInformation GI;
    public bool left;
    public float time;

    // Use this for initialization
    void Start () {
        count = new Dictionary<string, int>();
        count["치즈고양이1"] = 0;
        count["토끼1"] = 0;
        count["얼룩강아지1"] = 0;
        count["사자1"] = 0;
        count["백조1"] = 0;
        count["유니콘1"] = 0;
        count["나무1"] = 0;
        count["나무2"] = 0;
        count["나무3"] = 0;
        count["나무4"] = 0;
        count["돛단배"] = 0;
        count["보라튤립"] = 0;
        count["부바르디아"] = 0;
        count["선인장"] = 0;
        count["은행나무"] = 0;
        count["장미"] = 0;
        count["해바라기"] = 0;

        GI = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>();
        items = GI.items;
        foreach(ITEMINFO item in items)
        {
            if (count.ContainsKey(item.ITEM))
            {
                count[item.ITEM]++;
                GameObject.Find(item.ITEM.Replace("1","") + "수량").GetComponent<Text>().text = 
                    GameObject.Find(item.ITEM.Replace("1", "") + "수량").GetComponent<Text>().text.Replace((count[item.ITEM] - 1).ToString(), count[item.ITEM].ToString());
            }
        }
        left = true;
        time = 0.5f;
        dict_animal = GameObject.Find("동물도감리스트");
        dict_plant = GameObject.Find("식물도감리스트");
        dict = GameObject.Find("도감");
        cafe = GameObject.Find("카페통계");
    }
	
	// Update is called once per frame
	void Update () {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (left)
            {
                GameObject.Find("치즈고양이사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("치즈고양이사진").GetComponent<Image>().sprite.name.Replace("1", "2")];
                GameObject.Find("토끼사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("토끼사진").GetComponent<Image>().sprite.name.Replace("1", "2")];
                GameObject.Find("얼룩강아지사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("얼룩강아지사진").GetComponent<Image>().sprite.name.Replace("1", "2")];
                GameObject.Find("사자사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("사자사진").GetComponent<Image>().sprite.name.Replace("1", "2")];
                GameObject.Find("백조사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("백조사진").GetComponent<Image>().sprite.name.Replace("1", "2")];
                GameObject.Find("유니콘사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("유니콘사진").GetComponent<Image>().sprite.name.Replace("1", "2")];
                left = false;
            }
            else
            {
                GameObject.Find("치즈고양이사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("치즈고양이사진").GetComponent<Image>().sprite.name.Replace("2", "1")];
                GameObject.Find("토끼사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("토끼사진").GetComponent<Image>().sprite.name.Replace("2", "1")];
                GameObject.Find("얼룩강아지사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("얼룩강아지사진").GetComponent<Image>().sprite.name.Replace("2", "1")];
                GameObject.Find("사자사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("사자사진").GetComponent<Image>().sprite.name.Replace("2", "1")];
                GameObject.Find("백조사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("백조사진").GetComponent<Image>().sprite.name.Replace("2", "1")];
                GameObject.Find("유니콘사진").GetComponent<Image>().sprite = GI.sprite_Animals[GameObject.Find("유니콘사진").GetComponent<Image>().sprite.name.Replace("2", "1")];
                left = true;
            }
            time = 0.5f;
        }
    }
    public void OnClickAnimal()
    {
        dict_animal.transform.SetParent(dict.transform);
        dict_plant.transform.SetParent(cafe.transform);
    }
    public void OnClickPlant()
    {
        dict_animal.transform.SetParent(cafe.transform);
        dict_plant.transform.SetParent(dict.transform);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    private GameObject Ob1;
    public GameObject main;
    private GameObject point;
    public GameObject Plant;
    private Vector3 point_position;
    private GameObject Po;
    public string[] animal, plant;
    public int[] animalpoint, plantpoint;
    private GameObject main2;
    public AudioSource audioSource;
    ForGlobalInformation GI;

    private void Awake()
    {
        GI = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>();
        Plant = GameObject.Find("식물상점");
        point = GameObject.Find("빙하");
        point_position = point.transform.position;
        Po = GameObject.Find("GlobalInfo");
        main = GameObject.Find("메인화면");
        animalpoint = new int[] { -50, -100, -200, -400, -800, -1600 };
        plantpoint = new int[] { -30, -50, -70, -100, -150, -200, -300, -500, -700, -1000, -1500 };
    }

    public void OnClickObject()
    {
        audioSource = GameObject.Find("메인화면").GetComponent<AudioSource>();
        audioSource.Play();
        if (GameObject.Find("동물상점").GetComponent<Canvas>().sortingLayerName == "result")
        {
            int num = main.GetComponent<PedometerU.Main>().dict1[gameObject.GetComponent<Image>().sprite.name];
            if (Po.GetComponent<ForGlobalInformation>().curpoint >= (-animalpoint[num]))
            {
                GameObject.Find("동물상점").GetComponent<Canvas>().sortingLayerName = "default";
                Ob1 = new GameObject();
                Debug.Log(gameObject.GetComponent<Image>().sprite.name);
                Ob1.transform.parent = main.transform;
                Ob1.name = gameObject.GetComponent<Image>().sprite.name.Replace("left", "");
                Ob1.transform.position = point_position + new Vector3(0, 40, 0);
                Ob1.transform.localScale = new Vector3(0.3f, 0.3f);
                Ob1.AddComponent<Drag>();
                Ob1.AddComponent<Image>();
                Ob1.GetComponent<Image>().sprite = GI.sprite_Animals[gameObject.GetComponent<Image>().sprite.name.Replace("left","")];
                Ob1.AddComponent<BoxCollider2D>();
                Ob1.GetComponent<BoxCollider2D>().size += new Vector2(100f, 100f);

                GameObject ob2 = new GameObject();
                ob2.AddComponent<Image>().sprite = GI.sprite_ETC["체크"];
                ob2.transform.parent = main.transform;
                ob2.transform.position = point_position + new Vector3(0, 80, 0);
                ob2.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                ob2.AddComponent<Check>();
                ob2.GetComponent<Check>().ob = Ob1;

                Debug.Log("생성");
            }
            else
            {
                Debug.Log("포인트가 부족하여 구매할 수 없습니다.");
            }
        }
        else
        {
            if (GameObject.Find("식물상점").GetComponent<Canvas>().sortingLayerName == "result")
            {
                int num = main.GetComponent<PedometerU.Main>().dict2[gameObject.GetComponent<Image>().sprite.name];
                if (Po.GetComponent<ForGlobalInformation>().curpoint >= (-plantpoint[num]))
                {
                    AudioSource audio = main.GetComponent<AudioSource>();
                    audio.Play();
                    GameObject.Find("식물상점").GetComponent<Canvas>().sortingLayerName = "default";
                    Ob1 = new GameObject();
                    Ob1.name = gameObject.GetComponent<Image>().sprite.name;
                    Ob1.transform.parent = main.transform;
                    Ob1.transform.position = point_position + new Vector3(0, 40, 0);
                    Ob1.transform.localScale = new Vector3(0.3f, 0.3f);
                    Ob1.AddComponent<Drag>();
                    Ob1.AddComponent<Image>();
                    Ob1.GetComponent<Image>().sprite = GI.sprite_Plants[gameObject.GetComponent<Image>().sprite.name];
                    Ob1.AddComponent<BoxCollider2D>();
                    Ob1.GetComponent<BoxCollider2D>().size += new Vector2(100f, 100f);

                    GameObject ob2 = new GameObject();
                    ob2.AddComponent<Image>().sprite = GI.sprite_ETC["체크"];
                    ob2.transform.parent = main.transform;
                    ob2.transform.position = point_position + new Vector3(0, 80, 0);
                    ob2.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                    ob2.AddComponent<Check>();
                    ob2.GetComponent<Check>().ob = Ob1;
                    Debug.Log("생성");
                }
                else
                {
                    Debug.Log("포인트가 부족하여 구매할 수 없습니다.");
                }
            }
        }
    }
}
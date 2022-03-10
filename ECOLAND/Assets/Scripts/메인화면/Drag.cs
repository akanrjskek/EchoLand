using LitJson;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Drag : MonoBehaviour {
    //float distance = 10;
    private Vector3 mousePosition;
    private Vector3 Ob1Position;
    public Vector3 m_curPos;
    public Vector3 m_prevPos;
    public bool isclicked, onland, purchased, isox;
    private int[] animalpoint, plantpoint;

    ForGlobalInformation GI;
    GameObject sel_O, sel_X;
    Vector3 init_pos;

    private void Start()
    {
        init_pos = gameObject.transform.position;
        onland = false;
        isclicked = false;
        purchased = false;
        isox = false;
        GI = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>();
        animalpoint = new int[] { -50, -100, -200, -400, -800, -1600 };
        plantpoint = new int[] { -30, -50, -70, -100, -150, -200, -300, -500, -700, -1000, -1500 };
    }

    private void Update()
    {
        if (purchased)
        {
            purchased = false;
            Destroy(sel_O);
            Destroy(sel_X);
            ITEMINFO tmp = new ITEMINFO();
            tmp.ITEM = gameObject.name;
            tmp.LOC_X = transform.position.x.ToString();
            tmp.LOC_Y = transform.position.y.ToString();

            GameObject.Find("메인화면").GetComponent<PedometerU.Main>().items.Add(tmp);

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["DEVICE"] = SystemInfo.deviceUniqueIdentifier;
                values["ITEM"] = tmp.ITEM;
                values["LOC_X"] = tmp.LOC_X;
                values["LOC_Y"] = tmp.LOC_Y;

                var response = client.UploadValues("http://13.124.169.44:3000/update_item", values);

                var responseString = Encoding.Default.GetString(response);
                JsonData data = JsonMapper.ToObject(responseString);

                if (data["resultCode"].ToString() == "200")
                {
                    Debug.Log("아이템 업데이트 성공");
                }
                else
                {
                    Debug.Log("Error 발생");
                }
            }
            GetComponent<BoxCollider2D>().size = new Vector2(10f, 10f);
            
            gameObject.AddComponent<Items>();
            gameObject.GetComponent<Items>().info.ITEM = tmp.ITEM;
            gameObject.GetComponent<Items>().info.LOC_X = tmp.LOC_X;
            gameObject.GetComponent<Items>().info.LOC_Y = tmp.LOC_Y;
            gameObject.GetComponent<Items>().id = GI.numofanimals;
            GI.numofanimals++;

            if (GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().dict_Animals.ContainsKey("left" + this.gameObject.name))
            {
                gameObject.GetComponent<Items>().isanimal = true;
                int num = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().dict_Animals[gameObject.GetComponent<Image>().sprite.name];
                GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().updatepoint(false, animalpoint[num]);
            }
            else
            {
                gameObject.GetComponent<Items>().isanimal = false;
                int num = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().dict_Plants[gameObject.GetComponent<Image>().sprite.name];
                GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().updatepoint(false, plantpoint[num]);
                Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            }
            gameObject.AddComponent<Button>();
            gameObject.AddComponent<sellScript>();
            Destroy(this);
        }
    }

    private void OnMouseDown()
    {
        isclicked = true;
        m_prevPos = m_curPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("클릭");
    }

    private void OnMouseDrag()
    {
        if (!isox)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_curPos = mousePosition;
            Vector3 gap = m_curPos - m_prevPos;
            gameObject.transform.position += gap;
            m_prevPos = m_curPos;
        }
    }

    private void OnMouseUp()
    {
        if (!isox)
        {
            if (!onland)
            {
                isclicked = false;
                GameObject ob2 = new GameObject();
                ob2.name = "check";
                ob2.AddComponent<Image>().sprite = GI.sprite_ETC["체크"];
                ob2.transform.SetParent(GameObject.Find("메인화면").transform);
                ob2.transform.position = GameObject.Find("빙하").transform.position + new Vector3(0, 80, 0);
                ob2.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                ob2.AddComponent<Check>();
                ob2.GetComponent<Check>().ob = this.gameObject;

                gameObject.transform.position = init_pos;
            }
            else
            {
                isox = true;
                sel_O = new GameObject();
                sel_O.name = "O";
                sel_O.AddComponent<Image>().sprite = GI.sprite_ETC["O"];
                sel_O.transform.SetParent(GameObject.Find("메인화면").transform);
                sel_O.transform.position = gameObject.transform.position + new Vector3(-20, 30, 0);
                sel_O.transform.localScale = new Vector3(0.25f, 0.25f, 1);
                sel_O.AddComponent<Button>().onClick.AddListener(() => createButtonClickListner());

                sel_X = new GameObject();
                sel_X.name = "X";
                sel_X.AddComponent<Image>().sprite = GI.sprite_ETC["X"];
                sel_X.transform.SetParent(GameObject.Find("메인화면").transform);
                sel_X.transform.position = gameObject.transform.position + new Vector3(20, 30, 0);
                sel_X.transform.localScale = new Vector3(0.25f, 0.25f, 1);
                sel_X.AddComponent<Button>().onClick.AddListener(() => removeButtonClickListner());
            }
        }
    }

    void createButtonClickListner()
    {
        purchased = true;
    }

    void removeButtonClickListner()
    {
        Destroy(sel_O);
        Destroy(sel_X);
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "섬")
        {
            onland = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "섬")
        {
            onland = false;
        }
    }
}

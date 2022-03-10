using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragController : MonoBehaviour
{
    private GameObject playgame;

    private Vector3 trash_pos;
    private Vector3 truck_pos;

    private GameObject trash;
    private GameObject truck;
    private GameObject gameresult;
    public Text timelabel;
    float making = 1.6f;
    public int point = 0;
    public float timecount = 20;
    public Text resulttext;

    public bool started;

    private string[] str, str2;

    ForGlobalInformation GI;

    private void Awake()
    {
        GI = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>();
        playgame = GameObject.Find("게임진행창");
        trash = GameObject.Find("쓰레기");
        truck = GameObject.Find("Truck");
        str = new string[] { "can", "귤", "컵", "유리병","달걀껍질","닭뼈","담배","빨대","수정테이프","연필","전구","족발"
        ,"참치캔","커피찌꺼기","헌옷","헌운동화","화이트","의자","사이다","맥주병","맥주캔","참이슬","플라스틱그릇","플라스틱컵","플라스틱숟가락","향수병","스테인리스1","스테인리스2"};
        str2 = new string[] { "캔", "일반쓰레기", "일반쓰레기", "유리","일반쓰레기","일반쓰레기","일반쓰레기","일반쓰레기","플라스틱","일반쓰레기","일반쓰레기","일반쓰레기"
        ,"캔","일반쓰레기","일반쓰레기","일반쓰레기","플라스틱","플라스틱","캔","유리","캔","유리","플라스틱","플라스틱","플라스틱","유리","캔","캔"};

        System.Random r = new System.Random();
        int num = r.Next(0, str.Length);

        trash.GetComponent<SpriteRenderer>().sprite = GI.sprite_Trash[str[num]];
        trash.tag = str2[num];

        trash_pos = trash.transform.position;
        truck_pos = truck.transform.position;

        started = false;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (making > 0)
            {
                making -= Time.deltaTime;
            }
            else
            {
                makeTrash();
                making = 1.3f;
            }

            if (timecount > 0)
            {
                timecount -= Time.deltaTime;
                timelabel.text = string.Format("{0:N2}", timecount);
            }
            else
            {
                timelabel.text = string.Format("{0:N2}", 0);
                started = false;
                resulttext.text = "+" + point + " P";
                GI.updatepoint(true, point);
                gameresult = GameObject.Find("결과창");
                gameresult.GetComponent<Canvas>().sortingLayerName = "result";
            }
        }
    }


    public void makeTrash()
    {
        System.Random r = new System.Random();
        int num = r.Next(0, str.Length);

        trash = new GameObject();
        trash.transform.parent = playgame.transform;
        trash.name = "쓰레기";
        truck = new GameObject();
        truck.transform.parent = playgame.transform;
        truck.name = "트럭";

        trash.transform.position = trash_pos - new Vector3(-40, 0);
        trash.transform.localScale = new Vector3(20, 20, 1);

        truck.transform.position = truck_pos - new Vector3(-40, 0);
        truck.transform.localScale = new Vector3(10, 10, 1);

        trash.AddComponent<SpriteRenderer>();
        trash.GetComponent<SpriteRenderer>().sortingLayerName = "Unit";
        trash.GetComponent<SpriteRenderer>().sortingOrder = 1;
        trash.GetComponent<SpriteRenderer>().sprite = GI.sprite_Trash[str[num]];

        truck.AddComponent<SpriteRenderer>();
        truck.GetComponent<SpriteRenderer>().sortingLayerName = "Unit";
        truck.GetComponent<SpriteRenderer>().sortingOrder = 2;
        truck.GetComponent<SpriteRenderer>().sprite = GI.sprite_ETC["트럭"];

        trash.AddComponent<BoxCollider2D>();
        trash.GetComponent<BoxCollider2D>().isTrigger = true;
        trash.GetComponent<BoxCollider2D>().size = new Vector2(4.64f, 3.12f);

        trash.AddComponent<Rigidbody2D>();
        trash.GetComponent<Rigidbody2D>().mass = 0.0001f;
        trash.GetComponent<Rigidbody2D>().drag = 0;
        trash.GetComponent<Rigidbody2D>().angularDrag = 1;
        trash.GetComponent<Rigidbody2D>().gravityScale = 0;

        trash.AddComponent<Out>();
        truck.AddComponent<Truck>();

        trash.tag = str2[num];
    }

    public void GotoMain()
    {
        AudioSource audio = GameObject.Find("결과창").GetComponent<AudioSource>();
        audio.Stop();
        SceneManager.LoadScene("메인화면");
    }

    public void Start_Game()
    {
        GI.sub_heart();
        GameObject gb = GameObject.Find("문제설명창");
        gb.GetComponent<Canvas>().sortingLayerName = "Default";
        started = true;
    }
}
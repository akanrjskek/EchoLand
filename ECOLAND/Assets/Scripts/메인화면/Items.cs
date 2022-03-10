using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour {
    public System.Random r;
    public double timecount, stepcount;
    public Vector3 current;
    public bool isout, isleft, isanimal, waiting;
    public int num;
    string leftstep, rightstep;
    ForGlobalInformation GI;
    public ITEMINFO info;
    public int id;
    public GameObject main, standard;

    void Awake () {
        isout = false;
        timecount = 0;
        stepcount = 1;
        leftstep = "1";
        rightstep = "2";
        isleft = true;
        r = new System.Random();
        GI = GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>();
        isanimal = false;
        info = new ITEMINFO();
        waiting = false;
        main = GameObject.Find("메인화면");
        standard = GameObject.Find("standard");
    }
	
	// Update is called once per frame
	void Update () {
        if (isanimal)
        {
            if (!isout)
            {
                if (stepcount <= 0 && waiting)
                {
                    if (isleft)
                    {
                        leftstep = "2";
                        rightstep = "1";
                        isleft = false;
                    }
                    else
                    {
                        leftstep = "1";
                        rightstep = "2";
                        isleft = true;
                    }
                    gameObject.GetComponent<Image>().sprite = GI.sprite_Animals[gameObject.GetComponent<Image>().sprite.name.Replace(leftstep, rightstep)];
                    stepcount = 0.7f;
                }
                if (stepcount <= 0 && num != 4)
                {
                    if (isleft)
                    {
                        leftstep = "2";
                        rightstep = "1";
                        isleft = false;
                    }
                    else
                    {
                        leftstep = "1";
                        rightstep = "2";
                        isleft = true;
                    }
                    gameObject.GetComponent<Image>().sprite = GI.sprite_Animals[gameObject.GetComponent<Image>().sprite.name.Replace(leftstep, rightstep)];
                    stepcount = 1;
                }
                else
                {
                    stepcount -= Time.deltaTime;
                }

                if (timecount > 0)
                {
                    this.gameObject.transform.position += current;
                    timecount -= Time.deltaTime;
                }
                else
                {
                    if (waiting)
                    {
                        gameObject.GetComponent<Image>().sprite = GI.sprite_Animals[gameObject.GetComponent<Image>().sprite.name.Replace("touch","")];
                        waiting = false;
                    }
                    waiting = false;
                    num = r.Next(0, 5);
                    switch (num)
                    {
                        case 0:
                            current = new Vector3(0.35f, 0, 0);
                            gameObject.GetComponent<Image>().sprite = GI.sprite_Animals[gameObject.GetComponent<Image>().sprite.name.Replace("left", "right")];
                            break;
                        case 1:
                            current = new Vector3(0, 0.35f, 0);
                            gameObject.GetComponent<Image>().sprite = GI.sprite_Animals[gameObject.GetComponent<Image>().sprite.name];
                            break;
                        case 2:
                            current = new Vector3(-0.35f, 0, 0);
                            gameObject.GetComponent<Image>().sprite = GI.sprite_Animals[gameObject.GetComponent<Image>().sprite.name.Replace("right", "left")];
                            break;
                        case 3:
                            current = new Vector3(0, -0.35f, 0);
                            gameObject.GetComponent<Image>().sprite = GI.sprite_Animals[gameObject.GetComponent<Image>().sprite.name];
                            break;
                        default:
                            current = new Vector3(0, 0, 0);
                            break;
                    }

                    r = new System.Random();
                    timecount = r.NextDouble() + r.Next(0, 4);
                }
            }
            else
            {
                if(gameObject.transform.position.x < standard.transform.position.x)
                {
                    this.gameObject.transform.position += new Vector3(0.35f,0);
                }
                else
                {
                    this.gameObject.transform.position += new Vector3(-0.35f, 0);
                }
                if(gameObject.transform.position.y < standard.transform.position.y)
                {
                    this.gameObject.transform.position += new Vector3(0, 0.35f);
                }
                else
                {
                    this.gameObject.transform.position += new Vector3(0, -0.35f);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "섬")
        {
            isout = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isanimal)
        {
            isout = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "섬")
        {
            isout = false;
        }
    }
}

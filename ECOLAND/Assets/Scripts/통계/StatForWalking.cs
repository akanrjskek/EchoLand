using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatForWalking : MonoBehaviour {
    public Text[] Walkings, Days;
    float time;
    stat statScript;
    int counting;
    GameObject LineGraph;
    Vector3[] linepos;
    LineRenderer LR;

    // Use this for initialization
    void Start () {
        statScript = GameObject.Find("Main Camera").GetComponent<stat>();
        if(statScript.stats.Count < 6)
        {
            counting = statScript.stats.Count - 1;
            for(int i = 0; i < statScript.stats.Count; i++)
            {
                GameObject.Find("Day" + (6-i).ToString()).GetComponent<Text>().text = statScript.stats[i].FLAG_DATE;
            }
        }
        else
        {
            counting = 6;
            for (int i = 0; i < 7; i++)
            {
                GameObject.Find("Day" + (6 - i).ToString()).GetComponent<Text>().text = statScript.stats[i].FLAG_DATE;
            }
        }

        LineGraph = new GameObject();
        LineGraph.name = "걸음수";
        LineGraph.transform.SetParent(GameObject.Find("걸음수통계").transform);
        LineGraph.AddComponent<LineRenderer>();
        LR = LineGraph.GetComponent<LineRenderer>();
        LR.useWorldSpace = true;
        LR.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        LR.receiveShadows = true;
        LR.allowOcclusionWhenDynamic = true;
        linepos = new Vector3[7];
        for (int i = 0; i < 7; i++)
        {
            linepos[i].x = -125f + 87.5f * i;
            linepos[i].y = -150;
            linepos[i].z = 1;
        }
        LR.positionCount = 0;
        LR.SetPositions(linepos);
        LR.startWidth = 5;
        LR.endWidth = 5;

        for(int i = 0; i < 7; i++)
        {
            GameObject.Find("걸음수" + (6-i).ToString()).GetComponent<Text>().text = ((statScript.maxWALK / 6) * (6 - i)).ToString();
        }
        time = 0.1f;
    }

    // Update is called once per frame
    void Update ()
    {
        if(LineGraph != null)
        {
            if (counting < 0)
            {
                Destroy(this);
            }
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                LR.positionCount++;
                linepos[6 - counting].y += (GameObject.Find("걸음수6").transform.position.y - GameObject.Find("걸음수0").transform.position.y) * statScript.stats[counting].DAILYWALK / statScript.maxWALK;
                LR.SetPositions(linepos);

                counting--;
                if (counting < 0)
                {
                    Destroy(this);
                }
                time = 0.1f;
            }
        }
	}
}

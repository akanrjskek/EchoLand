using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour {
    int count, pos;
    public GameObject ob;
    private void Start()
    {
        count = 0;
        pos = 1;
    }

    // Update is called once per frame
    void Update () {
        if (ob.GetComponent<Drag>().isclicked)
        {
            Destroy(gameObject);
        }
        if (count == 10)
        {
            if (pos == 1)
            {
                pos = -1;
            }
            else
            {
                pos = 1;
            }
            count = 0;
        }
        transform.position += new Vector3(0, pos);
        count++;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePoint : MonoBehaviour {
    float TimeCount;

    private void Start()
    {
        TimeCount = 5;
    }

    // Update is called once per frame
    void Update () {
        if (TimeCount > 0)
        {
            gameObject.transform.position += new Vector3(0, 4);
            TimeCount -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);

        }
    }
}

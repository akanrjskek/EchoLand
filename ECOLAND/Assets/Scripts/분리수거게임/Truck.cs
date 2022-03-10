using UnityEngine;

public class Truck : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Main Camera").GetComponent<DragController>().started)
        {
            if (GameObject.Find("Main Camera").GetComponent<DragController>().timecount <= 0)
            {
                Destroy(this.gameObject);
            }
            this.gameObject.transform.position += new Vector3(2, 0);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
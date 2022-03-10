using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTrash : MonoBehaviour {
    private void Awake()
    {
        gameObject.AddComponent<Button>().onClick.AddListener(() => removeButtonClickListner());
    }

    void removeButtonClickListner()
    {
        GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().updatepoint(true, 2);
        Destroy(gameObject);
    }
}

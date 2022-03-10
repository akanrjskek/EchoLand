using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour {
    public void OnClickLoginAnonymous()
    {
        SceneManager.LoadScene("메인화면");
    }
}
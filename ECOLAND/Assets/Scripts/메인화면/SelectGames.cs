using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectGames : MonoBehaviour {
    private void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
    public void GotoOX()
    {
        if(GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().isokheart())
            SceneManager.LoadScene("OX게임");
    }

    public void GotoDrag()
    {
        if (GameObject.Find("GlobalInfo").GetComponent<ForGlobalInformation>().isokheart())
            SceneManager.LoadScene("분리수거게임");
    }

    public void GotoMain()
    {
        SceneManager.LoadScene("메인화면");
    }
}

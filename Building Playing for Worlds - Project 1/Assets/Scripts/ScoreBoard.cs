using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI Timee;
    public TextMeshProUGUI Killed;
    public TextMeshProUGUI Hit;
    public static bool lavadeath = false;
    public static float timer;
    public static int killed;
    public static int hit;

    public static int endscore;
    

    private void Start()
    {
        lavadeath = false;
        timer = 0;
        killed = 0;
        hit = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
        float seconds = timer;

        if(lavadeath == true)
        {
            int timescore = (int)seconds;
            endscore = (timescore + (killed * 3));
            SceneManager.LoadScene("Scenes/GameOverMenu");
        }
        if (hit >= 10)
        {
            int timescore = (int)seconds;
            endscore = (timescore + (killed * 3));
            SceneManager.LoadScene("Scenes/GameOverMenu");

        }
        Timee.text = ""+seconds;
        Killed.text = ""+killed;
        Hit.text = ""+hit;
    }


}

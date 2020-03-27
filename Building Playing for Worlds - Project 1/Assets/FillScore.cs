using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FillScore : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI tekst;
    void Start()
    {
        tekst.text = ""+ScoreBoard.endscore;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

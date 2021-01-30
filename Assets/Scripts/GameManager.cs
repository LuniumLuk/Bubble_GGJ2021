using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int gameLevel;//关卡

    int progress1 = 0, progress2 = 0; 

    public Text ProgressText;

    public void AddProgress()
    {
        if(gameLevel == 1)
        {
            progress1++;
        }
        if(gameLevel == 2)
        {
            progress2++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameLevel == 1)
        {
            ProgressText.text = progress1.ToString() + " / 5";
        }
        if (gameLevel == 2)
        {
            ProgressText.text = progress2.ToString() + " / 6";
        }
    }
}

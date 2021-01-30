using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int gameLevel;//关卡

    int progress1 = 0, progress2 = 0;

    public Transform canvas = null;
    public GameObject D3 = null;
    bool Dialogue3 = false;

    public Text ProgressText;

    private float countDown = 1f;

    private float timer = 0;

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
        //while(timer < countDown)
        //{
            //timer += Time.deltaTime;
            //fakeChild.transform.localPosition += Vector3.right * Time.deltaTime * 5f;
        //}
        if(progress1 == 6)
        {
            Settings.splitAbility = true;
            if(Dialogue3 == false)
            {
                Instantiate(D3,canvas);
                Dialogue3 = true;
            }
        }

        if(gameLevel == 1)
        {
            ProgressText.text = progress1.ToString() + " / 6";
        }
        if (gameLevel == 2)
        {
            ProgressText.text = progress2.ToString() + " / 6";
        }
    }
}

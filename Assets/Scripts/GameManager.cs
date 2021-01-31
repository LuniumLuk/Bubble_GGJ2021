using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int gameLevel;//关卡

    int progress1 = 0, progress2 = 0;

    public Transform canvas = null;
    public GameObject D3 = null;
    public GameObject D4 = null;
    bool Dialogue3 = false;
    bool level2Start = false;

    float rate = 20f;
    public GameObject Wall1 = null;
    public GameObject Wall2 = null;
    public GameObject Wall3 = null;

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
        Wall1.transform.Rotate(Vector3.forward * Time.deltaTime * rate);
        Wall2.transform.Rotate(Vector3.forward * Time.deltaTime * rate);
        Wall3.transform.Rotate(Vector3.forward * Time.deltaTime * rate);
        //while(timer < countDown)
        //{
        //timer += Time.deltaTime;
        //fakeChild.transform.localPosition += Vector3.right * Time.deltaTime * 5f;
        //}
        if (progress1 == 6)
        {
            Settings.splitAbility = true;
            if(Dialogue3 == false)
            {
                Instantiate(D3,canvas);
                Dialogue3 = true;
                gameLevel = 2;
                ProgressText.text = progress2.ToString() + " / 9";
            }
        }
        //GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = 1;

        if (gameLevel == 1)
        {
            ProgressText.text = progress1.ToString() + " / 6";
        }
        if (gameLevel == 2 && progress2>=1)
        {
            if(GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize < 3)
            {
                GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize += 0.01f; 
            }
            else
            {
                if (level2Start == false)
                {
                    Instantiate(D4, canvas);
                    level2Start = true;
                }
            }
            ProgressText.text = progress2.ToString() + " / 9";
        }
    }

    public void Retry()
    {
        Settings.splitAbility = false;
        SceneManager.LoadScene(0);
    }
}

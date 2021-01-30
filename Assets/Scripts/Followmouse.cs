using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Followmouse : MonoBehaviour
{
	
	//public Image gogo;
	Vector3  screenPosition;
	Vector3  mousePositionOnScreen;
	Vector3  mousePositionInWorld;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//物体的世界坐标转换为屏幕坐标
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		//获取鼠坐标
		mousePositionOnScreen = Input.mousePosition;
		//让鼠标坐标的Z=物体的坐标Z
		mousePositionOnScreen.z = 0;
		screenPosition.z = 0;
		//将鼠标坐标转化为世界坐标
		//mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
		//物体跟随鼠标移动
		transform.position = mousePositionOnScreen;
        if (Input.GetMouseButton(1)){
        //gogo.GetComponent<Image>().color=new Color((255/255f),(255/255f),(255/255f),(255/255f));
		GetComponent<Image>().color=new Color((255/255f),(255/255f),(255/255f),(255/255f));
		}
		else {
			//gogo.GetComponent<Image>().color=new Color((255/255f),(255/255f),(255/255f),(0/255f));
			GetComponent<Image>().color=new Color((255/255f),(255/255f),(255/255f),(0/255f));
		}
		
    }



}

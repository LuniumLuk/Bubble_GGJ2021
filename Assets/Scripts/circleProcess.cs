using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class circleProcess : MonoBehaviour {
	//public Image go;
	//[SerializeField]
	float speed;
	
	[SerializeField]
	Transform process;
	
	[SerializeField]
	Transform indicator;
	
	public int targetProcess{ get; set;}
	
	//currentAmout鼠标长按再运作，百分比和力量挂钩，这个函数有没有暂时不知道。
	private float currentAmout = 0;
	
	// Use this for initialization
	void Start () {
		targetProcess = 100;
        //speed = Settings.splitForce;
        speed = Split.addmass;
    }
	
	// Update is called once per frame
	void Update () {
		speed = Split.addmass;
		if (Input.GetMouseButton(1)){
		//go.GetComponent<Image>().color=new Color((255/255f),(0/255f),(0/255f),(255/255f));
		GetComponent<Image>().color=new Color((255/255f),(0/255f),(0/255f),(255/255f));
		if (currentAmout < targetProcess) {
			currentAmout += speed/10;
			if(currentAmout > targetProcess)
				currentAmout = targetProcess;
			process.GetComponent<Image>().fillAmount = currentAmout/100.0f;
		}
		}
		else {
			//go.GetComponent<Image>().color=new Color((255/255f),(0/255f),(0/255f),(0/255f));
			GetComponent<Image>().color=new Color((255/255f),(0/255f),(0/255f),(0/255f));
			currentAmout = 0;
		}
	}
	
	


}

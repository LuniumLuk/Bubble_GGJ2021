using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
 
    private float main_time;//记录点击时长
    public float maxHealth=100;//最大血量
    public float currentHealth = 100;//当前血量
    RectTransform rectTransform;
    private float width;//100  //记录血量宽度
                        //1000 
						
	BasicAttr attr = null;
	
	
    // Use this for initialization
    void Start () {
         rectTransform = GetComponent<RectTransform>();
        width = rectTransform.sizeDelta.x;
		
		attr = GetComponent<BasicAttr>();
    }
	
	// Update is called once per frame
	void Update () {
		//实时读取血条
            if (currentHealth>= maxHealth)
            {
                currentHealth = maxHealth;
            }
           if (currentHealth<=0)
           {
            currentHealth = 0;
            }
			// if (Input.GetMouseButton(0)){
			// if (main_time == 0.0f){
			// main_time = Time.time;
			// currentHealth = Thrust.addgas;
			// rectTransform.sizeDelta = new Vector2(width*10/maxHealth*currentHealth, rectTransform.sizeDelta.y);
			// }
			// if (Time.time - main_time > 1f) {
				 currentHealth = Thrust.addgas;
				 rectTransform.sizeDelta = new Vector2(width/maxHealth*currentHealth, rectTransform.sizeDelta.y);
			  //长按时执行的动作放这里
			// }
			
			
			// else{
			//	 main_time = 0.0f;
			// }
            
			
        
       
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thrust : MonoBehaviour
{
    BasicAttr attr = null;

    // 喷气特效
    public ParticleSystem emission = null;

    // 燃油数量
    private float gas = 0;

    public Text gasText = null;

    private void Awake()
    {
        attr = GetComponent<BasicAttr>();
        gas = Settings.gas;
    }

    private void Update()
    {
        // 鼠标左键点击
        if (Input.GetMouseButton(0) && attr.id == 0 && gas > 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            attr.speed += (transform.position - mousePos).normalized * Settings.thrustForce * Time.deltaTime;
            emission.transform.localEulerAngles = new Vector3(getAngle(transform.position - mousePos), 90f, 90f);
            gas -= Time.deltaTime * Settings.gasConsume;
            // 更新UI文字
            gasText.text = "Gas: " + gas.ToString();
            emission.Emit(1);
        }

    }

    private float getAngle(Vector3 direction)
    {
        if (direction.x > 0)
        {
            return -Mathf.Atan(direction.y / direction.x) * 180.0f / Mathf.PI - 180.0f;
        }
        else if (direction.x < 0)
        {
            return -Mathf.Atan(direction.y / direction.x) * 180.0f / Mathf.PI;
        }
        else
        {
            if (direction.y > 0)
                return 90f;
            else
                return -90f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // 主物体获取到燃料箱
        if(attr.id == 0 && other.gameObject.tag == "gastank") {
            Destroy(other.gameObject);
            gas += Settings.gasTank;
            // 更新UI文字
            gasText.text = "Gas: " + gas.ToString();
        }
    }
}

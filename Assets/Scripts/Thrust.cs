using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thrust : MonoBehaviour
{
    BasicAttr attr = null;

    // 喷气特效
    public ParticleSystem emission = null;
    public ParticleSystem emission1 = null;

    // 燃油数量
    private float gas = 100;

    public GameObject plus_UI = null;

    public Text gasText = null;

    //变化的燃油数量，用于血量条
    public static float addgas = 100;

    private void Awake()
    {
        attr = GetComponent<BasicAttr>();
        gas = Settings.gas;
        gasText.text = "Gas: " + 100 + '%';
        //rectTransform = GetComponent<RectTransform>();
        //width = rectTransform.sizeDelta.x;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.localEulerAngles = new Vector3(0, 0, 90f - getAngle(transform.position - mousePos));
        // 鼠标左键点击
        if (Input.GetMouseButton(0) && attr.id == 0 && gas > 0)
        {

            attr.speed += (transform.position - mousePos).normalized * Settings.thrustForce * Time.deltaTime;
            //emission.transform.localEulerAngles = new Vector3(getAngle(transform.position - mousePos), 90f, 90f);

            gas -= Time.deltaTime * Settings.gasConsume;
            // 更新UI文字
            gasText.text = "Gas: " + ((int)gas).ToString() + '%';
            //更新血条数值
            addgas = gas;
            emission.Emit(1);
            emission1.Emit(1);
            //开始火箭声音
            if (!AudioManager.instance.rocketAudio.isPlaying)
            {
                AudioManager.instance.rocketAudio.Play();
                print("RocketPlay");
            }
        }
        else if (!Input.GetMouseButton(0))
        {
            //开始火箭声音
            if (AudioManager.instance.rocketAudio.isPlaying)
            {
                AudioManager.instance.rocketAudio.Stop();
                print("RocketStop");


            }

        }

        //public static float gas1 = 1000f;
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        // 主物体获取到燃料箱
        if (attr.id == 0 && other.gameObject.tag == "gastank")
        {
            Vector3 position = other.transform.position + new Vector3(0.5f, 0.5f, 0);
            GameObject obj = Instantiate(plus_UI, position, Quaternion.identity);

            Destroy(other.gameObject);
            gas += Settings.gasTank;
            // 更新UI文字
            if (gas >= 100)
            {
                gas = 100;
            }
            gasText.text = "Gas: " + ((int)gas).ToString() + '%';
            //更新血条数值
            addgas = gas;

            //获得燃料瓶音效
            if (!AudioManager.instance.gasAudio.isPlaying)
            {
                AudioManager.instance.gasAudio.Play();
            }

        }

    }
}

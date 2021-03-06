using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{

    // 是否在使用鼠标右键拖拽，也可以起到保护bubble不融合的作用
    public bool dragging = false;
    BasicAttr attr = null;
    //follow
    public static float addmass = 0;
    // 用于分裂的新object
    GameObject newObject = null;

    // 用于指示方向的三角形，之后可以改成力度条或者其他贴图
    // public Transform pointer = null;

    public float del = 1f;

    public Sprite spriteChild = null;

    // 用于处理融合的临时gameObject
    private GameObject tempObject = null;
    private void Awake()
    {
        attr = GetComponent<BasicAttr>();
        //pointer.gameObject.SetActive(false);
    }


    private void FixedUpdate()
    {
        // 如果存在一个tempObject，进行融合处理
        if (tempObject)
        {
            Debug.LogWarning("colide and trgiger");
            float otherMass = tempObject.GetComponent<BasicAttr>().mass;
            Vector3 otherspeed = tempObject.GetComponent<BasicAttr>().speed;
            transform.position = (transform.position * attr.mass + tempObject.transform.position * otherMass) / (attr.mass + otherMass);
            // Destroy(tempObject);
            attr.speed = (attr.speed * attr.mass + otherspeed * otherMass) / (attr.mass + otherMass);
            
            attr.targetmass = attr.mass + otherMass;
            tempObject.GetComponent<BasicAttr>().targetmass = 0;
            AudioManager.instance.combainAudio.Play();
            //attr.mass += otherMass;
            //transform.localScale = Vector3.one * Mathf.Sqrt(attr.mass) * Settings.scaleC;
            tempObject = null;
        }
        if(!dragging) {
            if(Mathf.Abs(attr.mass - attr.targetmass) > del)
            {
                attr.mass += Mathf.Sign(attr.targetmass - attr.mass) * del;
                transform.localScale = Vector3.one * Mathf.Sqrt(attr.mass) * Settings.scaleC;
            }
            else if(attr.targetmass == 0) {
                if (attr.id == -1)
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().AddProgress();
                }
                Destroy(gameObject);
            }
        }



    }

    private void Update()
    {
        if(Settings.splitAbility)
        {
            // 物体为主物体且不出在拖动状态且右键按下
            if (!tempObject && attr.id == 0 && !dragging && !newObject && Input.GetMouseButton(1) && attr.mass > Settings.minimumMass)
            {
                dragging = true;
                Transform targetTransform = transform;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                float distance = Mathf.Sqrt(attr.mass - 1) + 1;
                attr.mass -= 1;
                transform.localScale = Vector3.one * Mathf.Sqrt(attr.mass) * Settings.scaleC;
                newObject = Instantiate(gameObject);
                // newObject.transform.position += (mousePos - transform.position).normalized * distance * Settings.dragDistanceC;

                //pointer.gameObject.SetActive(true);
                //pointer.localPosition = (mousePos - transform.position).normalized;

                newObject.transform.localScale = Vector3.one * Settings.scaleC;
                newObject.GetComponent<BasicAttr>().mass = 1;
                newObject.GetComponent<BasicAttr>().speed = Vector3.zero;
                newObject.GetComponent<BasicAttr>().id = 1;
                newObject.GetComponent<SpriteRenderer>().sprite = spriteChild;
            }
            // 如果为主物体且正在右键拖动中
            else if (attr.id == 0 && newObject && dragging)
            {
                // 右键按下
                if (Input.GetMouseButton(1))
                {
                    //先调用一次addmass，不然第一次会出现加载不出来的情况。
                    addmass = newObject.GetComponent<BasicAttr>().mass;
                    // 显示预览发射体的位置
                    Transform targetTransform = transform;
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    if (attr.mass - newObject.GetComponent<BasicAttr>().mass > 20f * Time.deltaTime)
                    {
                        attr.mass -= 10f * Time.deltaTime;
                        newObject.GetComponent<BasicAttr>().mass += 10f * Time.deltaTime;
                        float targetMass = newObject.GetComponent<BasicAttr>().mass;
                        transform.localScale = Vector3.one * Mathf.Sqrt(attr.mass) * Settings.scaleC;
                        newObject.transform.localScale = Vector3.one * Mathf.Sqrt(targetMass) * Settings.scaleC;
                    }
                    float distance = Mathf.Sqrt(attr.mass) + Mathf.Sqrt(newObject.GetComponent<BasicAttr>().mass);
                    newObject.transform.position = transform.position;
                    //pointer.localPosition = (mousePos - transform.position).normalized;
                    //newObject.transform.position = transform.position + (mousePos - transform.position).normalized * distance * Settings.dragDistanceC;
                }
                else
                {
                    // 发射分裂体
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    attr.targetmass = attr.mass;
                    attr.speed += (transform.position - mousePos).normalized * Settings.splitForce * newObject.GetComponent<BasicAttr>().mass;
                    newObject.GetComponent<BasicAttr>().speed += (mousePos - transform.position).normalized * Settings.splitForce * attr.mass;
                    newObject.GetComponent<BasicAttr>().targetmass = newObject.GetComponent<BasicAttr>().mass;
                    addmass = newObject.GetComponent<BasicAttr>().mass;
                    newObject = null;
                    //pointer.gameObject.SetActive(false);
                    //分裂声音
                    AudioManager.instance.ruptureAudio.Play();

                }
            }
            else if (newObject && !dragging)
            {
                // 发射分裂体
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                attr.targetmass = attr.mass;
                attr.speed += (transform.position - mousePos).normalized * Settings.splitForce / attr.mass;
                newObject.GetComponent<BasicAttr>().speed += (mousePos - transform.position).normalized * Settings.splitForce / newObject.GetComponent<BasicAttr>().mass;
                newObject.GetComponent<BasicAttr>().targetmass = newObject.GetComponent<BasicAttr>().mass;
                addmass = newObject.GetComponent<BasicAttr>().mass;
                newObject = null;
                //pointer.gameObject.SetActive(false);
                //分裂声音
                AudioManager.instance.ruptureAudio.Play();

            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 获取到接触到的bubble，因为挂了多个collider，所以这里会获取多次
        if (!tempObject && attr.id == 0 && !dragging && other.gameObject.tag == "bubble")
        {
            tempObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "bubble" && dragging)
        {
            Debug.LogWarning("trigger appear");
            dragging = false;
        }
        if (other.gameObject.tag == "bubble" && attr.id == 0 && dragging)
        {
            dragging = false;
        }
    }

}

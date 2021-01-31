using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollider : MonoBehaviour
{
    private BasicAttr attr = null;
    public Sprite spriteChild = null;

    private void Awake()
    {
        // 获取到BasicAttr脚本
        attr = GetComponent<BasicAttr>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint = other.contacts[0];
        if (other.gameObject.tag == "RotatingWall")
            attr.speed = Vector2.Reflect(attr.speed, contactPoint.normal) + contactPoint.normal * Settings.reflectForce;
        else
            attr.speed = Vector2.Reflect(attr.speed, contactPoint.normal);
        // 如果障碍物带有spike（尖刺）标签
        if (attr.mass > Settings.minimumMass && other.gameObject.tag == "spike")
        {
            GetComponent<Split>().dragging = true;
            attr.mass *= 0.5f;
            attr.targetmass = attr.mass;
            transform.localScale = Vector3.one * Mathf.Sqrt(attr.mass) * Settings.scaleC;
            GameObject newObject = Instantiate(gameObject);
            newObject.GetComponent<SpriteRenderer>().sprite = spriteChild;
            Debug.LogWarning(contactPoint.normal.ToString());
            // 向左矢量
            Vector3 leftDir = new Vector3(contactPoint.normal.y, -contactPoint.normal.x, 0);
            // 向右矢量
            Vector3 rightDir = new Vector3(-contactPoint.normal.y, contactPoint.normal.x, 0);
            transform.position += leftDir.normalized * Mathf.Sqrt(attr.mass) * Settings.dragDistanceC;
            newObject.transform.position += rightDir.normalized * Mathf.Sqrt(attr.mass) * Settings.dragDistanceC;
            float originalSpeed = attr.speed.magnitude;
            attr.speed += leftDir.normalized * attr.speed.magnitude;
            attr.speed = attr.speed.normalized * originalSpeed;
            newObject.GetComponent<BasicAttr>().speed += rightDir.normalized * attr.speed.magnitude;
            newObject.GetComponent<BasicAttr>().speed = newObject.GetComponent<BasicAttr>().speed.normalized * originalSpeed;
            GetComponent<Split>().dragging = false;
                        //分裂声音
            AudioManager.instance.ruptureAudio.Play();

        }
    }

}

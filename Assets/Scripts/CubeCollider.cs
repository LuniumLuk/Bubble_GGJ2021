using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollider : MonoBehaviour
{
    private BasicAttr attr = null;

    private void Awake()
    {
        attr = GetComponent<BasicAttr>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.LogWarning("Collision");
        ContactPoint2D contactPoint = other.contacts[0];
        attr.spd = Vector2.Reflect(attr.spd, contactPoint.normal);
        if (attr.mass > 16 && other.gameObject.tag == "spike")
        {
            GetComponent<Split>().dragging = true;
            attr.mass *= 0.5f;
            transform.localScale = Vector3.one * Mathf.Sqrt(attr.mass);
            GameObject newObject = Instantiate(gameObject);
            Debug.LogWarning(contactPoint.normal.ToString());
            Vector3 leftDir = new Vector3(contactPoint.normal.y, -contactPoint.normal.x, 0);
            Vector3 rightDir = new Vector3(-contactPoint.normal.y, contactPoint.normal.x, 0);
            transform.position += leftDir.normalized * Mathf.Sqrt(attr.mass) * 0.1f;
            newObject.transform.position += rightDir.normalized * Mathf.Sqrt(attr.mass) * 0.1f;
            float originalSpeed = attr.spd.magnitude;
            attr.spd += leftDir.normalized * attr.spd.magnitude;
            attr.spd = attr.spd.normalized * originalSpeed;
            newObject.GetComponent<BasicAttr>().spd += rightDir.normalized * attr.spd.magnitude;
            newObject.GetComponent<BasicAttr>().spd = newObject.GetComponent<BasicAttr>().spd.normalized * originalSpeed;
            GetComponent<Split>().dragging = false;
        }
    }

}

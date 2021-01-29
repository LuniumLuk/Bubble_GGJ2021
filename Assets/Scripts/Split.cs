using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public bool dragging = false;
    BasicAttr attr = null;
    GameObject newObject = null;
    public float splitForce = 1.0f;
    private GameObject tempObject = null;
    private void Awake()
    {
        attr = GetComponent<BasicAttr>();
    }

    private void OnMouseOver()
    {
        if (attr.id == 0 && !dragging && Input.GetMouseButton(1))
        {
            Debug.LogWarning("right button down");
            dragging = true;
            Transform targetTransform = transform;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            float distance = Mathf.Sqrt(attr.mass - 1) + 1;
            attr.mass -= 1;
            transform.localScale = Vector3.one * Mathf.Sqrt(attr.mass);
            newObject = Instantiate(gameObject);
            newObject.transform.position += (mousePos - transform.position).normalized * distance * 0.1f;
            newObject.transform.localScale = Vector3.one;
            newObject.GetComponent<BasicAttr>().mass = 1;
            newObject.GetComponent<BasicAttr>().spd = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (tempObject)
        {
            Debug.LogWarning("trigger");
            float otherMass = tempObject.GetComponent<BasicAttr>().mass;
            Vector3 otherSpd = tempObject.GetComponent<BasicAttr>().spd;
            transform.position = (transform.position * attr.mass + tempObject.transform.position * otherMass) / (attr.mass + otherMass);
            Destroy(tempObject);
            attr.spd = (attr.spd * attr.mass + otherSpd * otherMass) / (attr.mass + otherMass);
            attr.mass += otherMass;
            transform.localScale = Vector3.one * Mathf.Sqrt(attr.mass);
            tempObject = null;
        }

    }

    private void Update()
    {
        if (attr.id == 0 && newObject && dragging)
        {
            if (Input.GetMouseButton(1))
            {
                Transform targetTransform = transform;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                if (attr.mass - newObject.GetComponent<BasicAttr>().mass > 10f * Time.deltaTime)
                {
                    attr.mass -= 5f * Time.deltaTime;
                    newObject.GetComponent<BasicAttr>().mass += 5f * Time.deltaTime;
                    float targetMass = newObject.GetComponent<BasicAttr>().mass;
                    transform.localScale = Vector3.one * Mathf.Sqrt(attr.mass);
                    newObject.transform.localScale = Vector3.one * Mathf.Sqrt(targetMass);
                }
                float distance = Mathf.Sqrt(attr.mass) + Mathf.Sqrt(newObject.GetComponent<BasicAttr>().mass);
                newObject.transform.position = transform.position + (mousePos - transform.position).normalized * distance * 0.1f;
            }
            else
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                attr.spd += (transform.position - mousePos).normalized * splitForce / attr.mass;
                newObject.GetComponent<BasicAttr>().spd += (mousePos - transform.position).normalized * splitForce / newObject.GetComponent<BasicAttr>().mass;
                newObject = null;
                dragging = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!tempObject && attr.id == 0 && !dragging && other.gameObject.tag == "bubble")
        {
            tempObject = other.gameObject;
        }
    }

}

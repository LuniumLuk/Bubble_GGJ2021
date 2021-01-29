using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainC : MonoBehaviour
{

    static int sequence = 0;
    public int id = 0;
    public float scale = 10;
    public Vector3 speed;
    public Vector3 accelerate;
    public float Cdrag = 0.001f;
    public bool dragging = false;
    public bool newly = false;
    public bool applyDrag = false;
    private GameObject temp;
    // Start is called before the first frame update
    private void Start()
    {
        transform.localScale = Vector3.one * Mathf.Sqrt(scale);
        id = sequence++;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        speed += accelerate;
        if (speed.magnitude < 0.01f)
            speed = Vector3.zero;
        transform.position += speed * Time.fixedDeltaTime;
        Vector3 dragForce = Cdrag * scale * speed.magnitude * speed.magnitude * -speed.normalized;
        if(applyDrag)
            accelerate = dragForce;
        if (dragging && temp)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            temp.transform.position = mousePos;
        }
        CheckBoundary();
    }

    private void OnMouseDown()
    {
        if (scale > 1)
        {
            dragging = true;
            temp = Instantiate(gameObject);
            temp.transform.localScale = Vector3.one;
            temp.GetComponent<MainC>().scale = 1;
            temp.GetComponent<MainC>().newly = true;
            scale -= 1;
            transform.localScale = Vector3.one * Mathf.Sqrt(scale);
        }
    }

    private void OnMouseUp()
    {
        if (dragging)
        {
            dragging = false;
            temp.GetComponent<MainC>().dragging = false;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            temp.transform.position = transform.position;
            Vector3 throwSpeed = mousePos - transform.position;
            temp.GetComponent<MainC>().speed = speed + throwSpeed * scale / (scale + 1);
            speed -= throwSpeed / (scale + 1);
            temp = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!newly && !other.GetComponentInParent<MainC>().newly)
        {
            if (id > other.GetComponentInParent<MainC>().id)
            {
                Vector3 otherSpeed = other.GetComponentInParent<MainC>().speed;
                float otherScale = other.GetComponentInParent<MainC>().scale;
                speed = (speed * scale + otherSpeed * otherScale) / (scale + otherScale);
                transform.position = (transform.position * scale + other.transform.position * otherScale) / (scale + otherScale);
                scale += otherScale;
                transform.localScale = Vector3.one * Mathf.Sqrt(scale);
                Destroy(other.gameObject);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (newly && !other.GetComponentInParent<MainC>().dragging)
            newly = false;
    }

    void CheckBoundary()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 norm = Vector3.zero;
        if (pos.x > Screen.width)
        {
            norm = Vector3.left;
        }
        if (pos.x < 0)
        {
            norm = Vector3.right;
        }
        if (pos.y < 0)
        {
            norm = Vector3.up;
        }
        if (pos.y > Screen.height)
        {
            norm = Vector3.down;
        }
        if (norm != Vector3.zero)
        {
            speed = Vector2.Reflect(speed, norm);
            norm = Vector3.zero;
        }
    }

}

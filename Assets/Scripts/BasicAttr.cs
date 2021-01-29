using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttr : MonoBehaviour
{
    static int sequence = 0;
    public int id = 0;
    public float mass = 10;
    public Vector3 spd;
    public Vector3 acc;
    public float Ressistance = 0.001f;

    private void Start()
    {
        if(id >= 0)
            id = sequence++;
        transform.localScale = Vector3.one * Mathf.Sqrt(mass);
    }
    private void FixedUpdate()
    {
        spd += acc * Time.fixedDeltaTime;
        transform.position += spd * Time.fixedDeltaTime;
        CheckBoundary();
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
            spd = Vector2.Reflect(spd, norm);
            norm = Vector3.zero;
        }
    }

}

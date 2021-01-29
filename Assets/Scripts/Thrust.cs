using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour
{
    BasicAttr basicAttr = null;

    public float thrustForce = 0.001f;

    private void Awake() {
        basicAttr = GetComponent<BasicAttr>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            basicAttr.spd += (transform.position - mousePos).normalized * thrustForce * Time.deltaTime;
        }
        
    }
}

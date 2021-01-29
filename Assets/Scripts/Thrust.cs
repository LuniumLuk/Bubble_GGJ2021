using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thrust : MonoBehaviour
{
    BasicAttr attr = null;
    public ParticleSystem emission = null;

    public float thrustForce = 0.001f;
    public float gas = 100f;

    public Text gasText = null;

    private void Awake()
    {
        attr = GetComponent<BasicAttr>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && attr.id == 0 && gas > 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            attr.spd += (transform.position - mousePos).normalized * thrustForce * Time.deltaTime;
            emission.transform.localEulerAngles = new Vector3(getAngle(transform.position - mousePos), 90f, 90f);
            gas -= Time.deltaTime * 10;
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
        if(attr.id == 0 && other.gameObject.tag == "gastank") {
            Destroy(other.gameObject);
            gas += 20;
            gasText.text = "Gas: " + gas.ToString();
        }
    }
}

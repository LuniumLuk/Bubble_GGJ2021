using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo : MonoBehaviour
{
    public GameObject hint = null;


    private void OnTriggerEnter2D(Collider2D other)
    {
        hint.SetActive(true);
        Time.timeScale = 0.1f;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttr : MonoBehaviour
{
    // 自增序号
    static int sequence = 0;

    // bubble id，其中id为0的是主角色
    public int id = 0;

    // bubble质量，用于计算加速度阻力、动量守恒和缩放半径
    public float mass = 10;
    public float targetmass = 0;
    // 速度
    public Vector3 speed;

    // 加速度
    public Vector3 acceleration;

    private void Start()
    {
        // 自增序号
        //if (id >= 0)
        //    id = sequence++;

        // 将scale设置为质量的平方根
        targetmass = mass;
        transform.localScale = Vector3.one * Mathf.Sqrt(mass) * Settings.scaleC;
    }
    private void FixedUpdate()
    {
        Vector3 RForce = Settings.CRessistance * Mathf.Sqrt(mass) * speed.magnitude * speed.magnitude * -speed.normalized;
        if(Settings.applyRessistance)
            acceleration = RForce;
        speed += acceleration * Time.fixedDeltaTime;
        transform.position += speed * Time.fixedDeltaTime;
        Vector3 currentPos = transform.position;
        currentPos.z = -10;
        // 调整摄像机位置
        if (id == 0)
        {
            Camera.main.transform.position = currentPos;
        //    Camera.main.orthographicSize = Mathf.Sqrt(mass) / 2;
        }
    }

}

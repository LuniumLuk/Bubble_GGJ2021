using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    // 阻力系数
    public static float CRessistance = 0.01f;

    // 是否使用阻力
    public static bool applyRessistance = true;

    // 控制比例系数
    public static float scaleC = 0.25f;

    // 可分裂的最小质量
    public static float minimumMass = 16f;

    // 分裂时提供的分离力（固定值）
    public static float splitForce = 0.1f;

    // 右键拖动时预览位置系数，即保护距离
    // 在该距离系数加持下，两个物体不会再拖动时或者分裂时因为碰撞体太近而立刻融合
    public static float dragDistanceC = 0.15f;

    // 喷力大小
    public static float thrustForce = 1f;

    // 燃油初始数量
    public static float gas = 100f;

    // 耗油速度
    public static float gasConsume = 15f;

    // 燃料箱提供燃油数量
    public static float gasTank = 20f;

    // 开启分裂能力
    public static bool splitAbility = false;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController character; // 声明角色控制器
    void Awake()
    {
        character = GetComponent<CharacterController>(); // 获取角色控制器组件
    }

    private void Start()
    {
        rotationx = transform.eulerAngles.x; // 初始化X轴旋转角度
        rotationy = transform.eulerAngles.y; // 初始化Y轴旋转角度

        Time.timeScale = 1; // 设置时间流速为正常
        score = 0; // 初始化分数
    }
    public bool isMove, isFlyMove; // 是否移动和是否飞行移动
    public float speed = 5; // 角色移动速度
    void Update()
    {
        if (isMove && Camera.main) Move(); // 如果允许移动且主相机存在，调用移动方法

        TXT(); // 更新文本显示

        // 如果分数达到5，停止游戏并显示成功界面
        if (score == 5)
        {
            Time.timeScale = 0; // 暂停游戏时间
            success.SetActive(true); // 显示成功界面
            isMove = false; // 停止移动
        }

        // 控制音频音量
        audio1.volume = slider1.value;
        audio2.volume = slider2.value;
    }

    public Slider slider2, slider1; // 控制音频音量的滑动条
    public AudioSource audio1, audio2; // 两个音频源
    float rotationx, rotationy, horizontal, vertical; // 旋转和移动相关的变量

    void Move() // 移动方法
    {
        Quaternion headYaw = Quaternion.identity; // 初始化头部的旋转
        if (!isFlyMove) headYaw = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0); // 非飞行模式下，旋转角度仅考虑Y轴
        else headYaw = Quaternion.Euler(Camera.main.transform.eulerAngles); // 飞行模式下，旋转角度包括所有轴

        Vector3 dirction = Vector3.zero; // 初始化移动方向
        dirction = headYaw * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // 获取水平和垂直轴的输入，计算方向

        // 如果角色控制器启用，则使用SimpleMove进行移动，否则直接修改位置
        if (character.enabled) character.SimpleMove(dirction * speed);
        else transform.position += dirction * Time.deltaTime * speed;

        // 鼠标右键按下时，控制视角旋转
        if (Input.GetMouseButton(1))
        {
            rotationx -= Input.GetAxis("Mouse Y") * 5; // 控制X轴旋转
            rotationy += Input.GetAxis("Mouse X") * 5; // 控制Y轴旋转
            rotationx = Mathf.Clamp(rotationx, -60, 60); // 限制X轴旋转范围
            transform.eulerAngles = new Vector3(rotationx, rotationy, 0); // 更新角色旋转角度
        }
    }

    public static int score; // 分数变量
    public TextMeshProUGUI txtScore, txtEnemy; // 显示分数和敌人距离的文本组件
    void TXT() // 更新显示文本
    {
        if (GameObject.FindGameObjectsWithTag("coin").Length > 0) // 如果场景中有金币
        {
            GameObject[] coins = GameObject.FindGameObjectsWithTag("coin"); // 获取所有金币对象
            float dis = float.MaxValue; // 初始化最小距离为最大值
            foreach (GameObject co in coins) // 遍历每个金币
            {
                if (Vector3.Distance(transform.position, co.transform.position) < dis) // 计算角色与金币的距离
                {
                    dis = Vector3.Distance(transform.position, co.transform.position); // 更新最小距离
                }
            }
            txtScore.text = "最近的金币距离: " + dis.ToString("0") + "m" + "   " + score + "/5"; // 显示距离和分数
        }
        else
        {
            txtScore.text = "5/5"; // 如果没有金币，显示5/5
        }

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy"); // 获取所有敌人对象
        float dis1 = float.MaxValue; // 初始化最小距离为最大值
        foreach (GameObject e in enemys) // 遍历每个敌人
        {
            if (Vector3.Distance(transform.position, e.transform.position) < dis1) // 计算角色与敌人的距离
            {
                dis1 = Vector3.Distance(transform.position, e.transform.position); // 更新最小距离
            }
        }
        txtEnemy.text = "敌人距离: " + dis1.ToString("0") + "m"; // 显示敌人距离
    }

    public GameObject fail, success; // 失败和成功界面的游戏对象

    private void OnTriggerEnter(Collider other) // 触发器碰撞事件
    {
        if (other.tag == "dao") // 如果碰到标记为敌人"dao"的对象
        {
            Time.timeScale = 0; // 暂停游戏时间
            fail.SetActive(true); // 显示失败界面
            isMove = false; // 停止移动
        }
    }
}

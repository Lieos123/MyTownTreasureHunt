using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mycoin : MonoBehaviour
{
    public float speed = 1;  // 硬币旋转的速度

    void Update()
    {
        // 每帧旋转硬币，使其沿着 Z 轴旋转，旋转速度为 speed
        transform.Rotate(Vector3.forward, speed);
    }

    public AudioSource audioSource;  // 用于播放音效的音频源

    private void OnTriggerEnter(Collider other)
    {
        // 如果触发器碰撞到的物体是玩家
        if (other.name == "Player")
        {
            // 玩家得分增加
            Player.score++;
            // 播放音效
            audioSource.Play();
            // 销毁当前硬币对象
            Destroy(gameObject);
        }
    }
}

using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public Transform player;  // 玩家对象的引用

    void Update()
    {
        // 计算敌人和玩家之间的距离
        float dis = Vector3.Distance(transform.position, player.position);
        // 当敌人距离玩家小于2时，设置动画为攻击状态
        GetComponent<Animator>().SetBool("isAttack", dis < 2);
    }

    private Vector3 targetPosition;  // 存储目标位置

    public Seeker seeker;  // 寻路组件的引用

    // 存储路径
    public Path path;
    // 角色移动速度
    public float speed = 100.0f;
    // 角色转向速度
    public float turnSpeed = 5f;
    // 判断玩家与航点的距离
    public float nextWaypointDistance = 3;
    // 当前航点的索引
    private int currentWaypoint = 0;

    void FixedUpdate()
    {
        // 设置目标位置为玩家的位置
        targetPosition = player.position;

        // 开始寻路，获取从敌人到玩家的路径
        seeker.StartPath(transform.position, targetPosition);
        // 寻路结束后的回调函数
        seeker.pathCallback += OnPathComplete;

        // 如果路径为空，退出
        if (path == null)
        {
            return;
        }

        // 如果当前的航点索引大于或等于路径点数，说明路径已经走完
        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("路径搜索结束");
            return;
        }

        // 计算当前航点和下一航点之间的方向
        Vector3 dir = (path.vectorPath[currentWaypoint + 1] - transform.position);
        // 根据移动速度计算前进的距离
        dir *= speed * Time.fixedDeltaTime;

        // 移动敌人
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed);
        // 转向目标位置
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

        // 如果敌人与当前航点的距离小于给定的距离，转向下一个航点
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    /// <summary>
    /// 当寻路结束后调用这个函数
    /// </summary>
    /// <param name="p">路径对象</param>
    private void OnPathComplete(Path p)
    {
        Debug.Log("发现这个路线" + p.error);
        // 如果路径没有错误，更新路径和当前航点索引
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}

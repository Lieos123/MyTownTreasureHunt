using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController character; // ������ɫ������
    void Awake()
    {
        character = GetComponent<CharacterController>(); // ��ȡ��ɫ���������
    }

    private void Start()
    {
        rotationx = transform.eulerAngles.x; // ��ʼ��X����ת�Ƕ�
        rotationy = transform.eulerAngles.y; // ��ʼ��Y����ת�Ƕ�

        Time.timeScale = 1; // ����ʱ������Ϊ����
        score = 0; // ��ʼ������
    }
    public bool isMove, isFlyMove; // �Ƿ��ƶ����Ƿ�����ƶ�
    public float speed = 5; // ��ɫ�ƶ��ٶ�
    void Update()
    {
        if (isMove && Camera.main) Move(); // ��������ƶ�����������ڣ������ƶ�����

        TXT(); // �����ı���ʾ

        // ��������ﵽ5��ֹͣ��Ϸ����ʾ�ɹ�����
        if (score == 5)
        {
            Time.timeScale = 0; // ��ͣ��Ϸʱ��
            success.SetActive(true); // ��ʾ�ɹ�����
            isMove = false; // ֹͣ�ƶ�
        }

        // ������Ƶ����
        audio1.volume = slider1.value;
        audio2.volume = slider2.value;
    }

    public Slider slider2, slider1; // ������Ƶ�����Ļ�����
    public AudioSource audio1, audio2; // ������ƵԴ
    float rotationx, rotationy, horizontal, vertical; // ��ת���ƶ���صı���

    void Move() // �ƶ�����
    {
        Quaternion headYaw = Quaternion.identity; // ��ʼ��ͷ������ת
        if (!isFlyMove) headYaw = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0); // �Ƿ���ģʽ�£���ת�ǶȽ�����Y��
        else headYaw = Quaternion.Euler(Camera.main.transform.eulerAngles); // ����ģʽ�£���ת�ǶȰ���������

        Vector3 dirction = Vector3.zero; // ��ʼ���ƶ�����
        dirction = headYaw * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // ��ȡˮƽ�ʹ�ֱ������룬���㷽��

        // �����ɫ���������ã���ʹ��SimpleMove�����ƶ�������ֱ���޸�λ��
        if (character.enabled) character.SimpleMove(dirction * speed);
        else transform.position += dirction * Time.deltaTime * speed;

        // ����Ҽ�����ʱ�������ӽ���ת
        if (Input.GetMouseButton(1))
        {
            rotationx -= Input.GetAxis("Mouse Y") * 5; // ����X����ת
            rotationy += Input.GetAxis("Mouse X") * 5; // ����Y����ת
            rotationx = Mathf.Clamp(rotationx, -60, 60); // ����X����ת��Χ
            transform.eulerAngles = new Vector3(rotationx, rotationy, 0); // ���½�ɫ��ת�Ƕ�
        }
    }

    public static int score; // ��������
    public TextMeshProUGUI txtScore, txtEnemy; // ��ʾ�����͵��˾�����ı����
    void TXT() // ������ʾ�ı�
    {
        if (GameObject.FindGameObjectsWithTag("coin").Length > 0) // ����������н��
        {
            GameObject[] coins = GameObject.FindGameObjectsWithTag("coin"); // ��ȡ���н�Ҷ���
            float dis = float.MaxValue; // ��ʼ����С����Ϊ���ֵ
            foreach (GameObject co in coins) // ����ÿ�����
            {
                if (Vector3.Distance(transform.position, co.transform.position) < dis) // �����ɫ���ҵľ���
                {
                    dis = Vector3.Distance(transform.position, co.transform.position); // ������С����
                }
            }
            txtScore.text = "����Ľ�Ҿ���: " + dis.ToString("0") + "m" + "   " + score + "/5"; // ��ʾ����ͷ���
        }
        else
        {
            txtScore.text = "5/5"; // ���û�н�ң���ʾ5/5
        }

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy"); // ��ȡ���е��˶���
        float dis1 = float.MaxValue; // ��ʼ����С����Ϊ���ֵ
        foreach (GameObject e in enemys) // ����ÿ������
        {
            if (Vector3.Distance(transform.position, e.transform.position) < dis1) // �����ɫ����˵ľ���
            {
                dis1 = Vector3.Distance(transform.position, e.transform.position); // ������С����
            }
        }
        txtEnemy.text = "���˾���: " + dis1.ToString("0") + "m"; // ��ʾ���˾���
    }

    public GameObject fail, success; // ʧ�ܺͳɹ��������Ϸ����

    private void OnTriggerEnter(Collider other) // ��������ײ�¼�
    {
        if (other.tag == "dao") // ����������Ϊ����"dao"�Ķ���
        {
            Time.timeScale = 0; // ��ͣ��Ϸʱ��
            fail.SetActive(true); // ��ʾʧ�ܽ���
            isMove = false; // ֹͣ�ƶ�
        }
    }
}

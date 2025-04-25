using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P : MonoBehaviour
{


    /// <summary>
    /// �����±�
    /// </summary>
    public static void LoadScene(int index) { SceneManager.LoadScene(index); }

    /// <summary>
    /// ��������
    /// </summary>
    public static void LoadScene(string name) { SceneManager.LoadScene(name); }

    public  void NextScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    public  void ReloadScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    public  void LastScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); }

    /// <summary>
    /// ��ǰ��������תindex��λ
    /// </summary>
    public void JumpScene(int index) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index); }

    //�˳�
    public  void Quit() { Application.Quit(); }
}

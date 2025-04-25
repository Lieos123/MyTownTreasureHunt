using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P : MonoBehaviour
{


    /// <summary>
    /// 场景下标
    /// </summary>
    public static void LoadScene(int index) { SceneManager.LoadScene(index); }

    /// <summary>
    /// 场景名字
    /// </summary>
    public static void LoadScene(string name) { SceneManager.LoadScene(name); }

    public  void NextScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    public  void ReloadScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    public  void LastScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); }

    /// <summary>
    /// 当前场景上跳转index单位
    /// </summary>
    public void JumpScene(int index) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index); }

    //退出
    public  void Quit() { Application.Quit(); }
}

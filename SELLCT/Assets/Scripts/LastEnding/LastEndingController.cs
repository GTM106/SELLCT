using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LastEndingController : MonoBehaviour
{
    public void End()
    {
        //ゲームを終了する
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

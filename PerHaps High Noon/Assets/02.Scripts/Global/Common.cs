using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Common
{
    /// <summary>
    /// 입력상태(=입력액션)
    /// </summary>
    public enum INPUT { INPUT_BEGIN , INPUT_MOVE, INPUT_END }

    /// <summary>
    /// ITEM 유형
    /// </summary>
    public enum ITEM_TYPE { NONE = 0, LIFE = 1, FEVER = 2 }

    /// <summary>
    /// 현재 위치에서 aim이 생성되는 위치를 얻는다.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Vector3 GetAimPosition(Vector3 position)
    {
        return position + Vector3.up * 2;
    }

    public static bool isRunning = true;
}


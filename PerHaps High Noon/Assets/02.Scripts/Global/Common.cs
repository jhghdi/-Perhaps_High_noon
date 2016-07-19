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
    public enum INPUT { INPUT_BEGIN , INPUT_MOVE, INPUT_END, INPUT_NULL }

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
        return position + Vector3.up *2.5f;
    }

    /// <summary>
    /// 적의 움직임 유형
    /// </summary>
    public enum ENEMY_MOVE_TYPE { NORMAL = 0, PARABOLA, GUIDED };

    /// <summary>
    /// 적의 유형
    /// </summary>
    public enum ENEMY_TYPE { NORMAL = 0, TANKER, DRONE, TURRET, STAGE1_BOSS, STAGE2_BOSS, STAGE3_BOSS};

    public static bool isRunning = true;
}


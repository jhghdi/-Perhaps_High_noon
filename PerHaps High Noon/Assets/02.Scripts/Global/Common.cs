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
}


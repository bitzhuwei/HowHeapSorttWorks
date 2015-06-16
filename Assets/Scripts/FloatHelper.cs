using UnityEngine;
using System.Collections;

public static class FloatHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToShortString(this float value)
    {
        string result = string.Empty;

        if(-10.0f < value && value < 10.0f)
        {
            result = string.Format("{0:0.00}", value);
        }
        else
        {
            result = string.Format("{0:0.0}", value);
        }

        return result;
    }

}

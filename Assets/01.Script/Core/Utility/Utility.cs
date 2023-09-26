using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Utility
{
    #region Parse
    public static T ParseStringToEnum<T>(string s)
    {
        T returnValue;

        try
        {
            returnValue = (T)Enum.Parse(typeof(T), s);
        }
        catch
        {
            Error($"Enum({nameof(T)}) ���� {s}�� ����");
            return default(T);
        }

        return returnValue;
    }
    public static int ParseEnumToInt(Enum e)
    {
        return int.Parse(e.ToString("d"));
    }

    public static int ParseStringToInt(string s)
    {
        int returnValue;

        try
        {
            returnValue = Convert.ToInt32(s);
        }
        catch
        {
            Error($"{s} �� Int32 ������ �ٲܼ� ����");
            return -1;
        }

        return returnValue;
    }
    #endregion

    #region Invoke
    public static void Invoke(this MonoBehaviour mb, Action ac, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(ac, delay));
    }
    private static IEnumerator InvokeRoutine(Action ac, float delay)
    {
        yield return new WaitForSeconds(delay);
        ac();
    }
    #endregion

    #region Debug
    public static void DrawRay(Vector3 originPos, Vector3 dir, float distance = 10f, float duration = 1f, Color color = default(Color))
    {
        Debug.DrawRay(originPos, dir * distance, color, duration);
    }
    #endregion

    #region Vector
    public static Vector3 GetVecByAngle(float degrees, bool isLocalAngle = false, Transform refTrans = null)
    {
        if (isLocalAngle)
        {
            degrees += refTrans.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(degrees * Mathf.Deg2Rad), 0, Mathf.Cos(degrees * Mathf.Deg2Rad));
    }
    #endregion

    #region Search
    public static T SearchByName<T>(string s) where T : Object
    {
        T returnData = GameObject.Find(s).GetComponent<T>();
        if (returnData == null) { Error($"{s}�̸��� ������Ʈ�� ���ų� {nameof(T)} �� �����ϴ�"); }

        return returnData;
    }
    public static T SearchByClass<T>() where T : Object
    {
        T returnData = Object.FindObjectOfType<T>();
        if (returnData == null) { Error($"���� {nameof(T)} �� �����ϴ�"); }

        return returnData;
    }
    public static T SearchByTag<T>(string s) where T : Object
    {
        T returnData = GameObject.FindGameObjectWithTag(s).GetComponent<T>();
        if (returnData == null) { Error($"���� {nameof(T)} �±׸� ���� ������Ʈ�� �����ϴ�"); }

        return returnData;
    }
    //-----����Ž��-----//
    public static T[] SearchArrayByClass<T>() where T : Object
    {
        T[] returnData = Object.FindObjectsOfType<T>();
        if (returnData.Length == 0) { Error($"���� {nameof(T)} �� �����ϴ�"); }

        return returnData;
    }
    public static T[] SearchArrayByTag<T>(string s) where T : Object
    {
        List<T> returnData = new();
        foreach (var obj in GameObject.FindGameObjectsWithTag(s))
        {
            returnData.Add(obj.GetComponent<T>());
        }
        if (returnData.Count == 0) { Error($"���� {nameof(T)} �±׸� ���� ������Ʈ�� �����ϴ�"); }

        return returnData.ToArray();
    }
    #endregion

    #region Key
    public static bool ComboKeyCheck(KeyCode pressingKey, KeyCode eventKey)
    {
        return Input.GetKey(pressingKey) && Input.GetKeyDown(eventKey);
    }
    #endregion

    private static void Error(string errorString)
    {
        Debug.LogError($"Utility : {errorString}");
    }
}

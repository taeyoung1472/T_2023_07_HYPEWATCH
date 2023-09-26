using UnityEngine;
using Object = UnityEngine.Object;

public static class Define
{
    private static Character _character = null;
    public static Character Character { get { return SearchByClass<Character>(ref _character); } }
    private static Camera mainCam;
    public static Transform PlayerTrm { get { return Character.transform; } }
    public static Camera MainCam { get { return SearchByClass<Camera>(ref mainCam); } }

    private static T SearchByName<T>(ref T t, string s) where T : Object
    {
        if (t == null)
        {
            t = Utility.SearchByName<T>(s);
        }

        return t;
    }
    private static T SearchByClass<T>(ref T t) where T : Object
    {
        if (t == null)
        {
            t = Utility.SearchByClass<T>();
        }

        return t;
    }

    private static Camera _cam = null;
    public static Camera Cam
    {
        get
        {
            if (_cam == null)
                _cam = Camera.main;
            return _cam;
        }
    }
}
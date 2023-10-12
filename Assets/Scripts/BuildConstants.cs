using UnityEngine;

public class BuildConstants
{
    public static bool isExpo = false;
    public static bool isDebug = false;
    public static bool isWebGL = Application.platform == RuntimePlatform.WebGLPlayer;
    public static bool isMobile = Application.isMobilePlatform;
}
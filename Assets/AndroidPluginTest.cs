using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidPluginTest : MonoBehaviour
{
    private AndroidJavaObject _plugin;
    private string _strText;
    // Start is called before the first frame update
    private void Awake()
    {
        AndroidJavaClass Ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        _plugin = Ajc.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public void ReceiveString(string str)
    {
        _strText = str;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PM_Debug_Android : MonoBehaviour
{
    public Text testText;
    private AndroidJavaObject UnityActivity;
    private AndroidJavaObject UnityInstance;
    // Start is called before the first frame update
    void Start()
    {
        var ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        UnityActivity = ajc.GetStatic<AndroidJavaObject>("currentActivity");

        var plugin = new AndroidJavaClass("com.example.unityplugin.PluginClass");
        UnityInstance = plugin.CallStatic<AndroidJavaObject>("instance");

        UnityInstance.Call("setContext", UnityActivity);
        UnityInstance.Call("setReceiver");
        testText.text = plugin.CallStatic<string>("UnityCall", "testcall");
    }

    void OnApplicationPause(bool pause)
    {
       if(pause)
        {
            Debug.Log("시발");
            UnityInstance.Call("beginDownload", "https://user-images.githubusercontent.com/59655997/73188586-71352780-4166-11ea-929e-8587ef6bb38c.png");
        }
    }

    public void beginDownload()
    {
        UnityInstance.Call("beginDownload", "https://user-images.githubusercontent.com/59655997/73188586-71352780-4166-11ea-929e-8587ef6bb38c.png");
    }

    public void ShowToast()
    {
        UnityInstance.Call("ShowToast", "hi", 0);
    }

}

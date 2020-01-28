using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class PM_Debug_Android : MonoBehaviour
{
    private bool download = false;
    public Text testText;
    public Image img;
    private AndroidJavaObject UnityActivity;
    private AndroidJavaObject UnityInstance;
    // Start is called before the first frame update
    void Start()
    {
        var ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        UnityActivity = ajc.GetStatic<AndroidJavaObject>("currentActivity");

        var plugin = new AndroidJavaClass("com.example.pluginclass.PluginClass");
        UnityInstance = plugin.CallStatic<AndroidJavaObject>("instance");

        UnityInstance.Call("setContext", UnityActivity);
        UnityInstance.Call("setReceiver");
        testText.text = plugin.CallStatic<string>("UnityCall", "testcall");
    }

    void OnApplicationPause(bool pause)
    {
       if(pause && !download)
        {
            UnityInstance.Call("beginDownload", "https://user-images.githubusercontent.com/59655997/73188586-71352780-4166-11ea-929e-8587ef6bb38c.png");
        }
       else if(!pause)
       {
            download = UnityInstance.Call<bool>("getDownload");
            if(download)
            {
                byte[] byteArray = File.ReadAllBytes(Application.persistentDataPath + "/" + "Dummy");
                Texture2D texture = new Texture2D(8, 8);
                texture.LoadImage(byteArray);
                Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
                img.sprite = s;
            }
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

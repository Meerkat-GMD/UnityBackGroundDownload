package com.example.pluginclass;

import android.app.Activity;
import android.app.DownloadManager;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.Uri;
import android.os.Build;
import android.util.Log;
import android.widget.Toast;

import androidx.annotation.RequiresApi;
import androidx.core.app.NotificationCompat;

import java.io.File;

import static android.content.Context.DOWNLOAD_SERVICE;

public class PluginClass {
    private Context context;
    private boolean Download = false;
    private static PluginClass m_instance;
    public static final String NOTIFICATION_CHANNEL_ID = "10001";
    private long downloadID;

    private BroadcastReceiver onDownloadComplete = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context1, Intent intent) {
            //Fetching the download id received with the broadcast
            long id = intent.getLongExtra(DownloadManager.EXTRA_DOWNLOAD_ID, -1);
            //Checking if the received broadcast is for our enqueued download by matching download id
            if (downloadID == id) {
                Download = true;
                Toast.makeText(context, "Download Completed", Toast.LENGTH_SHORT).show();
            }
        }
    };

    private boolean getDownload()
    {
        return Download;
    }


    public static PluginClass instance()
    {
        if(m_instance == null)
        {
            m_instance = new PluginClass();
        }
        return m_instance;
    }

    @RequiresApi(api = Build.VERSION_CODES.N)
    private void beginDownload(String link) {
        File file = new File(context.getExternalFilesDir(null), "Dummy");
        /*
        Create a DownloadManager.Request with all the information necessary to start the download
         */


        DownloadManager.Request request = new DownloadManager.Request(Uri.parse(link))
                .setTitle("Dummy File")// Title of the Download Notification
                .setDescription("Downloading")// Description of the Download Notification
                .setNotificationVisibility(DownloadManager.Request.VISIBILITY_VISIBLE_NOTIFY_ONLY_COMPLETION)// Visibility of the download Notification
                .setDestinationUri(Uri.fromFile(file))// Uri of the destination file
                .setRequiresCharging(false)// Set if charging is required to begin the download
                .setAllowedOverMetered(true)// Set if download is allowed on Mobile network
                .setAllowedOverRoaming(true);// Set if download is allowed on roaming network
        DownloadManager downloadManager = (DownloadManager) context.getSystemService(DOWNLOAD_SERVICE);

        if(downloadID != -1L)
            downloadManager.remove(downloadID);

        downloadID = downloadManager.enqueue(request);// enqueue puts the download request in the queue.

    }

    private void cancelDownload()
    {

    }


    public static String UnityCall (String _param)
    {
        Log.e("UnityCall",_param);
        return "TESK OK";
    }

    private void setContext(Context ct)
    {
        context = ct;
    }

    private void setReceiver()
    {
        context.registerReceiver(onDownloadComplete,new IntentFilter(DownloadManager.ACTION_DOWNLOAD_COMPLETE));
    }

    private void ShowToast(String msg, int i)
    {
        if(i==0)
        {
            Toast.makeText(context,msg,Toast.LENGTH_SHORT).show();
        }
        else
        {
            Toast.makeText(context, msg, Toast.LENGTH_LONG).show();
        }
    }
}

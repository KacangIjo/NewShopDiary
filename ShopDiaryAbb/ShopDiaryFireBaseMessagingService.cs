﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace ShopDiaryAbb
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class ShopDiaryFireBaseMessagingService:FirebaseMessagingService
    {
        //public override void OnMessageReceived(RemoteMessage message)
    //    {
    //        base.OnMessageReceived(message);
    //        SendNotification(message.GetNotification().Body);
    //    }

    //    private void SendNotification(string body)
    //    {
    //        var intent = new Intent(this, typeof(MainActivity));
    //        intent.AddFlags(ActivityFlags.ClearTop);
    //        var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
    //        var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
    //        var notificationBuilder = new NotificationCompat.Builder(this)
    //            .SetSmallIcon(Resource.Drawable.logo)
    //            .SetContentTitle("You Have Expired Items")
    //            .SetContentText(body)
    //            .SetAutoCancel(true)
    //            .SetSound(defaultSoundUri)
    //            .SetContentIntent(pendingIntent);
    //        var notificationManager = NotificationManager.FromContext(this);
    //        notificationManager.Notify(0, notificationBuilder.Build());
    //    }
    }
}
using Android.App;
using Android.Content.PM;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Views;
using Common;
using System;
using static Android.Net.Wifi.WifiManager;

namespace ThirdGame
{
    [Activity(Label = "ó"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true        
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private Game1 game;
        private WifiLock wifilock;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().PermitAll().Build();
            StrictMode.SetThreadPolicy(policy);

            var wifi = (WifiManager)GetSystemService(WifiService);
            var ConnectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

            wifilock = wifi.CreateWifiLock(WifiMode.FullHighPerf, "WifiLock");
            wifilock.Acquire();

            game = new Game1(
                CreateUdpService(wifi, ConnectivityManager)
                , RuningOnAndroid: true
            );

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                Vibrator vibrator = (Vibrator)GetSystemService(VibratorService);
                Game1.AndroidVibrate = f => vibrator.Vibrate(VibrationEffect.CreateOneShot(f, VibrationEffect.DefaultAmplitude));
            }
            else
            {
                //TODO:   
            }

            SetViewFullScreen();

            //PowerManager pm = (PowerManager)GetSystemService(PowerService);
            //this.mWakeLock = pm.NewWakeLock(WakeLockFlags.ScreenDim, "My Tag");
            //this.mWakeLock.Acquire();
            game.Run();
        }

        private NetworkService CreateUdpService(WifiManager wifi, ConnectivityManager ConnectivityManager)
        {
            var udpService = new NetworkService(
                new UdpBroadcastForAndroid(
                    UdpConfig.PORT
                    , UdpConfig.PACKAGE_SIZE
                    , wifi
                    , ConnectivityManager
                )
            );

            return udpService;
        }

        private void SetViewFullScreen()
        {
            var view = (View)game.Services.GetService(typeof(View));
         
            view.SystemUiVisibility = (StatusBarVisibility)
                (SystemUiFlags.LayoutStable
                | SystemUiFlags.LayoutHideNavigation
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.HideNavigation
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.ImmersiveSticky
                );

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;

            SetContentView(view);
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetViewFullScreen();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            SetViewFullScreen();
        }

        protected override void OnDestroy()
        {
            wifilock.Release();
            base.OnDestroy();
        }
    }
}


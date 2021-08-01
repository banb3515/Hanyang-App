﻿#region API 참조
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

using Rg.Plugins.Popup.Services;
using Android.Content.Res;
using Android.Util;
#endregion

namespace Hanyang.Droid.Activitys
{
    [Activity(Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static MainActivity instance;

        #region OnCreate
        [System.Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            initFontScale();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            AiForms.Dialogs.Dialogs.Init(this);

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            instance = this;
        }
        #endregion

        #region OnRequestPermissionsResult
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        #endregion

        #region 디바이스 Back 버튼
        public override async void OnBackPressed()
        {
            if (((App)Xamarin.Forms.Application.Current).PromptToConfirmExit)
            {
                using (var alert = new AlertDialog.Builder(this))
                {
                    alert.SetTitle("한양이");
                    alert.SetMessage("앱을 종료하시겠습니까?");
                    alert.SetPositiveButton("예", (sender, args) => { FinishAffinity(); });
                    alert.SetNegativeButton("아니요", (sender, args) => { });

                    var dialog = alert.Create();
                    dialog.Show();
                }
                return;
            }

            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                await PopupNavigation.Instance.PopAsync();
                return;
            }

            base.OnBackPressed();
        }
        #endregion

        #region 폰트 크기 초기화
        [System.Obsolete]
        private void initFontScale()
        {
            Configuration configuration = Resources.Configuration;
            configuration.FontScale = (float)1.0;
            //0.85 small, 1 standard, 1.15 big，1.3 more bigger ，1.45 supper big 
            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            metrics.ScaledDensity = configuration.FontScale * metrics.Density;
            BaseContext.Resources.UpdateConfiguration(configuration, metrics);
        }
        #endregion

        #region 인스턴스 가져오기
        public static MainActivity GetInstance()
        {
            return instance;
        }
        #endregion
    }

    #region Back 버튼 핸들러
    public interface IBackButtonHandler
    {
        bool HandleBackButton();
    }
    #endregion
}
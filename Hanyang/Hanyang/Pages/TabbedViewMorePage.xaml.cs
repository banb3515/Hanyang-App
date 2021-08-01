﻿#region API 참조
using Hanyang.SubPages;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
#endregion

namespace Hanyang.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedViewMorePage : ContentPage
    {
        #region 생성자
        public TabbedViewMorePage()
        {
            InitializeComponent();
        }
        #endregion

        #region 함수
        #region 새 페이지 열기
        private async void NewPage(Page page)
        {
            await Navigation.PushAsync(page);
        }
        #endregion
        #endregion

        #region 버튼 클릭
        #region 설정 버튼
        private void SettingButton_Clicked(object sender, EventArgs e)
        {
            NewPage(new SettingPage());
        }
        #endregion

        #region 앱 정보 버튼
        private void AppInfoButton_Clicked(object sender, EventArgs e)
        {
            NewPage(new AppInfoPage());
        }
        #endregion
        #endregion
    }
}
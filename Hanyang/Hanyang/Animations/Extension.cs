﻿#region API 참조
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
#endregion

namespace Hanyang.Animations
{
    public static class Extension
    {
        #region 색상 변경 애니메이션
        public static Task<bool> ColorTo(this VisualElement self, Color fromColor, Color toColor, Action<Color> callback, uint length = 250, Easing easing = null)
        {
            Func<double, Color> transform = (t) =>
              Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
                             fromColor.G + t * (toColor.G - fromColor.G),
                             fromColor.B + t * (toColor.B - fromColor.B),
                             fromColor.A + t * (toColor.A - fromColor.A));

            return ColorAnimation(self, "ColorTo", transform, callback, length, easing);
        }

        static Task<bool> ColorAnimation(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }
        #endregion

        #region 애니메이션 취소
        public static void CancelAnimation(this VisualElement self)
        {
            self.AbortAnimation("ColorTo");
        }
        #endregion
    }
}
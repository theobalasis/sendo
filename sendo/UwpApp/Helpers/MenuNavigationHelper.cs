using Sendo.UwpApp.Services;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Sendo.UwpApp.Helpers
{
    public static class MenuNavigationHelper
    {
        private static object _lastParamUsed;
        private static SplitView _splitView;
        private static Frame _rightFrame;

        public static void Initialize(SplitView splitView, Frame rightFrame)
        {
            _splitView = splitView;
            _rightFrame = rightFrame;
        }

        public static void UpdateView(Type pageType, object parameters = null, NavigationTransitionInfo infoOverride = null)
        {
            NavigationService.Navigate(pageType, parameters, infoOverride, true);
        }

        public static void Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            NavigationService.Navigate(pageType, parameter, infoOverride);
        }
    }
}

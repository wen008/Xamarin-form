using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ManLuUi.Droid.MyClass;
using ManLuUi.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(Method))]
namespace ManLuUi.Droid.MyClass
{
    public class Method : ISizeTo
    {
        public int GetValue(int value)
        {
            float scale = MainActivity.ct.Resources.DisplayMetrics.Density;
            return (int)(value * scale + 0.5f);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ManLuUi.Interface;
using ManLuUi.iOS.MyClass;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Method))]
namespace ManLuUi.iOS.MyClass
{
    public class Method : ISizeTo
    {
        public int GetValue(int value)
        {
            return 2 * value;
        }
    }
}
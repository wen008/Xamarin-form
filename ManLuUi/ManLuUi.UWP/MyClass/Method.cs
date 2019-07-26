using ManLuUi.Interface;
using ManLuUi.UWP.MyClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Method))]
namespace ManLuUi.UWP.MyClass
{
    public class Method : ISizeTo
    {
        public int GetValue(int value)
        {
            return value;
        }
    }
}

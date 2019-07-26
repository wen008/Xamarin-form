using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ManLuUi.Control
{
    public class CheckButton: RectangleButton
    {
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
    propertyName: "IsChecked",
    returnType: typeof(bool),
    declaringType: typeof(bool),
    defaultValue: false
    );
        public event EventHandler CheckChanged;


        public bool IsChecked {
            get {
                return (bool)GetValue(IsCheckedProperty);
            }
            set {
                if ((bool)GetValue(IsCheckedProperty) != value)
                {
                    SetValue(IsCheckedProperty, value);
                    InvalidateSurface();
                }
            }
        }



        //private SKPaint paint1 = null;
        //private SKPaint paint2 = null;
        //private SKPaint paint3 = null;
        //private SKRect textBounds = new SKRect();

        private bool Isload = false;
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
            if (Isload == false)
            {
                Clicked += ToggleButton_Clicked;
                
                Isload = true;
                PropertyChanged += CheckButton_PropertyChanged;
            }
        }

        private void CheckButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                CheckChanged?.Invoke(sender,e);
            }
        }

        private void ToggleButton_Clicked(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }

        public override void Tapped(object sender, EventArgs e)
        {
            base.Tapped(sender, e);
        }

        //private void ToggleButton_Clicked(object sender, EventArgs e)
        //{
        //    IsChecked = !IsChecked;
        //}

        //public override void RectangleButton_Touch(object sender, SKTouchEventArgs e)
        //{
        //    base.RectangleButton_Touch(sender, e);
        //}
    }
}

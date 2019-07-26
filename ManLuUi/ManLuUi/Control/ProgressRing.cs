using ManLuUi.Interface;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml;
using Xamarin.Forms;

namespace ManLuUi.Control
{
    public class ProgressRing:SKCanvasView
    {
      //  SKCanvasView canvasView;
        private float revolveDegrees, rotateDegrees;
        private bool IsLoad = false;

        public static readonly BindableProperty ForegroundProperty = BindableProperty.Create(
propertyName: "Foreground",
returnType: typeof(Color),
declaringType: typeof(Color),
defaultValue: Color.DimGray
);
        public Color Foreground {
            get { return (Color)GetValue(ForegroundProperty); }
            set {
                SetValue(ForegroundProperty, value);
            }
        }

        public static readonly BindableProperty RadiusProperty = BindableProperty.Create(
propertyName: "Radius",
returnType: typeof(int),
declaringType: typeof(int),
defaultValue: 3
);
        public int Radius {
            get { return (int)GetValue(RadiusProperty); }
            set {
                SetValue(RadiusProperty, value);
            }
        }


        public static readonly BindableProperty TranslateProperty = BindableProperty.Create(
propertyName: "Translate",
returnType: typeof(int),
declaringType: typeof(int),
defaultValue: 3
);
        public int Translate {
            get { return (int)GetValue(TranslateProperty); }
            set {
                SetValue(TranslateProperty, value);
            }
        }

        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(
propertyName: "IsActive",
returnType: typeof(bool),
declaringType: typeof(bool),
defaultValue: false
);
        public bool IsActive {
            get { return (bool)GetValue(IsActiveProperty); }
            set {
                if(IsActive!=value)
                {
                    SetValue(IsActiveProperty, value);
                    if (value)
                    {
                        OnAppearing();
                    }
                    else
                    {
                        OnDisappearing();
                    }
                }
                
            }
        }



        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            var radius = DependencyService.Get<ISizeTo>().GetValue(Radius);
            var translate = 0 - DependencyService.Get<ISizeTo>().GetValue(25);
        canvas.Clear();

            using (SKPaint fillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Foreground.ToSKColor(),
            })
            {
                    canvas.Translate(info.Width / 2, info.Height / 2);

                    //// Rotate around center of canvas
                    canvas.RotateDegrees(revolveDegrees);

                //  Translate horizontally
                //float radius = Math.Min(info.Width, info.Height) / 3;
                //canvas.Translate(radius, 0);

                //// Rotate around center of object
                //canvas.RotateDegrees(rotateDegrees);

                // Draw a square



                //假设圆心（0，0)  半径Translate
                //首先确定4个点的坐标（0，B)  总共均匀分布8个点吧（45*8=360）(等腰三角形  x=y）
                //不要问我为什么....因为懒得算...(以后在改写)
                float d = (float)GetY(translate);
                canvas.DrawCircle(0, translate, radius, fillPaint);
                canvas.DrawCircle(0, 0- translate, radius, fillPaint);
                canvas.DrawCircle(translate, 0, radius, fillPaint);
                canvas.DrawCircle(0- translate, 0, radius, fillPaint);
                canvas.DrawCircle(d, d, radius, fillPaint);
                canvas.DrawCircle(-d, -d, radius, fillPaint);
                canvas.DrawCircle(-d, d, radius, fillPaint);
                canvas.DrawCircle(d, -d, radius, fillPaint);
                //

            //    canvas.DrawCircle(Translate, Translate, radius, fillPaint);

            }

        }

        /// <summary>
        /// 根据X求出Y（在一个圆的轨迹上）
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetY(double r)
        {
            return Math.Sqrt(Math.Pow(r, 2) / 2.0);
        }


        public  void OnAppearing()
        {

            new Animation((value) =>
            {
                revolveDegrees = 360 * (float)value;
                 InvalidateSurface();
            }).
                Commit(this, "revolveAnimation", length: 1400, repeat: () => true);

            //new Animation((value) =>
            //{
            //    rotateDegrees = 360 * (float)value;
            //    InvalidateSurface();
            //}).Commit(this, "rotateAnimation", length: 1000, repeat: () => true);
        }


       public void OnDisappearing()
        {
            this.AbortAnimation("revolveAnimation");
            this.AbortAnimation("rotateAnimation");
        }
    }
}

using ManLuUi.Interface;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace ManLuUi.Control
{
    public class RectangleButton: SKCanvasView
    {
        /// <summary>
        /// 表示边框的宽度
        /// </summary>
        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(
            propertyName: "Thickness",
            returnType: typeof(float),
            declaringType: typeof(float),
            defaultValue: 1f);
        public float Thickness {
            get {
                return (float)GetValue(ThicknessProperty);
            }
            set {
                SetValue(ThicknessProperty, value);
            }
        }
        /// <summary>
        /// 边框的颜色
        /// </summary>
        public static readonly BindableProperty ThicknessColorProperty =BindableProperty.Create(
            propertyName: "ThicknessColor",
            returnType: typeof(Color),
            declaringType: typeof(Color),
            defaultValue: Color.DimGray);
        public Color ThicknessColor {
            get {
                return (Color)GetValue(ThicknessColorProperty);
            }
            set {
                SetValue(ThicknessColorProperty, value);
            }
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public static readonly BindableProperty BackgroundProperty = BindableProperty.Create(
            propertyName: "Background",
            returnType: typeof(Color),
            declaringType: typeof(Color),
            defaultValue: Color.WhiteSmoke);
        public Color Background {
            get {
                return (Color)GetValue(BackgroundProperty);
            }
            set {
                SetValue(BackgroundProperty, value);
            }
        } 

        /// <summary>
        /// 按钮文本
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
    propertyName: "Text",
    returnType: typeof(string),
    declaringType: typeof(string),
    defaultValue: "rec");
        public string Text {
            get {
                return (string)GetValue(TextProperty);
            }
            set {
                SetValue(TextProperty, value);
            }
        }


        public static readonly BindableProperty TextSizeProperty = BindableProperty.Create(
propertyName: "TextSize",
returnType: typeof(int),
declaringType: typeof(int),
defaultValue: 20);
        public int TextSize {
            get {
                return (int)GetValue(TextSizeProperty);
            }
            set {
                SetValue(TextSizeProperty, value);
            }
        }
        /// <summary>
        /// 文本颜色
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
propertyName: "TextColor",
            returnType: typeof(Color),
            declaringType: typeof(Color),
            defaultValue: Color.DimGray);
        public Color TextColor {
            get {
                return (Color)GetValue(TextColorProperty);
            }
            set {
                SetValue(TextColorProperty, value);
            }
        }

        /// <summary>
        /// 鼠标悬浮
        /// </summary>
        public static readonly BindableProperty IsMouseOverProperty = BindableProperty.Create(
            propertyName: "IsMouseOver",
            returnType: typeof(bool),
            declaringType: typeof(bool),
            defaultValue: false
            );
        public bool IsMouseOver {
            get {
                return (bool)GetValue(IsMouseOverProperty);
            }
            set {
                SetValue(IsMouseOverProperty, value);
                //InvalidateSurface();
            }
        }

        //点击状态
        public static readonly BindableProperty IspressedProperty = BindableProperty.Create(
            propertyName: "Ispressed",
            returnType: typeof(bool),
            declaringType: typeof(bool),
            defaultValue: false
            );
        public bool Ispressed {
            get {
                return (bool)GetValue(IsMouseOverProperty);
            }
            set {
               if ((bool)GetValue(IspressedProperty) != value)
                {
                    SetValue(IspressedProperty, value);
                    InvalidateSurface();
                }
                //InvalidateSurface();
            }
        }

        public static readonly BindableProperty RefreshProperty = BindableProperty.Create(
            propertyName: "Refresh",
            returnType: typeof(bool),
            declaringType: typeof(bool),
            defaultValue: false
            );
        public bool Refresh {
            get { return (bool)GetValue(RefreshProperty); }
            set {
                if (value == false)
                {
                    InvalidateSurface();
                }
                SetValue(RefreshProperty, false);
            }
        }


        private SKPaint paint1 = null;
        private SKPaint paint2 = null;
        private SKPaint paint3 = null;
        public event EventHandler Clicked;
        private SKRect textBounds=new SKRect();

        private bool Isload = false;

        /// <summary>
        /// 绘制矩形按钮
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
         //   Thread.Sleep(3000);
            SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;
            int n1 = DependencyService.Get<ISizeTo>().GetValue(1);
            int n3 = DependencyService.Get<ISizeTo>().GetValue(3);
            int n4 = DependencyService.Get<ISizeTo>().GetValue(4);
            canvas.Clear();
            if (paint1 != null)
            {
                paint1.Dispose();
                paint1 = null;
            }
            if (paint2 != null)
            {
                paint2.Dispose();
                paint2 = null;
            }
            if (paint3 != null)
            {
                paint3.Dispose();
                paint3 = null;
            }
            //边框画笔
            paint1 = new SKPaint() 
            {
                Color = ThicknessColor.ToSKColor(),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = Thickness,
            };
            //背景画笔
            paint2 = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = Background.ToSKColor(),
                //投影  阴影效果
                ImageFilter = SKImageFilter.CreateDropShadow(n1, n1, n3, n3, SKColors.Black, SKDropShadowImageFilterShadowMode.DrawShadowAndForeground)
            };
            //文本画笔
            paint3 = new SKPaint()
            {
                Color=TextColor.ToSKColor(),
                Typeface= SKFontManager.Default.MatchCharacter('是')
            // TextSize= TestSize,
            //ImageFilter = SKImageFilter.CreateDropShadow(10, 10 ,6, 6, SKColors.Red, SKDropShadowImageFilterShadowMode.DrawShadowAndForeground)

        };
            
            float textWidth = paint3.MeasureText(Text);
            paint3.TextSize = DependencyService.Get<ISizeTo>().GetValue(TextSize);
            // Find the text bounds   

            paint3.MeasureText(Text, ref textBounds);


            canvas.DrawRect(n1, n1, info.Width-n4, info.Height-n4, paint1);

            canvas.DrawRect(Thickness + n1, Thickness + n1, info.Width - n4 - Thickness, info.Height - n4 - Thickness, paint2);
            float xText = info.Width / 2 - textBounds.MidX;
            float yText = info.Height / 2 - textBounds.MidY;
            canvas.DrawText(Text, xText, yText,paint3);

            if(Isload==false)
            {
                //TapGestureRecognizer tap = new TapGestureRecognizer();
                
                //tap.Tapped += Tapped;
                //GestureRecognizers.Add(tap);
                if (EnableTouchEvents)
                {
                    Touch += RectangleButton_Touch;
                }
                Isload = true;
            }


        }

        private void RectangleButton_Touch(object sender, SKTouchEventArgs e)
        {
            e.Handled = true;
            switch (e.ActionType)
            {
                case SKTouchAction.Entered:
                    IsMouseOver = true;
                    break;
                case SKTouchAction.Pressed:
                    Ispressed = true;
                    break;
                case SKTouchAction.Moved:
                   
                    break;
                case SKTouchAction.Released:
                    Ispressed = false;
                        Clicked?.Invoke(sender, e);
                    break;
                case SKTouchAction.Exited:
                    //Ispressed = false;
                    IsMouseOver = false;
                    break;

                case SKTouchAction.Cancelled:
                    break;
            }
        }

        public virtual void Tapped(object sender, EventArgs e)
        {
            Clicked?.Invoke(sender, e);
        }
    }
}

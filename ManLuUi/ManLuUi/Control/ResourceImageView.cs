using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ManLuUi.Control
{
    public class ResourceImageView : SKCanvasView
    {
        public static readonly BindableProperty ImageStringProperty = BindableProperty.Create(
propertyName: "ImageString",
returnType: typeof(string),
declaringType: typeof(string),
defaultValue: null
);
        public string ImageString {
            get { return (string)GetValue(ImageStringProperty); }
            set {
                SetValue(ImageStringProperty, value);
            }
        }


        private SKBitmap bitmap = null;
        private bool IsLoad = false;
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
            SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;

            if (bitmap != null)
            {
                bitmap.Dispose();
            }
            if (ImageString != null)
            {
                try
                {
                    bitmap = LoadBitmapResource(typeof(ResourceImageView), ImageString);
                }
                catch
                {
                    //图片加载不到啊
                    //这里可以加载一张固定的
                }
            }
            else
            {
                //没有图片资源
                //这里也可以加载一张固定的
            }
            canvas.Clear();

            canvas.DrawBitmap(bitmap, info.Rect);

            if (IsLoad == false)
            {
                IsLoad = true;
                if (EnableTouchEvents)
                {
                    Touch += ResourceImageView_Touch;
                }

            }

        }
        public event EventHandler TouchDown;
        public event EventHandler TouchMove;
        public event EventHandler TouchUp;

        private void ResourceImageView_Touch(object sender, SKTouchEventArgs e)
        {
            e.Handled = true;
            switch (e.ActionType)
            {
                case SKTouchAction.Entered:
                    //悬浮？
                    break;
                case SKTouchAction.Pressed:
                    TouchDown?.Invoke(sender, e);
                    break;
                case SKTouchAction.Moved:
                    TouchMove?.Invoke(sender, e);
                    break;
                case SKTouchAction.Released:
                    TouchUp?.Invoke(sender, e);
                    break;
                case SKTouchAction.Exited:
                    //不在区域内
                    break;
            }
        }

        public SKBitmap LoadBitmapResource(Type type, string resourceID)
        {
            var vvv = resourceID.GetType();
            Assembly assembly = type.GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(resourceID)) { return SKBitmap.Decode(stream); }
        }
    }
}

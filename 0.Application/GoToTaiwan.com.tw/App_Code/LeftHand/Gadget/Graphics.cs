using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;


namespace LeftHand.Gadget
{
    public class Graphics
    {
        //Combine
        public static Bitmap Combine(List<Bitmap> SourceImages)
        {
            //找出最大Size的Image
            int Width = SourceImages.Max(s => s.Width);
            int Height = SourceImages.Max(s => s.Height);

            Bitmap Result = new Bitmap(Width, Height);
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(Result);

            foreach (Bitmap Source in SourceImages)
            {
                gr.DrawImage(Source, 0, 0);
            }

            return Result;
        }

        //Resize
        public enum ResizeMode { ByWidth, ByHeight }
        public static Bitmap Resize(Bitmap OriginalImage, int MaxLength, ResizeMode Mode)
        {
            int OriginalWidth = OriginalImage.Width;
            int OriginalHeight = OriginalImage.Height;

            //計算圖片要縮小的比例
            double ResizePercentage = 0;

            switch (Mode)
            {
                case ResizeMode.ByWidth:
                    ResizePercentage = (double)OriginalWidth / (double)MaxLength;
                    break;
                case ResizeMode.ByHeight:
                    ResizePercentage = (double)OriginalHeight / (double)MaxLength;
                    break;
            }

            //填入要縮小的寬度與高度
            int NewWidth = (int)(Math.Ceiling(OriginalWidth / ResizePercentage));
            int NewHeight = (int)(Math.Ceiling(OriginalHeight / ResizePercentage));

            //高品質縮圖
            Bitmap ResizedBitmap = new Bitmap(NewWidth, NewHeight);
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(ResizedBitmap);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            gr.DrawImage(OriginalImage, 0, 0, NewWidth, NewHeight);

            return ResizedBitmap;
        }

        public enum ScopeMode { InScope, OutScope }
        public static Bitmap ResizeByScope(Bitmap OriginalImage, int ScopeWidth, int ScopeHeight, ScopeMode ScopeMode)
        {
            Bitmap ScopeBitmap = new Bitmap(ScopeWidth, ScopeHeight);

            double WidthPercentage = (double)OriginalImage.Width / (double)ScopeWidth;
            double HeightPercentage = (double)OriginalImage.Height / (double)ScopeHeight;

            //確定縮放模式
            Bitmap ResizedBitmap = null;
            switch (ScopeMode)
            {
                case ScopeMode.InScope:
                    if (WidthPercentage > HeightPercentage)
                    { ResizedBitmap = Resize(OriginalImage, ScopeWidth, ResizeMode.ByWidth); }
                    else
                    { ResizedBitmap = Resize(OriginalImage, ScopeHeight, ResizeMode.ByHeight); }
                    break;

                case ScopeMode.OutScope:
                    if (WidthPercentage > HeightPercentage)
                    { ResizedBitmap = Resize(OriginalImage, ScopeHeight, ResizeMode.ByHeight); }
                    else
                    { ResizedBitmap = Resize(OriginalImage, ScopeWidth, ResizeMode.ByWidth); }
                    break;
            }

            //置中座標補償
            int LeftX = (ScopeBitmap.Width - ResizedBitmap.Width) / 2;
            int TopY = (ScopeBitmap.Height - ResizedBitmap.Height) / 2;

            //Graphics
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(ScopeBitmap);
            gr.Clear(Color.Black);
            gr.DrawImage(ResizedBitmap, LeftX, TopY, ResizedBitmap.Width, ResizedBitmap.Height);

            return ScopeBitmap;
        }

        //Save
        private static ImageCodecInfo GetEncoder(ImageFormat Format)
        {
            ImageCodecInfo[] Codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo Codec in Codecs)
            {
                if (Codec.FormatID == Format.Guid) return Codec;
            }
            return null;
        }
        public static void SaveToJpg(Bitmap Bitmap, string FileName, int Quality = 80)
        {
            FileName = FileName.Replace(Path.GetExtension(FileName), "") + ".jpg";

            EncoderParameters EncoderParameters = new EncoderParameters();
            EncoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Convert.ToInt64(Quality));
            Bitmap.Save(FileName, GetEncoder(ImageFormat.Jpeg), EncoderParameters);
        }
        public static void SaveToPng(Bitmap Bitmap, string FileName)
        {
            FileName = FileName.Replace(Path.GetExtension(FileName), "") + ".png";

            Bitmap.Save(FileName, ImageFormat.Png);
        }

        //ProcessImage + Save
        public enum SaveFormat { Jpg, Png }
        public static void CombineAndSave(List<Bitmap> SourceImages, string FileName, SaveFormat SaveFormat)
        {
            Bitmap Image = Combine(SourceImages);

            switch (SaveFormat)
            {
                case SaveFormat.Jpg:
                    SaveToJpg(Image, FileName);
                    break;

                case SaveFormat.Png:
                    SaveToPng(Image, FileName);
                    break;
            }
        }
        public static void ResizeAndSave(Bitmap OriginalImage, int MaxLength, ResizeMode Mode, string FileName, SaveFormat SaveFormat)
        {
            Bitmap Image = Resize(OriginalImage, MaxLength, Mode);

            switch (SaveFormat)
            {
                case SaveFormat.Jpg:
                    SaveToJpg(Image, FileName);
                    break;

                case SaveFormat.Png:
                    SaveToPng(Image, FileName);
                    break;
            }
        }
        public static void ResizeByScopeAndSave(Bitmap OriginalImage, int ScopeWidth, int ScopeHeight, ScopeMode ScopeMode, string FileName, SaveFormat SaveFormat)
        {
            Bitmap Image = ResizeByScope(OriginalImage, ScopeWidth, ScopeHeight, ScopeMode);

            switch (SaveFormat)
            {
                case SaveFormat.Jpg:
                    SaveToJpg(Image, FileName);
                    break;

                case SaveFormat.Png:
                    SaveToPng(Image, FileName);
                    break;
            }
        }
    }
}
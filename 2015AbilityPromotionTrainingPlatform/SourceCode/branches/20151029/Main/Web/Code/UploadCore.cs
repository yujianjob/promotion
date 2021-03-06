﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Code
{
    public class UploadCore
    {
        enum UploadFileType
        {
            Image = 1,
            Attach = 2,
            Video = 3
        }

        private static string ImageExts = ".jpg,.jpeg,.gif,.png";
        private static string AttachExts = ".zip,.rar,.7z,.pdf,.doc,.txt,.xls,.docx,.xlsx";
        private static string VideoExts = ".mp4";
        private static int ImageLimit = 3 * 1024 * 1024;
        private static int AttachLimit = 10 * 1024 * 1024;
        private static int VideoLimit = 3 * 1024 * 1024;

        public static string UploadImage(HttpPostedFileBase file, ref string msg)
        {
            if (file == null)
            {
                msg = "找不到文件。";
                return "";
            }

            if (!CheckFileSize(file.ContentLength, UploadFileType.Image))
            {
                msg = "文件大小超过限制。";
                return "";
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            if (!CheckFileExt(fileName, UploadFileType.Image))
            {
                msg = "非法的文件后缀名。";
                return "";
            }

            string dir = ("/Upload/") + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(dir)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dir));

            string path = dir + Guid.NewGuid() + fileExt;
            file.SaveAs(HttpContext.Current.Server.MapPath(path));

            msg = "上传成功。";
            return path;
        }

        public static bool CreateThumbs(string filePath, int thumbsSize)
        {
            string dirThumbs = Path.GetDirectoryName(filePath) + "/_thumbs/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(dirThumbs)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dirThumbs));

            try
            {
                int thumbsWidth, thumbsHeight;
                Image image = Image.FromFile(HttpContext.Current.Server.MapPath(filePath));
                int sourceWith = image.Width;
                int sourceHeight = image.Height;
                if (image.Width >= image.Height)
                {
                    thumbsWidth = thumbsSize;
                    thumbsHeight = sourceHeight * thumbsSize / sourceWith;
                }
                else
                {
                    thumbsWidth = sourceWith * thumbsSize / sourceHeight;
                    thumbsHeight = thumbsSize;
                }

                Bitmap bmp = new Bitmap(thumbsWidth, thumbsHeight);
                Graphics GR = Graphics.FromImage(bmp);
                GR.SmoothingMode = SmoothingMode.Default;
                GR.CompositingQuality = CompositingQuality.Default;
                GR.InterpolationMode = InterpolationMode.Default;
                Rectangle rectDestination = new Rectangle(0, 0, thumbsWidth, thumbsHeight);
                GR.DrawImage(image, rectDestination, 0, 0, sourceWith, sourceHeight, GraphicsUnit.Pixel);

                string fileName = Path.GetFileName(filePath);
                string fileExt = Path.GetExtension(filePath);
                switch (fileExt.ToLower())
                {
                    case ".gif":
                        bmp.Save(HttpContext.Current.Server.MapPath(dirThumbs + fileName), System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case ".jpg":
                        bmp.Save(HttpContext.Current.Server.MapPath(dirThumbs + fileName), System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".jpeg":
                        bmp.Save(HttpContext.Current.Server.MapPath(dirThumbs + fileName), System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".png":
                        bmp.Save(HttpContext.Current.Server.MapPath(dirThumbs + fileName), System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default: return false;
                }

                bmp.Dispose();
                GR.Dispose();
                image.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string UploadAttach(HttpPostedFileBase file, ref string msg)
        {
            if (file == null)
            {
                msg = "找不到文件。";
                return "";
            }

            if (!CheckFileSize(file.ContentLength, UploadFileType.Attach))
            {
                msg = "文件大小超过限制。";
                return "";
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            if (!CheckFileExt(fileName, UploadFileType.Attach))
            {
                msg = "非法的文件后缀名。";
                return "";
            }

            string dir = ("/Upload/") + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(dir)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dir));

            string path = dir + Guid.NewGuid() + fileExt;
            file.SaveAs(HttpContext.Current.Server.MapPath(path));

            msg = "上传成功。";
            return path;
        }

        public static string UploadVideo(HttpPostedFileBase file, ref string msg)
        {
            if (file == null)
            {
                msg = "找不到文件。";
                return "";
            }

            if (!CheckFileSize(file.ContentLength, UploadFileType.Video))
            {
                msg = "文件大小超过限制。";
                return "";
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            if (!CheckFileExt(fileName, UploadFileType.Video))
            {
                msg = "非法的文件后缀名。";
                return "";
            }

            string dir = ("/Upload/") + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(dir)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dir));

            string path = dir + Guid.NewGuid() + fileExt;
            file.SaveAs(HttpContext.Current.Server.MapPath(path));

            msg = "上传成功。";
            return path;
        }

        private static bool CheckFileSize(int fileSize, UploadFileType type)
        {
            int limit = 0;
            switch (type)
            {
                case UploadFileType.Image:
                    limit = ImageLimit;
                    break;
                case UploadFileType.Attach:
                    limit = AttachLimit;
                    break;
                case UploadFileType.Video:
                    limit = VideoLimit;
                    break;
            }
            return limit > fileSize;
        }

        private static bool CheckFileExt(string fileName, UploadFileType type)
        {
            string fileExt = Path.GetExtension(fileName).ToLower();
            switch (type)
            {
                case UploadFileType.Image:
                    return Array.IndexOf(ImageExts.Split(','), fileExt) != -1;
                case UploadFileType.Attach:
                    return Array.IndexOf(AttachExts.Split(','), fileExt) != -1;
                case UploadFileType.Video:
                    return Array.IndexOf(VideoExts.Split(','), fileExt) != -1;
                default:
                    return false;
            }
        }
    }
}
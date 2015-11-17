using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using XXW.Enum;

namespace XXW.SiteUtils
{
    [Serializable]
    public class ImgResultModel
    {
        public string FileName { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Domain { get; set; }
        public string path { get; set; }

    }
    public enum OperationModeEnum
    {
        HW,
        W,
        H,
        Cut,
        Ori,
        Auto,
        /// <summary>
        /// 以最短一条边为尺寸。
        /// </summary>
        AutoRefMinSize
    }

    public enum ImgTarget
    {
        MemberPhoto,
        ReadingBookPic,
        File,
        AcivesPic,
        ActivesAttachment,
        ReadingRecommendPic,
        CoursePic,
        WareContentPic,
        WareContentFlash,
        WareAttachment,
        PortalEspeciallyPlan,
        PortalEspeciallyPlanPreview,
        PortalEspeciallyPlanHP,
        PortalSpecialRecommendCover,
        CircleCover,
        CirclePhotoAttachment,
        CircleFileAttachment,
        CourseRollPic,
        SharePic,
        LevelIco,
        TeamChargePersonPic,
        TeamHonorPic,
        TeamLogo,
        RecommendTeamPic
    }
    public class UploadFileHelper
    {
       
        //
        // GET: /UploadFile/
        /// <summary>
        /// 上传的零时目录
        /// </summary>
        //private string _path = "File/Temp";

        private string datetimePath;
        public static string ImageFileExt = ".jpg,.jpeg,.png,.bmp,.gif";

        public static string OtherFileExt = ".exe,.bat,.msi";

        public static string FlashFileExt = ".swf";

        public string TargetPath
        {
            get { return datetimePath; }
        }
        string _serverMapPath;
        private string StorageRoot
        {
            get { return _serverMapPath; }
        }

        /// <summary>
        /// 参数给Server.MapPath("~/")
        /// </summary>
        /// <param name="serverMapPath"></param>
        public UploadFileHelper(string serverMapPath)
        {
            this._serverMapPath = serverMapPath;
            DateTime c = DateTime.Now;
            datetimePath = c.Year.ToString() + "\\" + c.Month.ToString() + "\\" + c.Day.ToString();
        }


        public List<ImgResultModel> UpPic(ImgTarget e, HttpRequestBase request)
        {
            return UpPic(e, request, false);
        }
        public List<ImgResultModel> UpPic(ImgTarget e, HttpRequestBase request, bool useOriginalFileName)
        {

            List<ImgResultModel> r = new List<ImgResultModel>();

            foreach (string file in request.Files)
            {
                var statuses = new List<ImgResultModel>();
                var headers = request.Headers;

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {
                    UploadWholeFile(request, statuses, e, useOriginalFileName);
                }
                else
                {
                    UploadPartialFile(headers["X-File-Name"], request, statuses, e);
                }
                return statuses;

            }

            return r;
        }



        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadPartialFile(string fileName, HttpRequestBase request, List<ImgResultModel> result, ImgTarget e)
        {

            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            var fullName = Path.Combine(StorageRoot + getFloderName(e), Path.GetFileName(fileName));

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            result.Add(new ImgResultModel() { FileName = Path.GetFileName(fileName), Type = file.ContentType, Domain = XXW.SiteUtils.SiteAddressHelper.Instance.GetUrl(XXW.Enum.SiteTypeEnum.ResourceSite), path = (getFloderName(e) + "/").Replace("\\", "/"), Size = file.ContentLength });
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private Exception UploadWholeFile(HttpRequestBase request, List<ImgResultModel> result, ImgTarget e, bool userOriginalFileName)
        {
            Exception exn = null;
            for (int i = 0; i < request.Files.Count; i++)
            {

                string ext;
                string f2 = save(request.Files[i], e, out ext, userOriginalFileName);

                List<FileModel> list = new List<FileModel>();
                if (e != ImgTarget.File)
                {
                    string targetPath = StorageRoot + "\\" + getFloderName(e);
                    string oPath = getFloderName(e);
                    switch (e)
                    {
                        case ImgTarget.ReadingBookPic:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakethumbanilBookPic(Path.Combine(targetPath, f2)) });
                            break;
                        case ImgTarget.MemberPhoto:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnailMemberPhoto(Path.Combine(targetPath, f2)) });
                            break;
                        case ImgTarget.AcivesPic:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakethumbanilActivesPic(Path.Combine(targetPath, f2)) });
                            break;
                        case ImgTarget.ActivesAttachment:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            list.Add(new FileModel() { FilePath = targetPath + "\\S\\", OppositePath = oPath + "\\S\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 106, 106, OperationModeEnum.AutoRefMinSize) });
                            list.Add(new FileModel() { FilePath = targetPath + "\\M\\", OppositePath = oPath + "\\M\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnailLow(Path.Combine(targetPath, f2), 1024, 1024, OperationModeEnum.Ori) });
                            break;
                        case ImgTarget.ReadingRecommendPic:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnailLow(Path.Combine(targetPath, f2), 652, 205, OperationModeEnum.HW) });
                            break;
                        case ImgTarget.CoursePic:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakeThumbanilCoursePic(Path.Combine(targetPath, f2)) });
                            break;
                        case ImgTarget.CourseRollPic:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnailLow(Path.Combine(targetPath, f2), 652, 205, OperationModeEnum.HW) });
                            break;

                        case ImgTarget.WareContentPic:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            list.Add(new FileModel() { FilePath = targetPath + "\\S\\", OppositePath = oPath + "\\S\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 380, 200, OperationModeEnum.Auto) });
                            list.Add(new FileModel() { FilePath = targetPath + "\\M\\", OppositePath = oPath + "\\M\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnailLow(Path.Combine(targetPath, f2), 980, 520, OperationModeEnum.Ori) });
                            break;

                        case ImgTarget.WareContentFlash:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            break;
                        case ImgTarget.WareAttachment:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            break;
                        case ImgTarget.PortalEspeciallyPlan:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            //list.Add(new FileModel() { FilePath = targetPath + "\\S\\", OppositePath = oPath + "\\S\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 46, 46, OperationModeEnum.HW) });
                            list.Add(new FileModel() { FilePath = targetPath + "\\M\\", OppositePath = oPath + "\\M\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnailLow(Path.Combine(targetPath, f2), 1000, 400, OperationModeEnum.HW) });
                            break;
                        case ImgTarget.PortalEspeciallyPlanPreview:
                            //list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            list.Add(new FileModel() { FilePath = targetPath + "\\S\\", OppositePath = oPath + "\\S\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 46, 46, OperationModeEnum.HW) });
                            //list.Add(new FileModel() { FilePath = targetPath + "\\M\\", OppositePath = oPath + "\\M\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnailLow(Path.Combine(targetPath, f2), 1000, 400, OperationModeEnum.HW) });
                            break;
                        case ImgTarget.PortalEspeciallyPlanHP:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            //list.Add(new FileModel() { FilePath = targetPath + "\\S\\", OppositePath = oPath + "\\S\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 46, 46, OperationModeEnum.HW) });
                            //list.Add(new FileModel() { FilePath = targetPath + "\\M\\", OppositePath = oPath + "\\M\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnailLow(Path.Combine(targetPath, f2), 1000, 400, OperationModeEnum.HW) });
                            break;
                        case ImgTarget.PortalSpecialRecommendCover:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            break;
                        case ImgTarget.CircleCover:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 177, 177, OperationModeEnum.HW) });
                            break;
                        case ImgTarget.CircleFileAttachment:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });
                            break;
                        case ImgTarget.CirclePhotoAttachment:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            list.Add(new FileModel() { FilePath = targetPath + "\\S\\", OppositePath = oPath + "\\S\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 100, 100, OperationModeEnum.AutoRefMinSize) });
                            break;
                        case ImgTarget.SharePic:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            list.Add(new FileModel() { FilePath = targetPath + "\\S\\", OppositePath = oPath + "\\S\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 100, 100, OperationModeEnum.HW) });
                            break;
                        case ImgTarget.LevelIco:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 100, 100, OperationModeEnum.HW) });
                            break;
                        case ImgTarget.TeamChargePersonPic:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            break;
                        case ImgTarget.TeamHonorPic:
                            list.Add(new FileModel() { FilePath = targetPath, OppositePath = oPath + "\\", FileName = f2, Content = null });//原图
                            break;
                        case ImgTarget.TeamLogo:
                            list.Add(new FileModel() { FilePath = targetPath + "\\", OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 200, 200, OperationModeEnum.AutoRefMinSize) });
                            break;
                        case ImgTarget.RecommendTeamPic:
                            list.Add(new FileModel() { FilePath = targetPath + "\\", OppositePath = oPath + "\\", FileName = f2, Content = ThumbnailOperation.MakeThumbnail(Path.Combine(targetPath, f2), 652, 300, OperationModeEnum.AutoRefMinSize) });
                            break;
                        default:
                            break;
                    }


                    foreach (FileModel m in list)
                    {
                        if (m.Content == null)
                        {
                            result.Add(new ImgResultModel() { FileName = m.FileName, Type = request.Files[i].ContentType, Domain = XXW.SiteUtils.SiteAddressHelper.Instance.GetUrl(XXW.Enum.SiteTypeEnum.ResourceSite), path = (m.OppositePath).Replace("\\", "/"), Size = request.Files[i].ContentLength });
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(m.Content);
                            Bitmap bitMap = new Bitmap(ms);

                            try
                            {
                                if (!Directory.Exists(m.FilePath))
                                    Directory.CreateDirectory(m.FilePath);

                                bitMap.Save(m.FilePath + "\\" + m.FileName);

                            }
                            catch (Exception ex)
                            {
                                int a = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                                exn = ex;
                            }
                            finally
                            {
                                bitMap.Dispose();
                                ms.Close();
                            }

                            result.Add(new ImgResultModel() { FileName = m.FileName, Type = request.Files[i].ContentType, Domain = XXW.SiteUtils.SiteAddressHelper.Instance.GetUrl(XXW.Enum.SiteTypeEnum.ResourceSite), path = (m.OppositePath).Replace("\\", "/"), Size = request.Files[i].ContentLength });
                        }

                    }
                }

            }

            return exn;
        }

        private string save(HttpPostedFileBase file, ImgTarget t, out string extName, bool userOriginalFileName)
        {

            string ext = Path.GetExtension(file.FileName);
            string fn = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            if (userOriginalFileName)
                fn = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
            //string fn2 = fn + "_" + DateTime.Now.ToString("yyyyMMddHHmmssdddd");
            string a = fn + ext;

            //var fullPath = Path.Combine(StorageRoot, Path.GetFileName(file.FileName));
            string fullPath = Path.Combine(StorageRoot + "\\" + getFloderName(t), a);

            if (!Directory.Exists(StorageRoot + "\\" + getFloderName(t)))
                Directory.CreateDirectory(StorageRoot + "\\" + getFloderName(t));
            extName = ext;
            file.SaveAs(fullPath);
            return a;
        }
        public string getFloderName(ImgTarget t)
        {
            string path = string.Empty;
            switch (t)
            {
                case ImgTarget.MemberPhoto:
                    path = "Member\\Photo";
                    break;
                case ImgTarget.ReadingBookPic:
                    path = "Reading\\BookPic";
                    break;
                case ImgTarget.AcivesPic:
                    path = "Activity\\ActivityPic";
                    break;
                case ImgTarget.ActivesAttachment:
                    path = "Activity\\ActivityAtt";
                    break;
                case ImgTarget.ReadingRecommendPic:
                    path = "Reading\\Recommend";
                    break;
                case ImgTarget.CoursePic:
                    path = "Course\\CoursePic";
                    break;
                case ImgTarget.WareContentPic:
                    path = "Course\\WareContentPic";
                    break;
                case ImgTarget.WareContentFlash:
                    path = "Course\\WareContentFlash";
                    break;
                case ImgTarget.WareAttachment:
                    path = "Course\\WareAttachment";
                    break;
                case ImgTarget.PortalEspeciallyPlan:
                    path = "Portal\\EspeciallyPlan";
                    break;
                case ImgTarget.PortalEspeciallyPlanPreview:
                    path = "Portal\\EspeciallyPlan";
                    break;
                case ImgTarget.PortalEspeciallyPlanHP:
                    path = "Portal\\PortalEspeciallyPlanHP";
                    break;
                case ImgTarget.PortalSpecialRecommendCover:
                    path = "Portal\\SpecialRecommendCover";
                    break;
                case ImgTarget.CircleCover:
                    path = "Circle\\CircleCover";
                    break;
                case ImgTarget.CircleFileAttachment:
                    path = "Circle\\CircleFileAttachment";
                    break;
                case ImgTarget.CirclePhotoAttachment:
                    path = "Circle\\CirclePhotoAttachment";
                    break;
                case ImgTarget.CourseRollPic:
                    path = "Course\\CourseRollPic";
                    break;
                case ImgTarget.SharePic:
                    path = "Share\\SharePic";
                    break;
                case ImgTarget.LevelIco:
                    path = "Level\\Ico";
                    break;
                case ImgTarget.TeamChargePersonPic:
                    path = "Team\\ChargePersonPic";
                    break;
                case ImgTarget.TeamHonorPic:
                    path = "Team\\HonorPic";
                    break;
                case ImgTarget.TeamLogo:
                    path = "Team\\Logo";
                    break;
                case ImgTarget.RecommendTeamPic:
                    path = "Team\\Recommend";
                    break;
                default:
                    path = "Files";
                    break;
            }
            return path + "\\" + TargetPath;

        }


        public static UploadErrorEnum CheckImgFileExt(HttpRequestBase request)
        {
            UploadErrorEnum ue = UploadErrorEnum.None;
            if (request.Files.Count > 0)
            {
                foreach (string fileName in request.Files)
                {
                    HttpPostedFileBase file = request.Files[fileName];
                    string ext = Path.GetExtension(file.FileName).ToLower();
                    if (ImageFileExt.IndexOf(ext) == -1)
                    {
                        ue = UploadErrorEnum.InvalidExtension;
                        break;
                    }
                }
            }
            return ue;
        }

        public static UploadErrorEnum CheckOtherFileExt(HttpRequestBase request)
        {
            UploadErrorEnum ue = UploadErrorEnum.None;
            if (request.Files.Count > 0)
            {
                foreach (string fileName in request.Files)
                {
                    HttpPostedFileBase file = request.Files[fileName];
                    string ext = Path.GetExtension(file.FileName).ToLower();
                    if (OtherFileExt.IndexOf(ext) > -1)
                    {
                        ue = UploadErrorEnum.InvalidExtension;
                        break;
                    }
                }
            }
            return ue;
        }

        public static UploadErrorEnum CheckFlashFileExt(HttpRequestBase request)
        {
            UploadErrorEnum ue = UploadErrorEnum.None;
            if (request.Files.Count > 0)
            {
                foreach (string fileName in request.Files)
                {
                    HttpPostedFileBase file = request.Files[fileName];
                    string ext = Path.GetExtension(file.FileName).ToLower();
                    if (FlashFileExt.IndexOf(ext) == -1)
                    {
                        ue = UploadErrorEnum.InvalidExtension;
                        break;
                    }
                }
            }
            return ue;
        }

        /// <summary>
        /// 判断上传后文件的大小，kb为单位。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static UploadErrorEnum CheckFileSize(HttpRequestBase request, int size)
        {
            size = size * 1024;
            for (int i = 0; i < request.Files.Count; i++)
            {
                if (request.Files[i].ContentLength > size)
                {
                    return UploadErrorEnum.TooLarge;
                }
            }
            return UploadErrorEnum.None;
        }

        /*
        public static UploadErrorEnum CheckFile(HttpRequestBase request, ImgTarget it)
        {
            UploadErrorEnum ue_rst = UploadErrorEnum.None;
            foreach (UploadErrorEnum ue in UploadErrorList)
            {
                switch (ue)
                {
                    case UploadErrorEnum.TooLarge:
                        break;
                    case UploadErrorEnum.InvalidExtension:
                        break;
                }
                if (ue_rst != UploadErrorEnum.None)
                    break;
            }
            return ue_rst;
        }

        private static IList<UploadErrorEnum> _uploadErrorList;
        public static IList<UploadErrorEnum> UploadErrorList
        {
            get {
                if (_uploadErrorList == null) {
                    _uploadErrorList = new List<UploadErrorEnum>();
                    _uploadErrorList.Add(UploadErrorEnum.TooLarge);
                    _uploadErrorList.Add(UploadErrorEnum.InvalidExtension);
                }
                return _uploadErrorList;
            }
        }
         * */
    }

    public class FileModel
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string OppositePath { get; set; }

    }

    public class ThumbnailOperation
    {

        public static byte[] MakeThumbnailMemberPhoto(string originalImagePath)
        {

            return MakeThumbnail(originalImagePath, 120, 120, OperationModeEnum.Auto);

        }

        public static byte[] MakethumbanilBookPic(string originalImagePath)
        {
            return MakeThumbnail(originalImagePath, 110, 164, OperationModeEnum.HW);
        }


        public static byte[] MakethumbanilActivesPic(string originalImagePath)
        {
            return MakeThumbnail(originalImagePath, 184, 184, OperationModeEnum.Cut);
        }

        public static byte[] MakeThumbanilCoursePic(string originalImagePath)
        {
            return MakeThumbnail(originalImagePath, 184, 184, OperationModeEnum.Cut);
        }

        ///// <summary>
        ///// 第一个
        ///// </summary>
        ///// <param name="originalImagePath"></param>
        ///// <returns></returns>
        //public static List<byte[]> MakethumbanilActivesAttSmall(string originalImagePath)
        //{
        //    List<byte[]> result = new List<byte[]>();
        //    result.Add(MakeThumbnail(originalImagePath, 106, 106, OperationModeEnum.Auto));
        //    result.Add(MakeThumbnailLow(originalImagePath, 500, 500, OperationModeEnum.Ori));
        //    return result;


        //}
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static byte[] MakeThumbnail(string originalImagePath, int width, int height, OperationModeEnum mode)
        {
            byte[] imgData;

            #region

            using (Image originalImage = Image.FromFile(originalImagePath))
            {

                int towidth = width;
                int toheight = height;

                int x = 0;
                int y = 0;
                int ow = originalImage.Width;
                int oh = originalImage.Height;

                switch (mode)
                {
                    case OperationModeEnum.HW://指定高宽缩放（可能变形）                
                        break;
                    case OperationModeEnum.W://指定宽，高按比例                    
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case OperationModeEnum.H://指定高，宽按比例
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case OperationModeEnum.Cut://指定高宽裁减（不变形）                
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * towidth / toheight;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / towidth;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                        break;
                    case OperationModeEnum.Auto:
                        AutoSize(ow, oh, ref towidth, ref toheight);
                        break;
                    case OperationModeEnum.AutoRefMinSize:
                        AutoRefMinSize(ow, oh, ref towidth, ref toheight);
                        break;
                    default:
                        towidth = originalImage.Width;
                        toheight = originalImage.Height;
                        break;
                }

                //新建一个bmp图片
                using (Image bitmap = new System.Drawing.Bitmap(towidth, toheight))
                {

                    //新建一个画板
                    Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                    //设置高质量插值法
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //清空画布并以透明背景色填充
                    g.Clear(Color.Transparent);

                    //在指定位置并且按指定大小绘制原图片的指定部分
                    g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                        new Rectangle(x, y, ow, oh),
                        GraphicsUnit.Pixel);

                    try
                    {
                        //以jpg格式保存缩略图
                        /*
                        Bitmap a = new Bitmap(bitmap);
                        a.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        a.Dispose();
                         * */

                        Bitmap a = new Bitmap(bitmap);
                        MemoryStream ms = new MemoryStream();
                        a.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        /*
                        Bitmap a2 = new Bitmap(ms);
                        a2.Save(HttpContext.Current.Server.MapPath("/upload/s/a2.jpg"));

                         * */
                        imgData = ms.GetBuffer();
                        //new WebService1().SaveThumnail(imgData);
                        ms.Close();

                        /*
                        using (Stream stream = new MemoryStream())
                        { 
                            //克隆Bitmap对象  
                    Bitmap bmp = new Bitmap(bitmap);
                    bmp.Save(stream, bitmap.RawFormat);
                     bmp.Dispose();  
                        
                         * */
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                        //imgData = null;
                    }
                    finally
                    {
                        g.Dispose();
                        bitmap.Dispose();
                        originalImage.Dispose();
                    }
                    return imgData;
                }
            }
            #endregion
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static byte[] MakeThumbnailLow(string originalImagePath, int width, int height, OperationModeEnum mode)
        {
            byte[] imgData;

            #region

            using (Image originalImage = Image.FromFile(originalImagePath))
            {

                int towidth = width;
                int toheight = height;

                int x = 0;
                int y = 0;
                int ow = originalImage.Width;
                int oh = originalImage.Height;

                switch (mode)
                {
                    case OperationModeEnum.HW://指定高宽缩放（可能变形）                
                        break;
                    case OperationModeEnum.W://指定宽，高按比例                    
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case OperationModeEnum.H://指定高，宽按比例
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case OperationModeEnum.Cut://指定高宽裁减（不变形）                
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * towidth / toheight;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / towidth;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                        break;
                    case OperationModeEnum.Auto:
                        AutoSize(ow, oh, ref towidth, ref toheight);
                        break;
                    case OperationModeEnum.AutoRefMinSize:
                        AutoRefMinSize(ow, oh, ref towidth, ref toheight);
                        break;
                    default:
                        towidth = originalImage.Width;
                        toheight = originalImage.Height;
                        break;
                }

                //新建一个bmp图片
                using (Image bitmap = new System.Drawing.Bitmap(towidth, toheight))
                {

                    //新建一个画板
                    Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                    //设置高质量插值法
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;

                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

                    //清空画布并以透明背景色填充
                    g.Clear(Color.Transparent);

                    //在指定位置并且按指定大小绘制原图片的指定部分
                    g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                        new Rectangle(x, y, ow, oh),
                        GraphicsUnit.Pixel);

                    try
                    {
                        //以jpg格式保存缩略图
                        /*
                        Bitmap a = new Bitmap(bitmap);
                        a.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        a.Dispose();
                         * */

                        Bitmap a = new Bitmap(bitmap);
                        MemoryStream ms = new MemoryStream();
                        a.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        /*
                        Bitmap a2 = new Bitmap(ms);
                        a2.Save(HttpContext.Current.Server.MapPath("/upload/s/a2.jpg"));

                         * */
                        imgData = ms.GetBuffer();
                        //new WebService1().SaveThumnail(imgData);
                        ms.Close();

                        /*
                        using (Stream stream = new MemoryStream())
                        { 
                            //克隆Bitmap对象  
                    Bitmap bmp = new Bitmap(bitmap);
                    bmp.Save(stream, bitmap.RawFormat);
                     bmp.Dispose();  
                        
                         * */
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                        //imgData = null;
                    }
                    finally
                    {
                        g.Dispose();
                        bitmap.Dispose();
                        originalImage.Dispose();
                    }
                    return imgData;
                }
            }
            #endregion
        }
        private static void AutoSize(int ow, int oh, ref int towidth, ref int toheight)
        {
            if (towidth < ow)
            {
                if (toheight < oh)
                {
                    float finalRate = 0f;
                    float rate1 = (float)toheight / (float)oh;
                    float rate2 = (float)towidth / (float)ow;
                    if (rate1 > rate2)
                    {
                        finalRate = rate2;
                    }
                    else
                        finalRate = rate1;
                    towidth = (int)((float)ow * finalRate);
                    toheight = (int)((float)oh * finalRate);

                }
                else
                {
                    float finalRate = (float)towidth / (float)ow;
                    towidth = (int)((float)ow * finalRate);
                    toheight = (int)((float)oh * finalRate);
                }
            }
            else
            {
                if (toheight < oh)
                {

                    float finalRate = (float)toheight / (float)oh;
                    towidth = (int)((float)ow * finalRate);
                    toheight = (int)((float)oh * finalRate);


                }
                else
                {
                    towidth = ow;
                    toheight = oh;

                }
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="ow"></param>
        /// <param name="oh"></param>
        /// <param name="towidth"></param>
        /// <param name="toheight"></param>
        private static void AutoRefMinSize(int ow, int oh, ref int towidth, ref int toheight)
        {
            if (ow < towidth && oh < toheight)
            {
                towidth = ow;
                toheight = oh;
            }
            else
            {
                if (ow > oh)
                {
                    towidth = ow * toheight / oh;

                }
                else
                {
                    toheight = oh * towidth / ow;
                }
            }
        }

        private static Size NewSize(int maxWidth, int maxHeight, int Width, int Height)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(Width);
            double sh = Convert.ToDouble(Height);
            double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight);

            if (sw < mw && sh < mh)//如果maxWidth和maxHeight大于源图像，则缩略图的长和高不变  
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = maxWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = maxHeight;
                w = (h * sw) / sh;
            }
            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Jazb.Web.Infrastructure
{
    public enum AllowUploadStatuse
    {
        UploadSuccessFully,
        NotDefaineExtension,
        DoNotHaveContentFile,
        HaveNotName,
        HaveNotAllowUpload
    }

    public class FilePicture
    {
        public byte[] ThumbnailImageFile { get; set; }
        public byte[] ImageFile { get; set; }
    }

    public class KRBUpload
    {

        protected string Extension = ".zip,.rar,.pdf,.doc,.docx,.xls,.xlsx,.ppt,.pptx,.jpg,.png,.gif";

        public FilePicture ConvertToArray(HttpPostedFileBase HPFB, int width, int height)
        {
            int w = width, h = height;
            FilePicture FP = new FilePicture();
            try
            {
                WebImage webImage = new WebImage(HPFB.InputStream);
                WebImage webThumbnailImageFile;

                FP.ImageFile = webImage.GetBytes();

                if (width == 0)
                    w = webImage.Width;
                if (height == 0)
                    h = webImage.Height;

                webThumbnailImageFile = webImage.Resize(w, h);
                FP.ThumbnailImageFile = webThumbnailImageFile.GetBytes();
            }
            catch (Exception ex)
            {
                byte[] data;
                using (Stream inputStream = HPFB.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();

                    FP.ImageFile = data;
                    FP.ThumbnailImageFile = data;
                }
            }
            

            return FP;


        }


        public AllowUploadStatuse AllowUploadSpecialFilesOnly(string extensionsWhiteList, HttpPostedFileBase postedFile)
        {


            if (postedFile == null || postedFile.ContentLength <= 0)
                return AllowUploadStatuse.DoNotHaveContentFile;

            List<string> _toFilter = new List<string>();
            string _extensionsWhiteList;
            if (string.IsNullOrWhiteSpace(extensionsWhiteList))
                return AllowUploadStatuse.NotDefaineExtension;

            _extensionsWhiteList = extensionsWhiteList;
            var extensions = extensionsWhiteList.Split(',');
            foreach (var ext in extensions.Where(ext => !string.IsNullOrWhiteSpace(ext)))
            {
                _toFilter.Add(ext.ToLowerInvariant().Trim());
            }

            if (postedFile == null || postedFile.ContentLength == 0) return AllowUploadStatuse.DoNotHaveContentFile;

            if (string.IsNullOrWhiteSpace(postedFile.FileName)) return AllowUploadStatuse.HaveNotName;
            var ext1 = Path.GetExtension(postedFile.FileName.ToLowerInvariant());

            if (!_toFilter.Contains(ext1))
                return AllowUploadStatuse.HaveNotAllowUpload;



            return AllowUploadStatuse.UploadSuccessFully;
        }

        public AllowUploadStatuse AllowUploadSpecialFilesOnly(HttpPostedFileBase postedFile)
        {
            if (postedFile == null || postedFile.ContentLength <= 0)
                return AllowUploadStatuse.DoNotHaveContentFile;

            List<string> _toFilter = new List<string>();
            string _extensionsWhiteList;
            if (string.IsNullOrWhiteSpace(Extension))
                return AllowUploadStatuse.NotDefaineExtension;

            _extensionsWhiteList = Extension;
            var extensions = Extension.Split(',');
            foreach (var ext in extensions.Where(ext => !string.IsNullOrWhiteSpace(ext)))
            {
                _toFilter.Add(ext.ToLowerInvariant().Trim());
            }

            if (postedFile == null || postedFile.ContentLength == 0) return AllowUploadStatuse.DoNotHaveContentFile;

            if (string.IsNullOrWhiteSpace(postedFile.FileName)) return AllowUploadStatuse.HaveNotName;
            var ext1 = Path.GetExtension(postedFile.FileName.ToLowerInvariant());

            if (!_toFilter.Contains(ext1))
                return AllowUploadStatuse.HaveNotAllowUpload;



            return AllowUploadStatuse.UploadSuccessFully;
        }

        public string GetErrorMessage(AllowUploadStatuse allowUploadStatuse)
        {
            string strMsg = "";
            switch (allowUploadStatuse)
            {
                case AllowUploadStatuse.DoNotHaveContentFile:
                    strMsg = "فایل انتخاب شده خالی می باشد";
                    break;
                case AllowUploadStatuse.HaveNotAllowUpload:
                    strMsg = "شما اجازه ارسال این فایل را ندارید";
                    break;
                case AllowUploadStatuse.HaveNotName:
                    strMsg = "فایل انتخابی شما اسم ندارد";
                    break;
                case AllowUploadStatuse.NotDefaineExtension:
                    strMsg = "فرمت فایل انتخابی شما برای ارسال غیرمجاز می باشد";
                    break;
                case AllowUploadStatuse.UploadSuccessFully:
                    strMsg = "فایل با موفقیت ارسال شد";
                    break;
            }

            return strMsg;
        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NTier.UI.Helpers
{
    public class ImageUploader
    {
        //Image işlemleri. ürün ve user imahları bu metot ile guid tipinde isimlendirilir
        //Hata kodları =>
        //0 - Dosya Boş/Yok
        //1 - Zaten bu isimde dosya bulunmakta.
        //2 - Dosya uzantısı belirtilenlere uygun değil
        //Bu durumlarda ekleme aşamasında "default" bir imaj kullanılıyor.
        //Yoksa "Guid+dosya uzantısı" ile yeni bir isim oluşturulup return ediliyor.

        public static string UploadSingleImage(string serverPath, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var uniqueName = Guid.NewGuid();
                serverPath = serverPath.Replace("~", string.Empty);

                var fileArray = file.FileName.Split('.');
                string extension = fileArray[fileArray.Length - 1].ToLower(); //extension => png
                var fileName = uniqueName + "." + extension;

                if (extension == "jpg" || extension == "png" || extension == "jpeg" || extension == "gif")
                {
                    if(File.Exists(HttpContext.Current.Server.MapPath(serverPath+fileName)))
                    {
                        return "1";
                    }
                    else
                    {
                        var filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;
                    }
                }
                else
                {
                    return "2";
                }
            }
            return "0";
        }
    }
}
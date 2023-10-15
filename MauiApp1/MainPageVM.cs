using Plugin.Media.Abstractions;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Services.ImageResize;

namespace MauiApp1
{
    public class MainPageVM:BindableBase
    {
        private string photoPath;
        private readonly IImageResize imageResize;

        public MainPageVM(IImageResize imageResize)
        {
            this.imageResize = imageResize;
        }

        private string serviceBaseURI = "http://192.168.0.30/WebApplication1/";
        
        private DelegateCommand takePhotoCommand;

        public DelegateCommand TakePhotoCommand
        {
            get
            {
                return takePhotoCommand ?? (takePhotoCommand = new DelegateCommand(async () =>
                {
                    var path = await TakePhotoAndReturnPath().ConfigureAwait(false);
                    PhotoPath = path;
                }));
            }
        }

        private DelegateCommand uploadPhotoCommand;

        public DelegateCommand UploadPhotoCommand
        {
            get
            {
                return uploadPhotoCommand ?? (uploadPhotoCommand = new DelegateCommand(async () =>
                {
                    var contentAsBytes = System.IO.File.ReadAllBytes(this.photoPath);
                    var contentAsPost = new MultipartFormDataContent();

                    contentAsPost.Add(new ByteArrayContent(contentAsBytes), "photoPost", System.IO.Path.GetFileName(this.photoPath));


                    var photoUri = System.IO.Path.Combine(serviceBaseURI, "Home", "UploadPhoto");
                    var client = new HttpClient();
                    await client.PostAsync(photoUri, contentAsPost);

                    System.IO.File.Delete(this.photoPath);

                }));
            }
        }

        

        public string PhotoPath
        {
            get { return photoPath; }
            set { SetProperty(ref photoPath, value); }
        }

        private async Task<string> TakePhotoAndReturnPath()
        {
           var mediaFile = await takePhotoAsync();

            if(mediaFile ==  null) { return string.Empty; }

            byte[] photoContent = null;

            using(var sourceSteam = mediaFile.GetStream())
            {
                using (var ms = new MemoryStream())
                {
                    sourceSteam.CopyTo(ms);
                    photoContent = ms.ToArray();
                }
            }


            photoContent = imageResize.ResizeImage(photoContent, 1180, 1536);

            var fileName = $"photo_file_name_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
            var fileExtension = System.IO.Path.GetExtension(mediaFile.Path);

            var cachedPath = System.IO.Path.Combine(FileSystem.CacheDirectory, $"{fileName}{fileExtension}");

            File.WriteAllBytes( cachedPath, photoContent);

            

            //using (var stream = mediaFile.GetStream())
            //{
            //    byte[] resizedBytes = null;

            //    using (var ms = new MemoryStream())
            //    {
            //        stream.CopyTo(ms);
            //        //resizedBytes = imageResizer.ResizeImage(ms.ToArray(), 1180, 1536);
            //    }

            //    File.WriteAllBytes(cachedPath, resizedBytes);
            //}

            return cachedPath;
        }

        private async Task<MediaFile>  takePhotoAsync()
        {
            await PermissionsUtilities.RequestPermissions();
            await CrossMedia.Current.Initialize();

            var options = new StoreCameraMediaOptions();
            options.CompressionQuality = 30;

            return  await CrossMedia.Current.TakePhotoAsync(options);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.ImageResize.Platform
{
    public partial class ImageResizeService : IImageResize
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            return ResizePlatformImage(imageData, width, height);
        }

        private partial byte[] ResizePlatformImage(byte[] imageData, float width, float height);
    }
}

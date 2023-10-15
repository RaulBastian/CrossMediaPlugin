using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.ImageResize
{
    public interface IImageResize
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChartsCreator
{
    public class BitmapCreator
    {
        public static BitmapSource CreateBitmap(IEnumerable<Pixel> pixels)
        {
            PixelFormat pf = PixelFormats.Rgb24;
            int width = (int)Math.Sqrt(pixels.Count());
            int height = (int)Math.Sqrt(pixels.Count());
            int rawStride = (width * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * height];

            for (int i = 0, j = 0; i < pixels.Count(); i++, j = j + 3)
            {
                rawImage[j] = pixels.ElementAt(i).PixelColor.R;
                rawImage[j + 1] = pixels.ElementAt(i).PixelColor.G;
                rawImage[j + 2] = pixels.ElementAt(i).PixelColor.B;
            }

            return BitmapSource.Create(width, height,
                 500, 500, pf, null,
                rawImage, rawStride);
        }
    }
}

#region File Description
//-----------------------------------------------------------------------------
// Author: JCBDigger
// URL: http://Games.DiscoverThat.co.uk
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Engine
{
    /// <summary>
    /// Help convert bitmaps and other images to and from XNA Textures.
    /// </summary>
    public static class ImageHelper
    {
        // Thanks to, Popescu Alexandru-Cristian.  See:
        // http://communistgames.blogspot.co.uk/2010/10/converting-between-texture2d-and-image.html?m=1
        /// <summary>
        /// To convert a Texture2D to an Image.
        /// </summary>
        public static System.Drawing.Image Texture2Image(Texture2D texture)
        {
            if (texture == null)
            {
                return null;
            }
            if (texture.IsDisposed)
            {
                return null;
            }
            //Memory stream to store the bitmap data.
            MemoryStream ms = new MemoryStream();
            //Save the texture to the stream. 
            texture.SaveAsPng(ms, texture.Width, texture.Height);
            //Seek the beginning of the stream.
            ms.Seek(0, SeekOrigin.Begin);
            //Create an image from a stream.
            System.Drawing.Image bmp2 = System.Drawing.Bitmap.FromStream(ms);
            //Close the stream, we nolonger need it.
            ms.Close();
            ms = null;
            return bmp2;
        }

        // Thanks to, Popescu Alexandru-Cristian.  Modified from:
        // http://communistgames.blogspot.co.uk/2010/10/converting-between-texture2d-and-image.html?m=1
        /// <summary>
        /// To convert an Image to a Texture2D.
        /// </summary>
        public static void Image2Texture(System.Drawing.Image image,
                                         GraphicsDevice graphics,
                                         out Texture2D texture)
        {
            if (image == null)
            {
                texture = null;
                return;
            }
            texture = new Texture2D(graphics, image.Width, image.Height, false, SurfaceFormat.Color);
            //Memory stream to store the bitmap data.
            MemoryStream ms = new MemoryStream();
            //Save to that memory stream.
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //Go to the beginning of the memory stream.
            ms.Seek(0, SeekOrigin.Begin);
            //Fill the texture.
            texture = Texture2D.FromStream(graphics, ms, image.Width, image.Height, false);
            //Close the stream.
            ms.Close();
            ms = null;
        }


    }

}

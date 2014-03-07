using Explicatio.Main;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Graphics.Textures
{
    public class Textures
    {
        ///// <summary>
        ///// On Load
        ///// </summary>
        //public void CreateTexture(out int texture, Bitmap bitmap)
        //{
        //    // load texture 
        //    GL.GenTextures(1, out texture);
 
        //    //Still required else TexImage2D will be applyed on the last bound texture
        //    GL.BindTexture(TextureTarget.Texture2D, texture);
 
        //    BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
        //    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
 
        //    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
        //    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
 
        //    bitmap.UnlockBits(data);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        //}
        ///// <summary>
        ///// Right after CreateTexture (on load)
        ///// </summary>

        //public void BindTexture(ref int textureId, TextureUnit textureUnit, string UniformName)
        //{
        //    GL.ActiveTexture(textureUnit);
        //    GL.BindTexture(TextureTarget.Texture2D, textureId);
        //    GL.Uniform1(GL.GetUniformLocation(GameMain.ShaderProgramHandle, UniformName), textureUnit - TextureUnit.Texture0);
        //}
    }
}

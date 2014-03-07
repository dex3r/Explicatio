using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using Explicatio.Main;
using Explicatio.Graphics.Shaders;

namespace Explicatio.Graphics
{
    public static class Renderer
    {
        private static Matrix4 projectionMatrix;
        private static Matrix4 modelMatrix;
        private static Matrix4 viewMatrix;

        private static Shader currentShader;

         //!? Properties region
        #region PROPERTIES
        public static Matrix4 ProjectionMatrix
        {
            get { return Renderer.projectionMatrix; }
            set 
            { 
                Renderer.projectionMatrix = value;
                currentShader.ProjectionMatrix = value;
            }
        }
        public static Matrix4 ModelMatrix
        {
            get { return Renderer.modelMatrix; }
            set 
            { 
                Renderer.modelMatrix = value;
                currentShader.ModelMatrix = value;
            }
        }
        public static Matrix4 ViewMatrix
        {
            get { return Renderer.viewMatrix; }
            set 
            { 
                Renderer.viewMatrix = value;
                currentShader.ViewMatrix = value;
            }
        }
        public static Shader CurrentShader
        {
            get { return Renderer.currentShader; }
        }
        #endregion
        //!? END of properties region

        public static void Init()
        {
            //float aspectRatio = Display.Instance.ClientSize.Width / (float)(Display.Instance.ClientSize.Height);
            ////float aspectRatio = 1920.0f / 1080.0f;
            //Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4f, aspectRatio, 1f, 100, out projectionMatrix);
            ////Matrix4.CreateOrthographic(100, 100, 0.1f, 10000, out projectionMatrix);
            ////projectionMatrix = Matrix4.Mult(Matrix4.LookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0)), projectionMatrix);
            ////modelMatrix = Matrix4.LookAt(new Vector3(3, 2, 5), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            ////modelMatrix = Matrix4.Mult(Matrix4.CreateRotationZ(MathHelper.PiOver4), modelMatrix);
            ////Matrix4.CreateOrthographicOffCenter(-10, 10, -10, 10, 0.1f, 1000, out projectionMatrix);
            ////modelMatrix = Matrix4.Identity;
            ////viewMatrix = Matrix4.LookAt(new Vector3(0, 0, 5), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            //viewMatrix = Matrix4.Identity;
            //modelMatrix = Matrix4.Identity;
            ////modelviewMatrix = Matrix4.CreateTranslation(0, 0, -50);

            float aspectRatio = Display.Instance.ClientSize.Width / (float)(Display.Instance.ClientSize.Height);
            Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4f, aspectRatio, 0.001f, 100, out projectionMatrix);
            viewMatrix = Matrix4.LookAt(new Vector3(0, 0, 30), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            modelMatrix = Matrix4.Identity;

            ChangeCurrentShader(Shader.SimpleColor, true);
        }

        /// <summary>
        /// Zmiena aktualnie używany shader na nowy, jeżeli podano inny niż aktualny
        /// </summary>
        /// <param name="newShader"></param>
        public static void ChangeCurrentShader(Shader newShader, bool changePMVToCurrent)
        {
            if(currentShader != newShader)
            {
                currentShader = newShader;
                if (changePMVToCurrent)
                {
                    newShader.SetPMVAndUpdate(projectionMatrix, modelMatrix, viewMatrix);
                }
                currentShader.Use();
            }
        }
    }
}

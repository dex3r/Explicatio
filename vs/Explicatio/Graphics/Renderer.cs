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

        private static int projectionMatrixHandle;
        private static int modelMatrixHandle;
        private static int viewMatrixHandle;

        private static Shader currentShader;

         //!? Properties region
        #region PROPERTIES
        public static Matrix4 ProjectionMatrix
        {
            get { return Renderer.projectionMatrix; }
            set { Renderer.projectionMatrix = value; }
        }
        public static Matrix4 ModelMatrix
        {
            get { return Renderer.modelMatrix; }
            set { Renderer.modelMatrix = value; }
        }
        public static Matrix4 ViewMatrix
        {
            get { return Renderer.viewMatrix; }
            set { Renderer.viewMatrix = value; }
        }
        public static int ProjectionMatrixHandle
        {
            get { return Renderer.projectionMatrixHandle; }
        }
        public static int ModelMatrixHandle
        {
            get { return Renderer.modelMatrixHandle; }
        }
        public static int ViewMatrixHandle
        {
            get { return Renderer.viewMatrixHandle; }
        }
        public static Shader CurrentShader
        {
            get { return Renderer.currentShader; }
        }
        #endregion
        //!? END of properties region

        public static void Init()
        {
            //? TODO: dodać pobieranie rączek z shaderów prze pomocy UBO

            //float aspectRatio = Display.Instance.ClientSize.Width / (float)(Display.Instance.ClientSize.Height);
            //float aspectRatio = 1920.0f / 1080.0f;
            //Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4f, aspectRatio, 1f, 100, out projectionMatrix);
            //Matrix4.CreateOrthographic(5, 5, 0.1f, 10000, out projectionMatrix);
            //projectionMatrix = Matrix4.Mult(Matrix4.LookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0)), projectionMatrix);
            //modelviewMatrix = Matrix4.LookAt(new Vector3(3, 2, 5), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            Matrix4.CreateOrthographicOffCenter(-10, 10, -10, 10, 0.1f, 1000, out projectionMatrix);
            modelMatrix = Matrix4.Identity;
            viewMatrix = Matrix4.LookAt(new Vector3(3, 2, 5), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            //modelviewMatrix = Matrix4.Identity;
            //modelviewMatrix = Matrix4.CreateTranslation(0, 0, -50);

            GL.UniformMatrix4(projectionMatrixHandle, false, ref projectionMatrix);
            GL.UniformMatrix4(modelMatrixHandle, false, ref modelMatrix);
            //GL.UniformMatrix4(viewMatrixHandle, false, ref viewnMatrix);
        }

        /// <summary>
        /// Zmiena aktualnie używany shader na nowy, jeżeli podano inny niż aktualny
        /// </summary>
        /// <param name="newShader"></param>
        public static void ChangeCurrentShader(Shader newShader)
        {
            if(currentShader != newShader)
            {
                currentShader = newShader;
                currentShader.Use();
            }
        }
    }
}

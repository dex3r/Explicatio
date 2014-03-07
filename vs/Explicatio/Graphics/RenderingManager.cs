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
    public static class RenderingManager
    {
        private static Matrix4 projectionMatrix;
        private static Matrix4 modelMatrix;

        private static Shader currentShader;

         //!? Properties region
        #region PROPERTIES
        public static Matrix4 ProjectionMatrix
        {
            get { return RenderingManager.projectionMatrix; }
            set 
            { 
                RenderingManager.projectionMatrix = value;
                currentShader.ProjectionMatrix = value;
            }
        }
        public static Matrix4 ModelMatrix
        {
            get { return RenderingManager.modelMatrix; }
            set 
            { 
                RenderingManager.modelMatrix = value;
                currentShader.ModelMatrix = value;
            }
        }
        public static Shader CurrentShader
        {
            get { return RenderingManager.currentShader; }
        }
        #endregion
        //!? END of properties region

        public static void Init()
        {
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
                    newShader.SetPMAndUpdate(projectionMatrix, modelMatrix);
                }
                currentShader.Use();
            }
        }

        public static void UpdateMatrices()
        {
            currentShader.ProjectionMatrix = projectionMatrix;
            currentShader.ModelMatrix = modelMatrix;
        }
    }
}

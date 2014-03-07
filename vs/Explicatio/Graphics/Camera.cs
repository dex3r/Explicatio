using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Explicatio.Graphics
{
    public static class Camera
    {
        private static float zoom = 1.0f;
        private static float posX = 0;
        private static float posY = 0;

         //!? Properties region
        #region PROPERTIES
        public static float Zoom
        {
            get { return Camera.zoom; }
            set { Camera.zoom = value; }
        }
        public static float PosX
        {
            get { return Camera.posX; }
            set { Camera.posX = value; }
        }
        public static float PosY
        {
            get { return Camera.posY; }
            set { Camera.posY = value; }
        }
        #endregion
        //!? END of properties region

        public static void Update()
        {
            float width = (((float)Display.Instance.ClientSize.Width / 20f) / zoom) / 2;
            float height = (((float)Display.Instance.ClientSize.Height / 20f) / zoom) / 2;
            RenderingManager.ProjectionMatrix =  Matrix4.CreateOrthographicOffCenter(-width - posX, width - posX, -height - posY, height - posY, 0.1f, 10000);
            RenderingManager.ProjectionMatrix = Matrix4.Mult(RenderingManager.ProjectionMatrix, Matrix4.CreateTranslation(0f, 0f, 1f));
            RenderingManager.UpdateMatrices();
        }
    }
}

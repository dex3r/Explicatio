using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexGUI
{
    public class UpdateState
    {
        public float MouseX { get; private set; }
        public float MouseY { get; private set; }
        public bool IsLMBPressed { get; private set; }
        public bool IsRMBPressed { get; private set; }
        public string TypedText { get; private set; }

        public UpdateState(float mouseX, float mouseY, bool isLMPPressed, bool isRMPPressed, string typedText)
        {
            this.MouseX = mouseX;
            this.MouseY = mouseY;
            this.IsLMBPressed = isLMPPressed;
            this.IsRMBPressed = isRMPPressed;
            this.TypedText = typedText;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Explicatio.Controls
{
    public delegate void ButtonChangeState(MyKey key);

    public class MyKey
    {
        public string Name { get; private set; }
        private bool isPressed = false;
        public bool IsPressed
        {
            get
            {
                return isPressed;
            }
        }
        public bool WasPressed { get; set; }
        public ButtonChangeState ButtonDownEvent { get; set; }
        public ButtonChangeState ButtonUpEvent { get; set; }
        public ButtonChangeState ButtonToggleOnEvent { get; set; }
        public ButtonChangeState ButtonToggleOffEvent { get; set; }

        public List<Keys> registeredKeys { get; private set; }

        public MyKey(string keyName)
        {
            MyKeyboard.AllKeys.Add(this);
            this.Name = keyName;
            registeredKeys = new List<Keys>(1);
        }

        public MyKey(string keyName, Keys key)
            : this(keyName)
        {
            registeredKeys.Add(key);
        }

        public MyKey(string keyName, List<Keys> keys)
            : this(keyName)
        {
            registeredKeys = keys;
        }

        public void Update(KeyboardState kstate)
        {
            WasPressed = IsPressed;
            isPressed = false;
            for (int i = 0; i < registeredKeys.Count; i++)
            {
                if (kstate.IsKeyDown(registeredKeys[i]))
                {
                    isPressed = true;
                    break;
                }
            }
            if (isPressed)
            {
                if (ButtonDownEvent != null)
                {
                    ButtonDownEvent.Invoke(this);
                }
            }
            else if (!isPressed)
            {
                if (ButtonUpEvent != null)
                {
                    ButtonUpEvent.Invoke(this);
                }
            }
           if(!isPressed && WasPressed)
           {
               if(ButtonToggleOffEvent != null)
               {
                   ButtonToggleOffEvent.Invoke(this);
               }
           }
           if(isPressed && !WasPressed)
           {
               if(ButtonToggleOnEvent != null)
               {
                   ButtonToggleOnEvent.Invoke(this);
               }
           }
        }

        public bool RegisterKey(Keys key)
        {
            if (!registeredKeys.Contains(key))
            {
                return false;
            }
            registeredKeys.Add(key);
            return true;
        }

        public bool UnregisterKey(Keys key)
        {
            return registeredKeys.Remove(key);
        }
    }
}

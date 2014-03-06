using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicatio.Controls
{
    public class MyKey
    {
        private string name;
        private bool isPressed = false;
        private bool wasPressed;
        private bool isToggled;
        private int toggleRepeatRate;
        private int currentToggleTicks;
        private List<Key> registeredKeys;
        private List<MouseButton> registeredMouseButtons;

        //!? Properties region
        #region PROPERTIES
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool IsPressed
        {
            get { return isPressed; }
        }
        public bool WasPressed
        {
            get { return wasPressed; }
        }
        public bool IsToggled
        {
            get { return isToggled; }
        }
        /// <summary>
        /// Z jaką częstotliwością (ticks) ma zmieniaś się stan IsToggled dla danego przycisku
        /// -1 (domyślne) dla przełączenia jednorazowego
        /// </summary>
        public int ToggleRepeatRate
        {
            get { return toggleRepeatRate; }
        }
        public List<Key> RegisteredKeys
        {
            get { return registeredKeys; }
            set { registeredKeys = value; }
        }
        public List<MouseButton> RegisteredMouseButtons
        {
            get { return registeredMouseButtons; }
            set { registeredMouseButtons = value; }
        }
        #endregion
        //!? END of properties region

        public MyKey(string keyName)
        {
            MyKeyboard.AllKeys.Add(this);
            this.Name = keyName;
            registeredKeys = new List<Key>(1);
            registeredMouseButtons = new List<MouseButton>(0);
            toggleRepeatRate = -1;
        }

        public MyKey(string keyName, Key key)
            : this(keyName)
        {
            registeredKeys.Add(key);
        }

        public MyKey(string keyName, MouseButton mouseButton)
            : this(keyName)
        {
            registeredMouseButtons.Add(mouseButton);
        }

        public MyKey(string keyName, List<Key> keys)
            : this(keyName)
        {
            registeredKeys = keys;
        }

        public MyKey SetRepeatRate(int repeatRate)
        {
            this.toggleRepeatRate = repeatRate;
            return this;
        }

        public void Update(KeyboardState kstate)
        {
            wasPressed = IsPressed;
            isPressed = false;
            for (int i = 0; i < registeredKeys.Count; i++)
            {
                if (kstate.IsKeyDown(registeredKeys[i]))
                {
                    isPressed = true;
                    break;
                }
            }
            if (!isPressed)
            {
                foreach (MouseButton mouseButton in registeredMouseButtons)
                {
                    if (Mouse.GetState().IsButtonDown(mouseButton))
                    {
                        isPressed = true;
                        break;
                    }
                }
            }
            if (toggleRepeatRate == -1)
            {
                isToggled = IsPressed && !wasPressed;
            }
            else
            {
                isToggled = false;
                if (currentToggleTicks > 0)
                {
                    currentToggleTicks++;
                }
                if (currentToggleTicks >= toggleRepeatRate)
                {
                    currentToggleTicks = 0;
                }
                if (IsPressed && currentToggleTicks == 0)
                {
                    isToggled = true;
                    currentToggleTicks++;
                }
            }
        }

        public MyKey RegisterKey(Key key)
        {
            if (!registeredKeys.Contains(key))
            {
                registeredKeys.Add(key);
            }
            return this;
        }

        public MyKey RegisterMouseButton(MouseButton mouseButton)
        {
            if (!registeredMouseButtons.Contains(mouseButton))
            {
                registeredMouseButtons.Add(mouseButton);
            }
            return this;
        }

        public bool UnregisterKey(Key key)
        {
            return registeredKeys.Remove(key);
        }

        public bool UnregisterMouseButton(MouseButton mouseButton)
        {
            return registeredMouseButtons.Remove(mouseButton);
        }
    }
}


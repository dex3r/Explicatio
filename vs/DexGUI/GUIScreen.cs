using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexGUI
{
    public class GUIScreen : IComparable<GUIScreen>
    {
        public bool isVisible { get; set; }
        private int drawOrder;
        private List<GUIComponent> components;
        private List<GUIComponent> componentsToDelete;
        private List<GUIComponent> componentsToAdd;
        private EState currentState;

         //!? Properties region
        #region PROPERTIES
        public int DrawOrder
        {
            get { return drawOrder; }
            set
            {
                drawOrder = value;
                GUIManager.Screens.Sort();
            }
        }
        internal List<GUIComponent> Components
        {
            get { return components; }
        }
        #endregion
        //!? END of properties region

        public GUIScreen()
        {
            components = new List<GUIComponent>();
            componentsToDelete = new List<GUIComponent>();
            componentsToAdd = new List<GUIComponent>();
        }

        public int CompareTo(GUIScreen other)
        {
            return DrawOrder - other.DrawOrder;
        }

        public virtual void Update()
        {
            if(currentState != EState.Idle)
            {
                throw new Exception("Can't Update during concurent draw or update of the same GUIScreen");
            }
            currentState = EState.Updating;
            for(int i = 0; i < components.Count; i++)
            {
                components[i].Update();
            }
            currentState = EState.Idle;
            checkNewComponents();
        }

        public virtual void Draw()
        {
            if (currentState != EState.Idle)
            {
                throw new Exception("Can't Draw during concurent draw or update of the same GUIScreen");
            }
            currentState = EState.Drawing;
            if(!isVisible)
            {
                return;
            }
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw();
            }
            currentState = EState.Idle;
            checkNewComponents();
        }

        private void checkNewComponents()
        {
            if(currentState != EState.Idle)
            {
                throw new Exception("Checking for new components can only occur after Draw or after Update");
            }
            if(componentsToDelete.Count > 0)
            {
                for(int i = 0; i < componentsToDelete.Count; i++)
                {
                    RemoveComponent(componentsToDelete[i]);
                }
                componentsToDelete.Clear();
            }
            if(componentsToAdd.Count > 0)
            {
                for(int i = 0; i < componentsToAdd.Count; i++)
                {
                    AddComponent(componentsToAdd[i]);
                }
                componentsToAdd.Clear();
            }
        }

        public virtual void AddComponent(GUIComponent component)
        {
            if(component.Owner != null && component.Owner != this)
            {
                throw new Exception("GUIComponent can have only one owner at a time");
            }
            if(currentState == EState.Idle)
            {
                components.Add(component);
                component.Owner = this;
                components.Sort();
            }
            else
            {
                componentsToAdd.Add(component);
            }
        }

        public virtual void RemoveComponent(GUIComponent component)
        {
            if(currentState == EState.Idle)
            {
                if(components.Remove(component))
                {
                    component.Owner = null;
                }
            }
            else
            {
                componentsToDelete.Add(component);
            }
        }
    }
}

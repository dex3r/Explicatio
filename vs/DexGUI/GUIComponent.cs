using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;

namespace DexGUI
{
    public class GUIComponent : IComparable<GUIComponent>
    {
        public bool IsVisible { get; private set; }
        private RectangleF Position { get; set; }
        public RectangleF PrevPosition { get; private set; }
        public bool IsDirty { get; protected set; }

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
                if(value != drawOrder)
                {
                    IsDirty = true;
                }
                drawOrder = value;
            }
        }
        #endregion
        //!? END of properties region

        public GUIComponent()
        {
            components = new List<GUIComponent>();
            componentsToDelete = new List<GUIComponent>();
            componentsToAdd = new List<GUIComponent>();
            Position = new RectangleF();
            PrevPosition = new RectangleF();
        }

        public int CompareTo(GUIComponent other)
        {
            return DrawOrder - other.DrawOrder;
        }

        public virtual void Update(UpdateState updateState)
        {
            if (currentState != EState.Idle)
            {
                throw new Exception("Can't Update during concurrent draw or update of the same GUIScreen");
            }
            try
            {
                checkNewComponents();
                currentState = EState.Updating;
                for (int i = 0; i < components.Count; i++)
                {
                    components[i].Update(updateState);
                }
            }
            finally
            {
                currentState = EState.Idle;
            }
        }

        public virtual void PreDraw()
        {

        }

        public virtual void ActualDraw()
        {

        }

        public virtual void PostDraw()
        {

        }

        public void Draw()
        {
            if (currentState != EState.Idle)
            {
                throw new Exception("Can't Draw during concurrent draw or update of the same GUIScreen");
            }
            try
            {
                currentState = EState.Drawing;
                if (!IsVisible)
                {
                    return;
                }
                for (int i = 0; i < components.Count; i++)
                {
                    components[i].Draw();
                }
            }
            finally
            {
                currentState = EState.Idle;
            }

            try
            {
                PreDraw();
                ActualDraw();
                PostDraw();
                IsDirty = false;
            }
            catch(Exception ex)
            {
                //Utils.HandleException(ex);
            }
            finally
            {

            }
        }

        private void checkNewComponents()
        {
            if (currentState != EState.Idle)
            {
                throw new Exception("Checking for new components can only occur after Draw or after Update");
            }
            if (componentsToDelete.Count > 0)
            {
                for (int i = 0; i < componentsToDelete.Count; i++)
                {
                    RemoveComponent(componentsToDelete[i]);
                }
                componentsToDelete.Clear();
            }
            if (componentsToAdd.Count > 0)
            {
                for (int i = 0; i < componentsToAdd.Count; i++)
                {
                    AddComponent(componentsToAdd[i]);
                }
                componentsToAdd.Clear();
            }
        }

        public virtual void AddComponent(GUIComponent component)
        {
            if (currentState == EState.Idle)
            {
                components.Add(component);
                components.Sort();
            }
            else
            {
                componentsToAdd.Add(component);
            }
        }

        /// <summary>
        /// Remove subcomponent.
        /// Note that if Update/Draw is in progress this is not done immediately but before the next Update.
        /// </summary>
        /// <param name="component">GUIComponent to delete</param>
        public virtual void RemoveComponent(GUIComponent component)
        {
            if (currentState == EState.Idle)
            {
                components.Remove(component);
            }
            else
            {
                componentsToDelete.Add(component);
            }
        }
    }
}

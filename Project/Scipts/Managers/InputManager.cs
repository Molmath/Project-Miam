using Godot;
using System;

// Author : Mathys Moles

namespace Com.IsartDigital.Manager
{

	public partial class InputManager : Node2D
	{
        [Signal] public delegate void EnterClickEventHandler();
        [Signal] public delegate void ExitClickEventHandler();

        #region Singleton
        static private InputManager instance;

		private InputManager() { }

		static public InputManager GetInstance()
		{
			if (instance == null) instance = new InputManager();
			return instance;
		}
        #endregion

        public override void _EnterTree()
        {
            base._EnterTree();
            #region Singleton Ready
            if (instance != null)
            {
                QueueFree();
                GD.Print(nameof(InputManager) + " Instance already exist, destroying the last added.");
                return;
            }

            instance = this;
            #endregion
            GD.Print("Input");
        }
        public override void _Input(InputEvent @event)
        {
            base._Input(@event);
            if(@event is InputEventScreenTouch lTouch)
            {
                if(lTouch.Index == Prop.ZERO)
                {
                    if (lTouch.Pressed) EmitSignal(nameof(EnterClick));
                    else if (!lTouch.Pressed) EmitSignal(nameof(ExitClick));
                }
            }
        }
		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}

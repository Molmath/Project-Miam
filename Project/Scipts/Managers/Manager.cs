using Godot;

// Author : Mathys Moles

namespace Com.IsartDigital.Manager {
	
	public partial class Manager : Node2D
	{
        [Export] bool autoActive;

        /// <summary>. <br/> Don't use Ready for Manager !!! <br/> .<br/> use Init for correct Comportement </summary>
        public override void _Ready()
        {
            base._Ready();

            if (!autoActive) SetProcess(false);
            else Init();
        }
        public virtual void Init()
		{
            SetProcess(true);
        }
		
	}
}

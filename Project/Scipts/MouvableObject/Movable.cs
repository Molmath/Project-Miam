using Godot;
using System;

// Author : Mathys Moles

namespace Com.IsartDigital.Miam.Movable
{
	
	public partial class Movable : Node2D
	{
		[Export] public float speed = 10f;
        public float initSpeed;
        protected bool onMove;

		private Vector2 _velocity;
		public Vector2 Velocity
		{
			get { return _velocity; }
			set 
			{
				if (value == Vector2.Zero)
				{
					onMove = false;
                    processAction -= Move;
                }
			    else if(!onMove)
				{
					onMove = true;
                    processAction += Move;
                }
                _velocity = value;
				SetVelocity();
            }
		}

		protected Action<float> processAction;

        public override void _Ready()
        {
            base._Ready();
			initSpeed = speed;
        }
        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			processAction?.Invoke(lDelta);
        }
		protected virtual void Move(float pDelta)
		{
			Position += Velocity * speed * pDelta;
		}
		protected virtual void SetVelocity() { }
	}
}

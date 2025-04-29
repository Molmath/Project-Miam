using Com.MathysMoles.Extension;
using Godot;
using System;

// Author : Mathys Moles

namespace Com.IsartDigital.Miam.Movable {
	
	public partial class Card : TargetMover
	{
		[Export] protected PerspectiveSprite2D renderer;
        protected override void SetTarget()
        {
            base.SetTarget();
            renderer.lookPoint = Target;
        }
        public override void _Process(double pDelta)
        {
            base._Process(pDelta);
           // GD.Print(speed);
        }
    }
}

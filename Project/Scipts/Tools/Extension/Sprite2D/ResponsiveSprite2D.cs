using Godot;
using System;
using System.Drawing;

// Author : Mathys Moles

namespace Com.MathysMoles.Extension
{
    public partial class ResponsiveSprite2D : Sprite2D
    {
        private Vector2 baseResolution = new Vector2(1920,1080);
        public override void _Ready()
        {
            Resize();
            GetViewport().SizeChanged += Resize;
        }
        private void Resize()
        {
            Vector2 screenSize = GetWindow().GetVisibleRect().Size;
            Scale = screenSize / baseResolution;
        }
    }
}

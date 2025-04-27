using Godot;
using System;
using System.Drawing;

// Author : Mathys Moles

namespace Com.IsartDigital.ProjectName
{
    public partial class ResponsiveSprite2D : Sprite2D
    {
        private Vector2 baseResolution;
        public override void _Ready()
        {
            baseResolution = GetWindow().Size;
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

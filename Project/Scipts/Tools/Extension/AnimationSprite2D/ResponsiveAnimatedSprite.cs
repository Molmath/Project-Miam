using Godot;
using System;
using System.Drawing;

// Author : Mathys Moles

namespace Com.IsartDigital.ProjectName {
	
	public partial class ResponsiveAnimatedSprite : AnimatedSprite2D
	{
        private Vector2 baseResolution = new Vector2(1920, 1080); 

        public override void _Ready()
        {
            Resize(); 
            GetWindow().SizeChanged += OnWindowSizeChanged;
        }

        private void OnWindowSizeChanged()
        {
            Resize();
        }

        private void Resize()
        {
            Vector2 screenSize = GetViewport().GetVisibleRect().Size;
            Scale = screenSize / baseResolution;
            GD.Print("Screen size : ", screenSize, ", Scale : ", Scale);
        }
    }
}

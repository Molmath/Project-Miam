using Godot;
using System;

// Author : Mathys Moles

namespace Com.IsartDigital.ProjectName
{

    public partial class PerspectiveSprite2D : ResponsiveSprite2D
    {
        private const string SHADER_PATH = "res://Scipts/Tools/Shaders/FackPerspective.gdshader";
        private const string SHADER_PARAM_X = "x_rot";
        private const string SHADER_PARAM_Y = "y_rot";

        [Export]private float distanceImpact = 30;
        [Export]private float maxDistanceIntensity = 100f;

        private ShaderMaterial shaderMaterial = new();
        private Vector2 currentObjectif;

        private Vector2 _direction;
        public Vector2 Direction
        {
            get { return _direction; }
            set
            {
                float lDistance =  value.DistanceTo(Position);
                float lIntensity =  lDistance / distanceImpact;
                _direction = value.Normalized() * Math.Clamp(lIntensity,0,maxDistanceIntensity);
                GD.Print(Math.Clamp(lIntensity, 0, maxDistanceIntensity));
                shaderMaterial.SetShaderParameter(SHADER_PARAM_X, _direction.X);
                shaderMaterial.SetShaderParameter(SHADER_PARAM_Y, _direction.Y);
            }
        }

        private Vector2 _perspective;
        public Vector2 Perspective
        {
            get{ return _perspective; }
            set 
            {
                shaderMaterial.SetShaderParameter(SHADER_PARAM_X, value.X);
                shaderMaterial.SetShaderParameter(SHADER_PARAM_Y, value.Y);
                _perspective = value;
            }
        }
        public override void _Ready()
        {
            base._Ready();
            shaderMaterial.Shader = GD.Load<Shader>(SHADER_PATH);
            Material = shaderMaterial;
        }
    }
}

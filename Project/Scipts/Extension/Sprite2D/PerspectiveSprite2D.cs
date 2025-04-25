using Godot;
using System;

// Author : Mathys Moles

namespace Com.IsartDigital.ProjectName
{

    public partial class PerspectiveSprite2D : Sprite2D
    {
        private ShaderMaterial shaderMaterial = new();
        private const string SHADER_PATH = "res://Scipts/Shaders/FackPerspective.gdshader";
        private const string SHADER_PARAM_X = "x_rot";
        private const string SHADER_PARAM_Y = "y_rot";
        private Vector2 _direction;
        public Vector2 Direction
        {
            get { return _direction; }
            set
            {
                GD.Print(value);
                _direction = value * 25f;
                shaderMaterial.SetShaderParameter(SHADER_PARAM_X, _direction.X);
                shaderMaterial.SetShaderParameter(SHADER_PARAM_Y, _direction.Y);
            }
        }
        public override void _Ready()
        {
            shaderMaterial.Shader = GD.Load<Shader>(SHADER_PATH);
            Material = shaderMaterial;
        }

        public override void _Process(double pDelta)
        {
            float lDelta = (float)pDelta;
            Direction = GetLocalMousePosition().Normalized();
        }

        protected override void Dispose(bool pDisposing)
        {

        }
    }
}

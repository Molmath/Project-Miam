using Com.IsartDigital.ProjectName;
using Godot;
using System;

// Author : Mathys Moles

namespace Com.MathysMoles.Extension
{

    public partial class PerspectiveSprite2D : ResponsiveSprite2D
    {
        private const string SHADER_PATH = "res://Scipts/Tools/Shaders/FackPerspective.gdshader";
        private const string SHADER_PARAM_X = "x_rot";
        private const string SHADER_PARAM_Y = "y_rot";

        [Export] private float distanceImpact = 30;
        [Export] private float maxDistanceIntensity = 100f;
        [Export] private float marge = 100f;

        Action processAction;

        private ShaderMaterial shaderMaterial = new();
        private Vector2 currentObjectif;

        public Vector2 lookPoint;

        private Vector2 _direction;
        public Vector2 Direction
        {
            get { return _direction; }
            set
            {
                Vector2 lDirection = value - GlobalPosition;

                float lDistance = value.DistanceTo(GlobalPosition);
                float lIntensity = lDistance / distanceImpact;

                _direction = lDirection.Normalized() * Math.Clamp(lIntensity, 0, maxDistanceIntensity);
                Perspective = _direction;
            }
        }

        private Vector2 _perspective;
        public Vector2 Perspective
        {
            get { return _perspective; }
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
            processAction += UpdateLookPoint;
            shaderMaterial.Shader = GD.Load<Shader>(SHADER_PATH);
            Material = shaderMaterial;
        }
        public override void _Process(double delta)
        {
            base._Process(delta);
            processAction?.Invoke();
        }
        private void UpdateLookPoint()
        {
            Direction = lookPoint;
        }
    }
}

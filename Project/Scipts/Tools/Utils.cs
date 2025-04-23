using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Godot.OpenXRInterface;
namespace Com.IsartDigital.ProjectName
{
   
    public static partial class Utils
    {
        public static float LookPosition(Vector2 pStartPosition, Vector2 pEndPosition)
        {
            Vector2 lDistancePoint = pEndPosition - pStartPosition;
            return Mathf.Atan2(lDistancePoint.Y, lDistancePoint.X);
        }
        public static Vector2 MoveWithRotation(float pRotation, float pSpeed)
        {
            return new Vector2(Mathf.Cos(pRotation), Mathf.Sin(pRotation)) * pSpeed;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OverratedEngine.Util
{
    public static class OverratedHelper
    {
        /// <summary>
        /// Returns the angle between two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>Angle between the vectors (in radains)</returns>
        public static float AngleBetweenVectors2D(Vector2 v1, Vector2 v2)
        {
            float angle;
            v1.Normalize();
            v2.Normalize();

            angle = (float)Math.Acos(Vector2.Dot(v1, v2));
            // If the angle is small enought, it becames 0
            if (Math.Abs(angle) < 0.0001)
                return 0;
            angle *= (v1.Y * v2.X - v2.Y * v1.X) > 0 ? 1 : -1;

            return angle;
        }
    }
}

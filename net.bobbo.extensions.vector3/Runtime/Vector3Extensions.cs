using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet.Extensions
{
    public static class Vector3Extensions
    {
        // Add or subtract from vector by amount so that it decays towards zero
        public static Vector3 Decay(this Vector3 vector, float amount)
        {
            return GoTowards(vector, Vector3.zero, amount);
        }

        // Add or subtract from some Vector3 with the goal of getting to value. Speed controls the maximum amount that it can add or subtract
        public static Vector3 GoTowards(this Vector3 vector, Vector3 value, float speed)
        {
            return Vector3.MoveTowards(vector, value, speed);
        }

        public static Vector3 GoTowardsSmooth(this Vector3 vector, Vector3 value, float speed)
        {
            return Vector3.Lerp(vector, value, speed);
        }
    }
}
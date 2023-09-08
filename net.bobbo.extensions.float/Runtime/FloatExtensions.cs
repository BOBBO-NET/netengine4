using System.Collections;
using System.Collections.Generic;

namespace BobboNet.Extensions
{
    public static class FloatExtensions
    {
        // Add or subtract from someFloat by amount so that it decays towards zero
        public static float Decay(this float someFloat, float amount)
        {
            return someFloat.GoTowards(0, amount);
        }

        // Add or subtract from some float with the goal of getting to value. Speed controls the maximum amount that it can add or subtract
        public static float GoTowards(this float someFloat, float value, float speed)
        {
            // Base case
            if (someFloat == value)
            {
                return value;
            }

            // If we're dealing with a positive delta...
            if (someFloat > value)
            {
                // If the delta will still be positive after moving...
                if (someFloat - speed > value)
                {
                    return someFloat - speed;
                }
                // Otherwise, the delta will wrap around to being negative, so move to the value
                else
                {
                    return value;
                }
            }
            // Otherwise we're dealing with a negative delta, so...
            else
            {
                // If the delta will still be negative after moving...
                if (someFloat + speed < value)
                {
                    return someFloat + speed;
                }
                // Otherwise, the delta will wrap around to being positive, so move to the value
                else
                {
                    return value;
                }
            }
        }
    }
}
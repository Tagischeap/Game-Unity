using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsMath : MonoBehaviour
{
    // If the return value is positive, then rotate to the left. Else,
    // rotate to the right.
    float ShortestRotation(float from, float to)
    {
        // If from or to is a negative, we have to recalculate them.
        // For an example, if from = -45 then from(-45) + 360 = 315.
        if(from < 0) {
            from += 360;
        }
        
        if(to < 0) {
            to += 360;
        }
    
        // Do not rotate if from == to.
        if(from == to ||
            from == 0  && to == 360 ||
            from == 360 && to == 0)
        {
            return 0;
        }
        
        // Pre-calculate left and right.
        float left = (360 - from) + to;
        float right = from - to;
        // If from < to, re-calculate left and right.
        if(from < to)  {
            if(to > 0) {
                left = to - from;
                right = (360 - to) + from;
            } else {
                left = (360 - to) + from;
                right = to - from;
            }
        }
    
        // Determine the shortest direction.
        return ((left <= right) ? left : (right * -1));
    }
    
    // Call CalcShortestRot and check its return value.
    // If CalcShortestRot returns a positive value, then this function
    // will return true for left. Else, false for right.
    bool ShortestRotationDirection(float from, float to)
    {
        // If the value is positive, return true (left).
        if(ShortestRotation(from, to) >= 0) {
            return true;
        }
        return false; // right
    }
}

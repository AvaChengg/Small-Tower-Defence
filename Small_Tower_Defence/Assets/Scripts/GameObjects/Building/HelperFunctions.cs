using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    // test vidiblity between two points
    public static bool TestVisibility(Vector3 start, Vector3 end, float maxDistance,Vector3 viewDirection, float viewHalfAngle, LayerMask occlusionMask)
    {
        // check distance to target
        if (Vector3.Distance(start, end) > maxDistance) return false;

        // check view angle  to target
        Vector3 targetDirection = (end - start).normalized;
        float targetAngle = Vector3.Angle(targetDirection, viewDirection);
        if (targetAngle > viewHalfAngle) return false;

        // linecast to test for occlusion, if something is hit, position isn't visible
        if(Physics.Linecast(start, end, out RaycastHit hit, occlusionMask))
        {
            Debug.DrawLine(start, hit.point, Color.red);
            return false;
        }

        // if all tests succeed, assume we can see end point
        Debug.DrawLine(start, end, Color.green);
        return true;
    }
}

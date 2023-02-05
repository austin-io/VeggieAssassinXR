using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    #region RAYS
    
    public static bool RayPlaneIntersection(Vector3 planePosition, Vector3 planeNormal, Ray ray, out float t, out Vector3 hitPoint){
        t = 0;
        hitPoint = ray.origin;
        float denom = Vector3.Dot(planeNormal, ray.direction);
        
        if(Mathf.Abs(denom) > 0.00001f){
            t = Vector3.Dot((planePosition - ray.origin), planeNormal) / denom;
            hitPoint = ray.origin + (ray.direction * t);
            return true;
        }

        return false;
    }
    
    #endregion

    #region POINTS

    public static Vector3 ClosestPointToTriangle(Vector3 point, Vector3 trianglePointA, Vector3 trianglePointB, Vector3 trianglePointC){
        Vector3 result = Vector3.zero;

        Vector3 triangleNormal = GetTriangleNormal(trianglePointA, trianglePointB, trianglePointC);

        Vector3 projectedPoint = ProjectPointPlane(point, trianglePointA, triangleNormal);

        // If the project is already inside the triangle, we're done
        if(IsPointInTriangle(projectedPoint, trianglePointA, trianglePointB, trianglePointC, triangleNormal))
            return projectedPoint;

        // Otherwise test each line against the projection, starting with line AB, then BC, and CA
        result = ClosestPointToLine(projectedPoint, trianglePointA, trianglePointB);
        float minDistance = (result - projectedPoint).sqrMagnitude;

        Vector3 testPoint = ClosestPointToLine(projectedPoint, trianglePointB, trianglePointC);
        float testDistance = (testPoint - projectedPoint).sqrMagnitude;

        if(testDistance < minDistance){
            minDistance = testDistance;
            result = testPoint;
        }

        testPoint = ClosestPointToLine(projectedPoint, trianglePointC, trianglePointA);
        testDistance = (testPoint - projectedPoint).sqrMagnitude;

        if(testDistance < minDistance){
            minDistance = testDistance;
            result = testPoint;
        }

        return result;
    }

    public static Vector3 ProjectPointPlane(Vector3 point, Vector3 planePosition, Vector3 planeNormal){
        float planeHeight = Vector3.Dot(planePosition, planeNormal);
        float pointHeight = Vector3.Dot(point, planeNormal);

        float deltaHeight = planeHeight - pointHeight;
        
        return point + (deltaHeight * planeNormal);
    }

    public static float ClosestPointToRay_T(Vector3 point, Ray ray){
        return Vector3.Dot((point - ray.origin), ray.direction);
    }

    public static Vector3 ClosestPointToRay(Vector3 point, Ray ray){
        return ray.origin + (ray.direction * ClosestPointToRay_T(point, ray));
    }

    public static Vector3 ClosestPointToLine(Vector3 point, Vector3 lineA, Vector3 lineB){
        Vector3 lineDirection = lineB - lineA;
        float lineMagnitude = lineDirection.magnitude;
        lineDirection.Normalize();
        float pointDistance = ClosestPointToRay_T(point, new Ray(lineA, lineDirection));
        pointDistance = Mathf.Clamp(pointDistance, 0, lineMagnitude);
        return lineA + (lineDirection * pointDistance);
    }

    #endregion

    #region SHAPES
    
    public static Vector3 GetTriangleNormal(Vector3 trianglePointA, Vector3 trianglePointB, Vector3 trianglePointC){
        return Vector3.Cross(trianglePointB - trianglePointA, trianglePointC - trianglePointA).normalized;
    }

    public static bool IsPointInTriangle(Vector3 point, Vector3 trianglePointA, Vector3 trianglePointB, Vector3 trianglePointC){
        return IsPointInTriangle(point, trianglePointA, trianglePointB, trianglePointC, GetTriangleNormal(trianglePointA, trianglePointB, trianglePointC));
    }

    public static bool IsPointInTriangle(Vector3 point, Vector3 trianglePointA, Vector3 trianglePointB, Vector3 trianglePointC, Vector3 triangleNormal){
        return (
            Vector3.Dot(Vector3.Cross(trianglePointB - trianglePointA, point - trianglePointA), triangleNormal) > 0 &&
            Vector3.Dot(Vector3.Cross(trianglePointC - trianglePointB, point - trianglePointB), triangleNormal) > 0 &&
            Vector3.Dot(Vector3.Cross(trianglePointA - trianglePointC, point - trianglePointC), triangleNormal) > 0 
        );
    }

    public static bool IsPointInSphere(Vector3 point, Vector3 spherePosition, float radius){
        return (point - spherePosition).sqrMagnitude < radius * radius;
    }
    
    #endregion
}

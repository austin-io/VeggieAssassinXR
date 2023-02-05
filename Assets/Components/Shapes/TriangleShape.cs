using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleShape : BaseShape {
    
    public Vector3 pointA = Vector3.zero, pointB = Vector3.zero, pointC = Vector3.zero, normal = Vector3.up;
    
    void Update(){
        if(DRAW_DEBUG){
            Debug.DrawLine(pointA, pointB);
            Debug.DrawLine(pointB, pointC);
            Debug.DrawLine(pointA, pointC);
        }
    }

    public Vector3 CalcNormal(){
        normal = Utils.GetTriangleNormal(pointA, pointB, pointC);
        return normal;
    }
}

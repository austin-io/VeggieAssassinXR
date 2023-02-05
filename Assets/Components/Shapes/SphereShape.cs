using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereShape : BaseShape {

    public float radius = 1.0f;

    // Update is called once per frame
    void Update() {
        if(DRAW_DEBUG) DebugExtension.DebugWireSphere(transform.position, Color.green, radius);
    }

    void OnDrawGizmos(){
        if(DRAW_DEBUG) DebugExtension.DebugWireSphere(transform.position, Color.green, radius);
    }
}

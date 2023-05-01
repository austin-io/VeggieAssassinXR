using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionSystem : MonoBehaviour {
    
    [SerializeField] public List<Katana> katanas = new List<Katana>();
    [SerializeField] public List<SphereShape> shapes = new List<SphereShape>();
    [SerializeField] UnityEvent OnShapeHit;
    
    //void Start(){}

    void Update(){
        foreach(var sphere in shapes){
            if(sphere == null || sphere.isDone) continue;
            
            foreach(var katana in katanas){
                bool wasHit = Utils.IsPointInSphere(Utils.ClosestPointToTriangle(sphere.transform.position, katana.mainTriangle.pointA, katana.mainTriangle.pointB, katana.mainTriangle.pointC), sphere.transform.position, sphere.radius);
                wasHit |= Utils.IsPointInSphere(Utils.ClosestPointToTriangle(sphere.transform.position, katana.followTriangle.pointA, katana.followTriangle.pointB, katana.followTriangle.pointC), sphere.transform.position, sphere.radius);
                
                if(wasHit){
                    sphere.OnShapeHit?.Invoke(katana.mainTriangle);
                    katana.mainTriangle.OnShapeHit?.Invoke(sphere);
                    OnShapeHit?.Invoke();
                    StartCoroutine(katana.ShakeController());
                }
            }
        }

        shapes.RemoveAll(DeadShape);
    }

    private static bool DeadShape(BaseShape shape){
        return shape.isDone;
    }

}

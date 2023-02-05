using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodType : MonoBehaviour {
    
    [SerializeField] Material mat;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] CollisionSystem collisionSystem;
    [SerializeField] SphereShape hitsphere;

    // Start is called before the first frame update
    void Start() {
        hitsphere.OnShapeHit.AddListener(OnHit);
        collisionSystem.shapes.Add(hitsphere);
    }

    // Update is called once per frame
    void Update(){

    }

    public void OnHit(BaseShape shape){
        mesh.material = mat;
    }
}

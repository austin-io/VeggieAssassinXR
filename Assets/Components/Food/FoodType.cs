using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodType : MonoBehaviour {
    
    [SerializeField] Material mat;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] CollisionSystem collisionSystem;
    [SerializeField] public SphereShape hitsphere;
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpImpulse = 10;

    // Start is called before the first frame update
    void Start() {
        if(collisionSystem == null) collisionSystem = FindObjectOfType<CollisionSystem>();

        hitsphere.OnShapeHit.AddListener(OnHit);
        collisionSystem.shapes.Add(hitsphere);

        rb.AddForce(Vector3.up * 8, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update(){
        if(transform.position.y > 0.2f) return;

        hitsphere.isDone = true;
        Destroy(gameObject);
    }

    public void OnHit(BaseShape shape){
        //mesh.material = mat;
        transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 1.75f, Random.Range(-1.5f, 1.5f));
        hitsphere.isDone = true;
        Destroy(gameObject);
    }
}

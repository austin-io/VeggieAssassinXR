using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EzySlice;

public class FoodType : MonoBehaviour {
    
    [SerializeField] GameObject foodObject;
    [SerializeField] CollisionSystem collisionSystem;
    [SerializeField] public SphereShape hitsphere;
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpImpulse = 8;
    [SerializeField] TrailRenderer trail;
    [SerializeField] GameObject foodBurst, failBurst;
    
    public UnityEvent onMissEvent;
    public Material crossSectionMaterial; 

    // Start is called before the first frame update
    void Start() {}

    void OnDestroy(){
        onMissEvent.RemoveAllListeners();
    }

    public void Init(CollisionSystem collision_system, GameObject food_object, Material cross_section_material, float radius, Vector3 startingImpulse){
        //if(collisionSystem == null) 
        collisionSystem = collision_system;
        foodObject = food_object;
        crossSectionMaterial = cross_section_material;
        hitsphere.radius = radius;
        trail.material = cross_section_material;

        hitsphere.OnShapeHit.AddListener(OnHit);
        collisionSystem.shapes.Add(hitsphere);

        rb.AddForce(startingImpulse * jumpImpulse, ForceMode.Impulse);
        rb.AddTorque(Random.rotation * Vector3.up * 0.05f, ForceMode.Impulse);

        onMissEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update(){
        if(transform.position.y > 0.2f) return;
        if(hitsphere.isDone) return;

        hitsphere.isDone = true;
        
        // Increase misses
        onMissEvent?.Invoke();
        Instantiate(failBurst, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    public void OnHit(BaseShape shape){
        TriangleShape tri = shape as TriangleShape;
        if(tri == null) return;

        Vector2 planeDirection = tri.CalcNormal();
        if(planeDirection.sqrMagnitude != 1){
            planeDirection = foodObject.transform.forward;
        }

        GameObject[] pieces = foodObject.SliceInstantiate(tri.pointA, planeDirection, crossSectionMaterial);

        if(pieces == null){
            pieces = foodObject.SliceInstantiate(transform.position, planeDirection, crossSectionMaterial);
        }

        pieces[0].transform.SetParent(transform);
        pieces[1].transform.SetParent(transform);

        pieces[0].transform.localPosition = Vector3.zero;
        pieces[1].transform.localPosition = Vector3.zero;

        foodObject.SetActive(false);

        hitsphere.isDone = true;

        // dampen upwards velocity
        if(rb.velocity.y > 0)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.4f, rb.velocity.z);

        GameObject burstGO = Instantiate(foodBurst, transform.position, transform.rotation);
        ParticleSystem.MainModule particleSettings = burstGO.GetComponent<ParticleSystem>().main;
        particleSettings.startColor = crossSectionMaterial.color;

        StartCoroutine(HandleSlices(pieces[0], pieces[1], planeDirection));
    }

    IEnumerator HandleSlices(GameObject lower, GameObject upper, Vector3 normal){
        while(transform.position.y > 0.2f){
            lower.transform.position += normal * Time.fixedDeltaTime;
            upper.transform.position -= normal * Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionSystem))]
public class FoodSpawnSystem : MonoBehaviour {
    
    [SerializeField] GameObject FoodObject;
    [SerializeField] CollisionSystem collisionSystem;
    [SerializeField] GameSystem gameSystem;
    [SerializeField] List<FoodData> foodData;
    [SerializeField] GameObject poofParticles;
    [SerializeField] Transform playerCamera;
    
    float timer = 0;

    [System.Serializable]
    public class FoodData {
        public GameObject foodModel;
        public Material crossSectionMaterial;
        public float radius;
    }

    // Start is called before the first frame update
    void Start() {
        if(collisionSystem == null) collisionSystem = GetComponent<CollisionSystem>();
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if(timer < 1) return;
        timer = 0;

        Vector3 focalPoint = Vector3.Scale(playerCamera.forward, new Vector3(1,0,1)).normalized * 2;

        //Vector3 spawnPosition = Vector3.forward * Random.Range(1, 1.5f);
        //spawnPosition = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * spawnPosition;
        Vector3 playerGroundPosition = Vector3.Scale(playerCamera.position, new Vector3(1,0,1));
        Vector3 spawnPosition = playerGroundPosition + focalPoint + (new Vector3(focalPoint.z, 0, -focalPoint.x) * Random.Range(-1.0f, 1.0f));
        spawnPosition.y = 0.3f;

        Instantiate(poofParticles, spawnPosition, Quaternion.identity);
        
        int randomIndex = Random.Range(0, foodData.Count);
        FoodType food = Instantiate(FoodObject, spawnPosition, Quaternion.identity).GetComponent<FoodType>();


        food.Init(
            collisionSystem, 
            Instantiate(foodData[randomIndex].foodModel, food.transform),
            foodData[randomIndex].crossSectionMaterial, 
            foodData[randomIndex].radius,
            (focalPoint * -0.05f) + (Quaternion.LookRotation(((playerGroundPosition + new Vector3(0,10,0)) - spawnPosition).normalized) * Vector3.forward)
            //((playerGroundPosition + new Vector3(0,0.3f,0)) - spawnPosition).normalized 
            );
        //collisionSystem.shapes.Add(food.hitsphere);
        
        food.onMissEvent.AddListener(gameSystem.OnMiss);
    }
}

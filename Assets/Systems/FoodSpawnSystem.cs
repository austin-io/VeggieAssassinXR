using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionSystem))]
public class FoodSpawnSystem : MonoBehaviour {
    
    [SerializeField] GameObject FoodObject;
    [SerializeField] CollisionSystem collisionSystem;
    [SerializeField] List<FoodData> foodData;
    [SerializeField] GameObject poofParticles;
    
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

        Vector3 spawnPosition = Vector3.forward * Random.Range(1, 1.5f);
        spawnPosition = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * spawnPosition;
        spawnPosition.y = 0.3f;

        Instantiate(poofParticles, spawnPosition, Quaternion.identity);
        
        int randomIndex = Random.Range(0, foodData.Count);
        FoodType food = Instantiate(FoodObject, spawnPosition, Quaternion.identity).GetComponent<FoodType>();
        food.Init(
            collisionSystem, 
            Instantiate(foodData[randomIndex].foodModel, food.transform),
            foodData[randomIndex].crossSectionMaterial, 
            foodData[randomIndex].radius);
        //collisionSystem.shapes.Add(food.hitsphere);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnSystem : MonoBehaviour {
    
    [SerializeField] GameObject FoodObject;
    //[SerializeField] CollisionSystem collisionSystem;
    
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if(timer < 1) return;
        timer = 0;

        Vector3 spawnPosition = Vector3.forward * Random.Range(1, 1.5f);
        spawnPosition = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * spawnPosition;
        spawnPosition.y = 0.3f;

        FoodType food = Instantiate(FoodObject, spawnPosition, Quaternion.identity).GetComponent<FoodType>();
        //collisionSystem.shapes.Add(food.hitsphere);
    }
}

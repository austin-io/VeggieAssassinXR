using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour {
    
    [SerializeField] Transform hiltTransform, tipTransform, testPoint;
    [SerializeField] CollisionSystem collisionSystem;
    [SerializeField] UltimateXR.Haptics.Helpers.UxrFixedHapticFeedback haptics;
    [HideInInspector] public TriangleShape mainTriangle, followTriangle;


    Vector3 lastHilt, lastTip;
    
    // Start is called before the first frame update
    void Start() {
        lastHilt = hiltTransform.position;
        lastTip = tipTransform.position;

        mainTriangle = gameObject.AddComponent<TriangleShape>();
        followTriangle = gameObject.AddComponent<TriangleShape>();

        collisionSystem.katanas.Add(this);

    }

    // Update is called once per frame
    void Update() {

        UpdateTriangles();

        //DebugExtension.DebugWireSphere(Utils.ClosestPointToTriangle(testPoint.position, tipTransform.position, hiltTransform.position, lastTip), Color.red, 0.05f);
        //DebugExtension.DebugWireSphere(Utils.ClosestPointToTriangle(testPoint.position, hiltTransform.position, lastHilt, lastTip), Color.magenta, 0.05f);

        lastHilt = hiltTransform.position;
        lastTip = tipTransform.position;
    }

    void UpdateTriangles(){
        mainTriangle.pointA = tipTransform.position; 
        mainTriangle.pointB = hiltTransform.position; 
        mainTriangle.pointC = lastTip;

        followTriangle.pointA = hiltTransform.position; 
        followTriangle.pointB = lastHilt; 
        followTriangle.pointC = lastTip;
    }

    public IEnumerator ShakeController(){
        haptics.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        haptics.gameObject.SetActive(false);
    }
}

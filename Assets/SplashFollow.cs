using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashFollow : MonoBehaviour {

    [SerializeField] Transform bladeBase, bladeTip, planeTransform;
    [SerializeField] ParticleSystem rippleParticles, splashParticles;
    
    float rayLength = 1.0f;
    bool isActive = false;

    // Start is called before the first frame update
    void Start() {
        rayLength = (bladeTip.position - bladeBase.position).magnitude;
    }

    // Update is called once per frame
    void Update() {

        bool wasPlaying = isActive;
        Ray ray = new Ray(bladeBase.position, (bladeTip.position - bladeBase.position) / rayLength);
        if(RayPlaneIntersection(planeTransform.position, planeTransform.up, ray, out float t, out Vector3 hitPoint) && t < rayLength && t > 0){
            isActive = true;
            transform.position = hitPoint;
        } else isActive = false;

        if(!wasPlaying && isActive){
            rippleParticles.Play(true);
            splashParticles.Play(true);
        } 

        if(!isActive) {
            rippleParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            splashParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

    }

    bool RayPlaneIntersection(Vector3 planePosition, Vector3 planeNormal, Ray ray, out float t, out Vector3 hitPoint){
        t = 0;
        hitPoint = ray.origin;
        float denom = Vector3.Dot(planeNormal, ray.direction);
        
        if(Mathf.Abs(denom) > 0.0001f){
            t = Vector3.Dot((planePosition - ray.origin), planeNormal) / denom;
            hitPoint = ray.origin + (ray.direction * t);
            return true;
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShape : MonoBehaviour {
    [SerializeField] protected bool DRAW_DEBUG = false;

    public UnityEngine.Events.UnityEvent<BaseShape> OnShapeHit;

    void Start(){
        if(OnShapeHit == null)
            OnShapeHit = new UnityEngine.Events.UnityEvent<BaseShape>();
    }

}

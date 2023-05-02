using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour {

    [SerializeField] List<GameObject> lifeImages;

    int currentLife = 2;

    public void ResetLives(){
        lifeImages[0].SetActive(true);
        lifeImages[1].SetActive(true);
        lifeImages[2].SetActive(true);

        lifeImages[3].SetActive(false);
        lifeImages[4].SetActive(false);
        lifeImages[5].SetActive(false);
        
        currentLife = 2;
    }

    public void RemoveLife(){
        lifeImages[currentLife].SetActive(false);
        lifeImages[currentLife+3].SetActive(true);
        currentLife--;
    }


}

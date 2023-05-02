using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

    [SerializeField] FoodSpawnSystem foodSpawnSystem;
    [SerializeField] GameObject mainMenu, gameOverMenu;
    [SerializeField] TMPro.TMP_Text scoreText;
    [SerializeField] LifeCounter lifeCounter;

    int m_PlayerScore = 0, m_Misses = 0;
    bool gameEnded = false;

    // Start is called before the first frame update
    void Start() {
        //OnStartButton();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnStartButton(){
        foodSpawnSystem.enabled = true;
        m_PlayerScore = 0;
        m_Misses = 0;
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameEnded = false;
        lifeCounter.ResetLives();
        lifeCounter.gameObject.SetActive(true);
    }

    public void OnGameOver(){
        Debug.Log("OnGameOver Triggered");
        foodSpawnSystem.enabled = false;
        scoreText.text = "Score: " + m_PlayerScore;
        gameOverMenu.SetActive(true);
        gameEnded = true;
        lifeCounter.gameObject.SetActive(false);
    }

    public void OnMiss(){
        Debug.Log("OnMiss Triggered");
        if(gameEnded) return;

        if(m_Misses >= 3) {
            OnGameOver();
            return;
        }

        lifeCounter.RemoveLife();

        m_Misses++;
    }

    public void OnShapeHit(){
        m_PlayerScore += 100;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float startWait;
	public float spawnWait;
	public float waveWait;
    public int lifepoint;
    public int winScore;
    private int waveCount =1;

    public GUIText scoreText;
	private int score;

    public GUIText lifeText;
    private int life;

    public GUIText restartText;
	public GUIText gameoverText;
	private bool gameover;
	private bool restart;

	IEnumerator SpawnWave(){
		yield return new WaitForSeconds(startWait);
        while (true){
            if (waveCount <= 3)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    int random = UnityEngine.Random.Range(1, 10);
                    if(random <= 2)
                    {
                        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), UnityEngine.Random.Range(spawnValues.y-10, spawnValues.y), spawnValues.z);
                        Quaternion spawnRotate = Quaternion.identity;
                        Instantiate(hazard, spawnPos, spawnRotate);
                    }
                    else if(random <= 7)
                    {
                        int x = 1;
                        if (waveCount >= 2) x = waveCount;
                        for (int j = 0; j < x; j++)
                        {
                            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), UnityEngine.Random.Range(spawnValues.y - 10, spawnValues.y), spawnValues.z);
                            Quaternion spawnRotate = Quaternion.identity;
                            Instantiate(hazard, spawnPos, spawnRotate);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < waveCount; j++)
                        {
                            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), UnityEngine.Random.Range(spawnValues.y - 10, spawnValues.y), spawnValues.z);
                            Quaternion spawnRotate = Quaternion.identity;
                            Instantiate(hazard, spawnPos, spawnRotate);
                        }
                    }
                    yield return new WaitForSeconds(spawnWait);
                }
                waveCount++;
            }
            else
            {
                hazardCount++ ;
                for (int i = 0; i < hazardCount; i++)
                {
                    int random = UnityEngine.Random.Range(1, 10);
                    if (random <= 2)
                    {
                        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), UnityEngine.Random.Range(spawnValues.y - 10, spawnValues.y), spawnValues.z);
                        Quaternion spawnRotate = Quaternion.identity;
                        Instantiate(hazard, spawnPos, spawnRotate);
                    }
                    else if (random <= 7)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), UnityEngine.Random.Range(spawnValues.y - 10, spawnValues.y), spawnValues.z);
                            Quaternion spawnRotate = Quaternion.identity;
                            Instantiate(hazard, spawnPos, spawnRotate);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < waveCount; j++)
                        {
                            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), UnityEngine.Random.Range(spawnValues.y - 10, spawnValues.y), spawnValues.z);
                            Quaternion spawnRotate = Quaternion.identity;
                            Instantiate(hazard, spawnPos, spawnRotate);
                        }
                    }
                    yield return new WaitForSeconds(spawnWait);
                }
            }
			if (gameover) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
        }
	}
	// Use this for initialization
	void Start () {
		score = 0;
		gameover = false;
		restart = false;
		restartText.text = "";
		gameoverText.text = "";
		UpdateScore ();
		StartCoroutine (SpawnWave ());
	}

    public void DecreaseLife()
    {
        lifepoint -= 1;
        UpdateScore();
        if (lifepoint <= 0) GameOver();
    }

    public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
        if (score >= winScore) Win();
	}
    public void ReduceScore(int newScoreValue)
    {
        score -= newScoreValue;
        UpdateScore();
    }

    void UpdateScore(){
		scoreText.text = "Score: " + score;
        lifeText.text = "Life: " + lifepoint;
	}

	// Update is called once per frame
	void Update () {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	public void GameOver(){
		gameoverText.text = "Game Over!";
		gameover = true;
	}
    public void Win()
    {
        gameoverText.text = "You Win!";
        gameover = true;
    }
    public bool isGameOver()
    {
        return gameover;
    }
}

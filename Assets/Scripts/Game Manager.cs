using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public float maxX;
    public Transform spawnPoint;
    public float spawnRate;
    public Transform player; // Reference to the player
    public float swipeThreshold = 50f; // Minimum swipe distance
    public float fastMoveDistance = 2f; // Distance to move fast on swipe
    public AudioSource backgroundMusic; // Reference to background music

    private bool gameStarted = false;
    private bool gamePaused = false;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartSpawning();
            gameStarted = true;
            if (backgroundMusic != null)
            {
                backgroundMusic.Play();
            }
        }

        // Detect two-finger touch to toggle pause
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began)
        {
            TogglePause();
        }

        DetectSwipe();
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnBlock", 0.5f, spawnRate);
    }

    private void SpawnBlock()
    {
        if (!gamePaused)
        {
            Vector3 spawnPos = spawnPoint.position;
            spawnPos.x = Random.Range(-maxX, maxX);
            Instantiate(block, spawnPos, Quaternion.identity);
        }
    }

    private void TogglePause()
    {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0f : 1f; // Pause or resume the game

        if (backgroundMusic != null)
        {
            if (gamePaused)
                backgroundMusic.Pause();
            else
                backgroundMusic.UnPause();
        }
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                if (Mathf.Abs(swipeDelta.x) > swipeThreshold && Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0)
                    {
                        MovePlayerRight();
                    }
                    else
                    {
                        MovePlayerLeft();
                    }
                }
            }
        }
    }

    private void MovePlayerRight()
    {
        if (player != null)
        {
            player.position += Vector3.right * fastMoveDistance;
        }
    }

    private void MovePlayerLeft()
    {
        if (player != null)
        {
            player.position += Vector3.left * fastMoveDistance;
        }
    }
}

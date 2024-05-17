using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    private static Game_Manager _instance;
    public static Game_Manager Instance => _instance;

    public bool _isGameOver;

    private void Awake()
    {
        if (_instance != null) return;
        
        if (_instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        _isGameOver = false;
    }

    private void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}

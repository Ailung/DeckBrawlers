using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private SceneLoadManager sceneLoadManager;
    public CharacterController characterController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        sceneLoadManager = new SceneLoadManager();
    }

    public void ChangeScene(string id)
    {
        sceneLoadManager.ChangeScene(id);
    }
    public void quitApp()
    {
        Application.Quit();
    }

    public void SetCharacter(CharacterController character)
    {
        characterController = character;
    }

    private void Update()
    {
        if (FindObjectsOfType<Enemy>().Length <= 0 && SceneManager.GetActiveScene().name == "Level1")
        {
            ChangeScene("Win");
        }
    }
}

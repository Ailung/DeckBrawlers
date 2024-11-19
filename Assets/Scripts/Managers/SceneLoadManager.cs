using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public void ChangeScene(string id)
    {
        SceneManager.LoadScene(id);
    }
}

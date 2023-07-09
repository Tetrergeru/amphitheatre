using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartFromMenu : MonoBehaviour
{
    public void LoadLevel(int levelId)
    {
        SceneManager.LoadSceneAsync(levelId);
    }
}

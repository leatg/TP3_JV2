using System.Collections;
using System.Collections.Generic;
using SceneM = UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void PlayButton()
    {
        SceneM.SceneManager.LoadScene(1);
    }
}

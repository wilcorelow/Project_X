using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public GameObject player;

    public void KillPlayer()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

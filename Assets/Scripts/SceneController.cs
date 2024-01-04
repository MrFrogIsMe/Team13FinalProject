using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
   
        Debug.Log("GameStart method called.");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GameStart(){
        SceneManager.LoadScene("Game");
    }

    public void GameBack(){
        SceneManager.LoadScene("Start");
    }



}

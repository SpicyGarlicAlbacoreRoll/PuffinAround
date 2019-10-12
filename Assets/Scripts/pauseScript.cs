using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour {
    
    public bool gamePaused = false;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start() {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pausePanel.SetActive(gamePaused);

        if(Input.GetKeyDown(KeyCode.Escape)){
            if (gamePaused) {
                Time.timeScale = 1;
                gamePaused = false;
            }
            else {
                Time.timeScale = 0;
                gamePaused = true;
            }
        }
    }
}

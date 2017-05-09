using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMovement : MonoBehaviour {

    public BoardData board;
    public GhostAI ghostAI;
    public GameObject pacmanGameObject;

    /*
     * I was running into trouble initializing these variables in void start()
     * when calling them in the child classes. So I changed it to Awake. Awake runs before
     * start does so these are for sure instantiated before they are used.
     */
    void Awake () {
        this.board = new BoardData();
        this.ghostAI = new GhostAI();
        this.pacmanGameObject = GameObject.Find("pacman");
    }

    /*
     * public bool stopGhost():
     * This function belongs to the base class because every ghost
     * needs to stop at certain points in the game. It's easy to do it
     * this way so I don't have to copy paste code around.
     */
    public bool stopGhost(string ghostName)
    {
        bool ghostStop = false;

        if (GameManager.GMinstance.blinkyLocked == true && ghostName == "Blinky" ||
            GameManager.GMinstance.pinkyLocked == true && ghostName == "Pinky" ||
            GameManager.GMinstance.inkyLocked == true && ghostName == "Inky" ||
            GameManager.GMinstance.clydeLocked == true && ghostName == "Clyde")
        {
            ghostStop = true;
        }
        if (AudioManager.AMinstance.audioSources[0].isPlaying == true)
        {
            ghostStop = true;
        }

        return ghostStop;
    }
}

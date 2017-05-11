using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMovement : MonoBehaviour {

    public BoardData board;
    public GhostAI ghostAI;
    public GameObject pacmanGameObject;
    public Vector3[] vects;

    /*
     * I was running into trouble initializing these variables in void start()
     * when calling them in the child classes. So I changed it to Awake. Awake runs before
     * start does so these are for sure instantiated before they are used.
     */
    void Awake () {
        this.board = new BoardData();
        this.ghostAI = new GhostAI();
        this.pacmanGameObject = GameObject.Find("pacman");
        vects = board.getVects();
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

    public Vector3 goToCenter(Vector3 currVect, ref Vector3 lastMove, BoardData board)
    {
        //Vector3.Distance(transform.position, this.dest) <= 0.1f
        Vector3[] vects = board.getVects();
         Debug.Log("checking for ghost in house***********************");
        if(Vector3.Distance(GameManager.GMinstance.blinky.GetComponent<BlinkyMovement>().dest, vects[69]) <= 5)
        {
            Debug.Log("blinky in ghost house***********************");
            GameManager.GMinstance.blinkyMode = true;
            GameManager.GMinstance.blinky.GetComponent<BlinkyMovement>().ghostEaten = false;
            GameManager.GMinstance.blinky.GetComponent<Renderer>().material.color = GameManager.GMinstance.blinky.GetComponent <BlinkyMovement>().red;
        }
        else if(Vector3.Distance(GameManager.GMinstance.pinky.GetComponent<PinkyMovement>().dest, vects[69]) <= 5)
        {
            Debug.Log("pinky in ghost house***********************");
            GameManager.GMinstance.pinkyMode = true;
            GameManager.GMinstance.pinky.GetComponent<PinkyMovement>().ghostEaten = false;
            GameManager.GMinstance.pinky.GetComponent<Renderer>().material.color = GameManager.GMinstance.pinky.GetComponent<PinkyMovement>().pink;
        }
        else if (Vector3.Distance(GameManager.GMinstance.inky.GetComponent<InkyMovement>().dest, vects[69]) <= 5)
        {
            Debug.Log("inky in ghost house***********************");
            GameManager.GMinstance.InkyMode = true;
            GameManager.GMinstance.inky.GetComponent<InkyMovement>().ghostEaten = false;
            GameManager.GMinstance.inky.GetComponent<Renderer>().material.color = GameManager.GMinstance.inky.GetComponent<InkyMovement>().lightBlue;
        }
        else if (Vector3.Distance(GameManager.GMinstance.clyde.GetComponent<ClydeMovement>().dest, vects[69]) <= 5)
        {
            Debug.Log("clyde in ghost house***********************");
            GameManager.GMinstance.clydeMode = true;
            GameManager.GMinstance.clyde.GetComponent<ClydeMovement>().ghostEaten = false;
            GameManager.GMinstance.clyde.GetComponent<Renderer>().material.color = GameManager.GMinstance.clyde.GetComponent<ClydeMovement>().orange;
        }
        else
        {
            Debug.Log("GoToCenter Error.");
        }

        return ghostAI.AStar(currVect, vects[69], ref lastMove, board);
    }
}

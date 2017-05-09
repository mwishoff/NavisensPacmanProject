using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour {

    public static GameManager GMinstance;
    public int PelletsOnBoard;
    public float timer;

    //True means can't move, false means can move.
    public bool blinkyLocked;
    public bool pinkyLocked;
    public bool inkyLocked;
    public bool clydeLocked;
    public int inkyMode;
    // true == chase mode, false == scatter mode.
    public bool ghostMode;

    /*
     * void Awake():
     * I'm taking care of the initialization of the singleton in the Awake function
     * because the order in which functions call start is unclear. If I need the GM
     * in a start function there is a chance it will get me a null reference exception
     * so initializing the instance in Awake() is safer.
     */
    void Awake()
    {
        if (GMinstance != null && GMinstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            GMinstance = this;
        }

        //Board starts with 240 pellets
        PelletsOnBoard = 240;
        timer = 0;
        ghostMode = true;
    }

    /*
     * Start ():
     * The locked functions for the ghosts are booleans
     * that if turned false will not allow the ghosts to move.
     * I decided to put them in the GM so that they are at a central piece
     * in the code, and can be accessed in any class.
     */
    void Start ()
    {
        blinkyLocked = false;
        pinkyLocked = true;
        inkyLocked = true;
        clydeLocked = true;
    }

    /*
     * void Update ():
     * Currently the update for the gameManager is checking for inflexion points in the game.
     * for example when there are a certain number of pellets remaining on the board I am releasing
     * another ghost to chase after pacman. I'm also changing inky's chase mode from blinky to pinky in this update
     * As well as checking for the win condition. This all needs to be done in an update function because update is called
     * 60 times a second (once per frame).
     */
    void Update ()
    {
        timer += Time.deltaTime;

        if(PelletsOnBoard < 220)
        {
            Debug.Log("Releasing Pinky at 220 pellets left.");
            pinkyLocked = false;
        }
        if(PelletsOnBoard < 200)
        {
            Debug.Log("Releasing Inky at 200 pellets left.");
            inkyLocked = false;
        }
        if (PelletsOnBoard < 180)
        {
            Debug.Log("Releasing Clyde at 180 pellets left.");
            clydeLocked = false;
        }


        if (GameManager.GMinstance.getGameTime() % 20 == 0 && inkyMode == 0)
        {
            inkyMode = 1;
        }
        else if (GameManager.GMinstance.getGameTime() % 20 == 0 && inkyMode == 1)
        {
            inkyMode = 0;
        }


        if (checkForDidWin() == true)
        {
            blinkyLocked = true;
            pinkyLocked = true;
            inkyLocked = true;
            clydeLocked = true;
        }
	}

    /*
     * public bool checkForDidWin():
     * Checks for a win condition on the board.
     */
    public bool checkForDidWin()
    {
        if (this.PelletsOnBoard == 0)
            return true;
        else
            return false;
    }

    /*
     * public int getGameTime():
     * Returns the timer variable as an int.
     * the timer variable can't be declared as an int, because I'm adding the delta
     * between frames, and if it's an int, the additions will be truncated.
     */
    public int getGameTime()
    {
        return (int)timer;
    }

    /*
     * public void setGhostMode(bool newMode):
     * this function can change the ghosts from chase mode
     * to scatter mode.
     */
    public void setGhostMode(bool newMode)
    {
        ghostMode = newMode;
    }

    /*
     * public bool getGhostMode():
     * This function will return which mode
     * the ghosts are currently chasing in.
     */
    public bool getGhostMode()
    {
        return ghostMode;
    }
}

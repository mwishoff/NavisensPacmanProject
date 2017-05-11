using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour {

    public static GameManager GMinstance;
    public int PelletsOnBoard;
    public float timer;
    public float scatterTimer;
    public int timePoint;

    public GameObject blinky;
    public GameObject pinky;
    public GameObject inky;
    public GameObject clyde;

    //True means can't move, false means can move.
    public bool blinkyLocked;
    public bool pinkyLocked;
    public bool inkyLocked;
    public bool clydeLocked;
    public int inkyMode;
    // true == chase mode, false == scatter mode.

    public bool blinkyMode;
    public bool pinkyMode;
    public bool InkyMode;
    public bool clydeMode;

    public int[] ghostEatScore = new int[] { 0, 200, 400, 800, 1600 };
    public int numGhostsEaten = 0;

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
        blinkyMode = true;
        pinkyMode = true;
        InkyMode = true;
        clydeMode = true;
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
        timePoint = 0;

        blinky = GameObject.Find("Blinky");
        pinky = GameObject.Find("Pinky");
        inky = GameObject.Find("Inky");
        clyde = GameObject.Find("Clyde");
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

        if(PelletsOnBoard == 220)
        {
            Debug.Log("Releasing Pinky at 220 pellets left.");
            pinkyLocked = false;
        }
        if(PelletsOnBoard == 200)
        {
            Debug.Log("Releasing Inky at 200 pellets left.");
            inkyLocked = false;
        }
        if (PelletsOnBoard == 180)
        {
            Debug.Log("Releasing Clyde at 180 pellets left.");
            clydeLocked = false;
        }


        scatterTime();


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

    void scatterTime()
    {
        if (blinkyMode == false ||
            pinkyMode == false ||
            InkyMode == false ||
            clydeMode == false)
        {
            scatterTimer += Time.deltaTime;
            if (scatterTimer > timePoint + 12)
            {
                setGhostColorOrig();
                blinkyMode = true;
                pinkyMode = true;
                InkyMode = true;
                clydeMode = true;
            }
        }
        if (blinkyMode == true &&
            pinkyMode == true &&
            InkyMode == true &&
            clydeMode == true)
        {
            timer += Time.deltaTime;
        }
    }

    void setGhostColorOrig()
    {
        if(blinky.GetComponent<BlinkyMovement>().ghostEaten == false)
        {
            blinky.GetComponent<Renderer>().material.color = blinky.GetComponent<BlinkyMovement>().red;
        }
        if(pinky.GetComponent<PinkyMovement>().ghostEaten == false)
        {
            pinky.GetComponent<Renderer>().material.color = pinky.GetComponent<PinkyMovement>().pink;
        }
        if(inky.GetComponent<InkyMovement>().ghostEaten == false)
        {
            inky.GetComponent<Renderer>().material.color = inky.GetComponent<InkyMovement>().lightBlue;
        }
        if(clyde.GetComponent<ClydeMovement>().ghostEaten == false)
        {
            clyde.GetComponent<Renderer>().material.color = clyde.GetComponent<ClydeMovement>().orange;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyMovement : ghostMovement {

    public Vector3 dest;
    Vector3[] inkyScatterTiles;
    public Color lightBlue;
    public bool ghostEaten;
    public Vector3 lastDest;

    /*
     * void Start ():
     * Start is called once when the game object is initialized.
     * gives Inky a inital destination, so when update is called it is not null.
     */
    void Start()
    {
        lastDest = base.vects[69];
        ghostEaten = false;
        lightBlue = gameObject.GetComponent<Renderer>().material.color;
        this.dest = new Vector3(-2.11f, 3.01f, -44.33f);
        Vector3[] vects = base.board.getVects();
        inkyScatterTiles = new[] {vects[51], vects[61], vects[62],
                                  vects[63], vects[67], vects[66],
                                  vects[59], vects[60], vects[50]};
    }

    /*
     * void Update ():
     * Update is called once per frame
     * this function takes care of moving Inky around the board.
     * no logic for choosing where he moves is done here.
     */
    void Update () {
        if (base.stopGhost(gameObject.name) == true) return;

        transform.position = Vector3.MoveTowards(transform.position, this.dest, 0.3f);

        if (Vector3.Distance(transform.position, this.dest) <= 0.1f)
        {
            if (base.pacmanGameObject == null || base.pacmanGameObject.Equals(null))
            {
                GameManager.GMinstance.inkyLocked = true;
            }
            else if (ghostEaten == true)
            {
                this.dest = base.goToCenter(this.dest, ref this.lastDest, base.board);
            }
            else
            {
                this.dest = getNextMove(this.dest, base.pacmanGameObject.transform.position, base.board, inkyScatterTiles, ref this.lastDest);
            }
        }
    }

    /*
     * public Vector3 getNextMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board):
     * Makes a call to the ghostAI to get Inky's next move.
     * I seperated this out so that InkyMovement only deals with moving Inky around the board,
     * so there is no logic in choosing his destination here that is taken care of by the ghostAI fucntion call.
     */
    Vector3 getNextMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board, Vector3[] ScatterTiles, ref Vector3 lastMove)
    {
        pacmanTempMove pacmanTemp = pacmanGameObject.gameObject.GetComponent<pacmanTempMove>();
        return base.ghostAI.InkyMove(currVect, pacmanTemp.dest, pacmanTemp.lastMove, board, ScatterTiles, ref lastMove);
    }
}

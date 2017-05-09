using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyMovement : ghostMovement {
    
    private Vector3 dest;
    Vector3[] blinkyScatterTiles;

    
    /*
     * void Start ():
     * Start is called once when the game object is initialized.
     * gives Blinky a inital destination, so when update is called it is not null.
     */
    void Start () {
        this.dest = new Vector3(-2.11f, 3.01f, -44.33f);
        Vector3[] vects = base.board.getVects();
        blinkyScatterTiles = new[] { vects[5], vects[6], vects[13], vects[12] };
    }

    /*
     * void Update ():
     * Update is called once per frame
     * this function takes care of moving blinky around the board.
     * no logic for choosing where he moves is done here.
     */
    void Update () {
        if (base.stopGhost(gameObject.name) == true) return;

        transform.position = Vector3.MoveTowards(transform.position, this.dest, 0.3f);

        if (Vector3.Distance(transform.position, this.dest) <= 0.1f)
        {
            this.dest = getNextMove(this.dest, base.pacmanGameObject.transform.position, base.board, blinkyScatterTiles);
        }
    }

    /*
     * public Vector3 getNextMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board):
     * Makes a call to the ghostAI to get blinky's next move.
     * I seperated this out so that BlinkyMovement only deals with moving blinky around the board,
     * so there is no logic in choosing his destination here that is taken care of by the ghostAI.
     */
    public Vector3 getNextMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board, Vector3[] ScatterTiles)
    {
        return base.ghostAI.BlinkyMove(currVect, pacmanLoc, board, ScatterTiles);
    }
}

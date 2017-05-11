using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeMovement : ghostMovement {

    public Vector3 dest;
    Vector3[] clydeScatterTiles;
    public Color orange;
    public bool ghostEaten; //true means pacman ate the ghost. false means ghost is still livin and kickin.
    public Vector3 lastDest;

    /*
     * void Start ():
     * Start is called once when the game object is initialized.
     * gives Clyde a inital destination, so when update is called it is not null.
     */
    void Start()
    {
        lastDest = base.vects[69];
        ghostEaten = false;
        orange = gameObject.GetComponent<Renderer>().material.color;
        this.dest = new Vector3(-2.11f, 3.01f, -44.33f);
        Vector3[] vects = base.board.getVects();
        clydeScatterTiles = new[] { vects[47], vects[46], vects[56],
                                    vects[55], vects[54], vects[64],
                                    vects[65], vects[58], vects[57] };
    }

    /*
     * void Update ():
     * Update is called once per frame
     * this function takes care of moving Clyde around the board.
     * no logic for choosing where he moves is done here.
     */
    void Update()
    {
        if (base.stopGhost(gameObject.name) == true) return;

        transform.position = Vector3.MoveTowards(transform.position, this.dest, 0.3f);

        if (Vector3.Distance(transform.position, this.dest) <= 0.1f)
        {
            if (base.pacmanGameObject == null || base.pacmanGameObject.Equals(null))
            {
                GameManager.GMinstance.clydeLocked = true;
            }
            else if(ghostEaten == true)
            {
                this.dest = base.goToCenter(this.dest, ref this.lastDest, base.board);
            }
            else
            {
                this.dest = getNextMove(this.dest, base.pacmanGameObject.transform.position, base.board, clydeScatterTiles, ref this.lastDest);
            }
        }
    }

    /*
     * public Vector3 getNextMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board):
     * Makes a call to the ghostAI to get Clyde's next move.
     * I seperated this out so that ClydeMovement only deals with moving Clyde around the board,
     * so there is no logic in choosing his destination here that is taken care of by the ghostAI.
     */
    Vector3 getNextMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board, Vector3[] ScatterTiles, ref Vector3 lastMove)
    {
        return base.ghostAI.ClydeMove(currVect, pacmanLoc, board, ScatterTiles, ref lastMove);
    }
}

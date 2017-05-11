using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI {



    /*
     * public Vector3 BlinkyMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board):
     * Done reformatting for the moment.
     */
    public Vector3 BlinkyMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board, Vector3[] ScatterTiles, ref Vector3 lastMove)
    {
        if (GameManager.GMinstance.blinkyMode == true)
        {
            return AStar(currVect, pacmanLoc, ref lastMove, board);
        }
        else if (GameManager.GMinstance.blinkyMode == false)
        {
            return scatterPathCalc(currVect, board, ScatterTiles, ref lastMove);
        }
        else
        {
            //1337 is off the board, if a ghost moves there something went terribly wrong.
            return new Vector3(1337f, 1337f, 1337f);
        }
    }

    /*
     * public Vector3 PinkyMove(Vector3 currVect, Vector3 pacmanLoc, Vector3 pacmanLastLoc, BoardData board):
     * this function targets pinky at the intersection pacman might* be at next.
     */
    public Vector3 PinkyMove(Vector3 currVect, Vector3 pacmanLoc, Vector3 pacmanLastLoc, BoardData board, Vector3[] ScatterTiles, ref Vector3 lastMove)
    {
        if (GameManager.GMinstance.pinkyMode == true)
        {
            return targetPacmanNextIntersection(currVect, pacmanLoc, pacmanLastLoc, board, ref lastMove);
        }
        else if (GameManager.GMinstance.pinkyMode == false)
        {
            return scatterPathCalc(currVect, board, ScatterTiles, ref lastMove);
        }
        else
        {
            //1337 is off the board, if a ghost moves there something went terribly wrong.
            return new Vector3(1337f, 1337f, 1337f);
        }

    }

    /*
     * public Vector3 InkyMove(Vector3 currVect, Vector3 pacmanLoc, Vector3 pacmanLastLoc, BoardData board):
     * Moves like Blinky or Pinky for a duration.
     */
    public Vector3 InkyMove(Vector3 currVect, Vector3 pacmanLoc, Vector3 pacmanLastLoc, BoardData board, Vector3[] ScatterTiles, ref Vector3 lastMove)
    {
        if (GameManager.GMinstance.InkyMode == true)
        {
            return selectInkyMove(currVect, pacmanLoc, pacmanLastLoc, board, ref lastMove);
        }
        else if (GameManager.GMinstance.InkyMode == false)
        {
            return scatterPathCalc(currVect, board, ScatterTiles, ref lastMove);
        }
        else
        {
            Debug.Log("something went wrong in InkyMove");
            return new Vector3(1337f, 1337f, 1337f);
        }

    }


    /*
     * private Vector3 selectInkyMove(Vector3 currVect, Vector3 pacmanLoc, Vector3 pacmanLastLoc, BoardData board):
     * Takes care of figuring out which mode inky is in to decide if he moves like blinky or pinky.
     */
    private Vector3 selectInkyMove(Vector3 currVect, Vector3 pacmanLoc, Vector3 pacmanLastLoc, BoardData board, ref Vector3 lastMove)
    {
        //Move like blinky
        if (GameManager.GMinstance.inkyMode == 0)
        {
            return AStar(currVect, pacmanLoc, ref lastMove, board);
        }
        //Move like Pinky
        else if (GameManager.GMinstance.inkyMode == 1)
        {
            return targetPacmanNextIntersection(currVect, pacmanLoc, pacmanLastLoc, board, ref lastMove);
        }
        //You broke Inky
        else
        {
            return new Vector3(1337f, 1337f, 1337f);
        }
    }


    /*
     * public Vector3 ClydeMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board):
     * Should move like Blinky until he is within a certain proximity to pacman.
     * Once he is in a certain proximity to pacman, he should loop to his scatter tiles.
     */
    public Vector3 ClydeMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board, Vector3[] ScatterTiles, ref Vector3 lastMove)
    {
        //Clyde will run from pacman once I get scatter working.
        //For now he chases like blinky.
        if (GameManager.GMinstance.clydeMode == true)
        {
            return selectClydeMove(currVect, pacmanLoc, board, ScatterTiles, ref lastMove);
        }
        else if (GameManager.GMinstance.clydeMode == false)
        {
            return scatterPathCalc(currVect, board, ScatterTiles, ref lastMove);
        }
        else
        {
            Debug.Log("something went wrong in Clydemove");
            return new Vector3(1337f, 1337f, 1337f);
        }
    }

    /*
     * public Vector3 selectClydeMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board):
     * Takes care of checking if Clyde is close to pacman. if he is clyde goes to his scatter tiles.
     */
    public Vector3 selectClydeMove(Vector3 currVect, Vector3 pacmanLoc, BoardData board, Vector3[] ScatterTiles, ref Vector3 lastMove)
    {
        if (Vector3.Distance(currVect, pacmanLoc) > 15)
        {
            return AStar(currVect, pacmanLoc, ref lastMove, board);
        }
        else
        {
            return scatterPathCalc(currVect, board, ScatterTiles, ref lastMove);
        }
    }

    /*
     * This algorithm finds the next closest vector3 to choose NOT the entire path.
     * This function also makes sure that the ghost doesn't go through any of the walls,
     * but instead stays on the board grid. I decided to use A* because it's easy to implement,
     * and also has good performance.
     */
    public Vector3 AStar(Vector3 currVect, Vector3 pacmanLoc, ref Vector3 lastMove, BoardData board)
    {
        int index = 1;
        float shortestDist = 9999999.9f;
        //Calculate current distance
        //calculate distance from possible vectors to pacmanLoc vector.
        //Choose best possible vector to return.

        Hashtable Vecthash = board.getHash();
        float key = board.getKey(currVect);
        ArrayList vectArr = (ArrayList)Vecthash[key];

        for (int i = 0; i < vectArr.Count; i++)
        {
            float dist = Vector3.Distance((Vector3)vectArr[i], pacmanLoc);
            if (dist < shortestDist && lastMove != (Vector3)vectArr[i])
            {
                shortestDist = dist;
                index = i;
            }
            //calc distance
            //save shortest distance
        }

        lastMove = currVect;

        //return closest vector.
        return (Vector3)vectArr[index];
    }

    /*
     * public Vector3 targetPacmanNextIntersection(Vector3 currVect, Vector3 pacmanLoc, Vector3 pacmanLastLoc, BoardData board):
     * This algorithm tries to target the next tile pacman will be at. This allows pinky and inky to target the tiles
     * ahead of pacman and have a "ambush" attack pattern, rather than just a "chase" attack pattern.
     */
    public Vector3 targetPacmanNextIntersection(Vector3 currVect, Vector3 pacmanLoc, Vector3 pacmanLastLoc, BoardData board, ref Vector3 lastMove)
    {
        int index = -1;
        float shortestDist = 9999999.9f;
        float dist;
        Vector3 pacmanTargetIntersection;

        Hashtable Vecthash = board.getHash();
        float pacKey = board.getKey(pacmanLoc);
        ArrayList pacVectArr = (ArrayList)Vecthash[pacKey];  //ArrayList of possible moves for pacman
        int randNum = Random.Range(0, pacVectArr.Count);

        //Get a possible move for pacman assuming that pacman can't go backwards
        while ((Vector3)pacVectArr[randNum] == pacmanLastLoc)
        {
            randNum = Random.Range(0, pacVectArr.Count);
        }

        //Assuming this returns the right thing.
        pacmanTargetIntersection = findIntersection(pacmanLoc, pacmanLastLoc, (Vector3)pacVectArr[randNum], board);

        float key = board.getKey(currVect);
        ArrayList vectArr = (ArrayList)Vecthash[key];

        for (int i = 0; i < vectArr.Count; i++)
        {
            dist = Vector3.Distance((Vector3)vectArr[i], pacmanTargetIntersection);

            if (dist < shortestDist && lastMove != (Vector3)vectArr[i])
            {
                shortestDist = dist;
                index = i;
            }
        }

        lastMove = currVect;

        return (Vector3)vectArr[index];
    }

    /*
     * private Vector3 findIntersection(Vector3 pacCurrVect, Vector3 lastVect, Vector3 pacPosLoc, BoardData board):
     * Tries to best guess the next intersection pacman will be at.
     * This could be optimized a little more, I don't like the double while loop, but it works for now.
     */
    private Vector3 findIntersection(Vector3 pacCurrVect, Vector3 lastVect, Vector3 pacPosLoc, BoardData board)
    {
        int randNum;
        Hashtable Vecthash = board.getHash();
        float pacKey = board.getKey(pacPosLoc);
        ArrayList pacVectArr = (ArrayList)Vecthash[pacKey];

        //If the possible move is an intersection 
        if(pacVectArr.Count > 2)
        {
            return pacPosLoc;
        }

        //If the possible move is not an intersection keep moving till you find one.
        while ( pacVectArr.Count <= 2)
        {
            //Keep trying paths till you find one that goes forward.
            randNum = Random.Range(0, pacVectArr.Count);
            while ( (Vector3)pacVectArr[randNum] == lastVect)
            {
                randNum = Random.Range(0, pacVectArr.Count);
            }

            //lastVect = (Vector3)pacVectArr[randNum];
            lastVect = pacCurrVect;
            pacCurrVect = pacPosLoc;
            pacPosLoc = (Vector3)pacVectArr[randNum];


            pacKey = board.getKey((Vector3)pacVectArr[randNum]);
            pacVectArr = (ArrayList)Vecthash[pacKey];
        }

        return pacPosLoc;
    }

    /*
     * private Vector3 blinkyScatter(Vector3 ghostPos, BoardData board):
     * moved code from blinkyScatter to scatterPathcalc. this function may not be needed now.
     */

    /*
     * Depricated functions.
    private Vector3 blinkyScatter(Vector3 ghostPos, BoardData board, Vector3[] ScatterTiles)
    {
        return scatterPathCalc(ghostPos, board, ScatterTiles);
    }


    private Vector3 pinkyScatter(Vector3 ghostPos, BoardData board, Vector3[] ScatterTiles)
    {
        return scatterPathCalc(ghostPos, board, ScatterTiles);
    }


    private Vector3 inkyScatter(Vector3 ghostPos, BoardData board, Vector3[] ScatterTiles)
    {
        return scatterPathCalc(ghostPos, board, ScatterTiles);
    }


    private Vector3 clydeScatter(Vector3 ghostPos, BoardData board, Vector3[] ScatterTiles)
    {
        return scatterPathCalc(ghostPos, board, ScatterTiles);
    }

    */

    /*
     * I've coded the ScatterTiles in a specific order so that I can grab
     * the next one in the array and return it. allowing the ghost to loop around
     * it's specific scatter tile only using this function.
     */
    private Vector3 scatterPathCalc(Vector3 ghostPos, BoardData board, Vector3[] ScatterTiles, ref Vector3 lastMove)
    {
        for (int i = 0; i < ScatterTiles.Length; i++)
        {
            if (ScatterTiles[i] == ghostPos && i != ScatterTiles.Length - 1)
            {
                return AStar(ghostPos, ScatterTiles[i + 1], ref lastMove, board);
            }
        }

        return AStar(ghostPos, ScatterTiles[0], ref lastMove, board);
    }
}

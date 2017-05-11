using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacmanTempMove : MonoBehaviour {

    public Vector3 dest;
    public Vector3 lastMove;
    public BoardData board;
    private Vector3[] demoMoves;
    private Vector3[] demoMoves2;
    private GhostAI gAI;

    // Use this for initialization
    void Start()
    {
        this.gAI = new GhostAI();
        this.dest = new Vector3(-2.34f, 3.01f, -2.45f);
        this.lastMove = this.dest;
        this.board = new BoardData();
        Vector3[] vects = this.board.getVects();
        this.demoMoves = new[] {vects[68], vects[48], vects[47], vects[57], vects[58], vects[65], vects[64], vects[54], vects[55], vects[45], vects[44],
                                 vects[36], vects[37], vects[38], vects[34], vects[35], vects[41], vects[40], vects[49], vects[50], vects[51], vects[61], vects[62],
                                 vects[52], vects[53], vects[43], vects[42], vects[32], vects[21], vects[22], vects[13], vects[6], vects[5], vects[12],
                                 vects[11], vects[10], vects[14], vects[9], vects[8], vects[7], vects[15], vects[16], vects[29], vects[30], vects[23], vects[24],
                                 vects[27], vects[25], vects[26], vects[31], vects[35]};

        this.demoMoves2 = new[] {vects[68], vects[48], vects[47], vects[57], vects[58], vects[65], vects[64], vects[54], vects[55], vects[45], vects[44],
                                 vects[36], vects[37], vects[38], vects[34], vects[35], vects[41], vects[40], vects[49], vects[50], vects[51], vects[61], vects[62],
                                 vects[52], vects[53], vects[43], vects[42], vects[32], vects[21], vects[22], vects[13], vects[6], vects[5], vects[12],
                                 vects[11], vects[10], vects[14], vects[9], vects[8], vects[7], vects[1], vects[2], vects[8]};
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioManager.AMinstance.audioSources[0].isPlaying == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, this.dest, 0.3f);

            if (Vector3.Distance(transform.position, this.dest) <= 0.1f)
            {
                //this.dest = getRandomNextMove(this.dest);
                this.dest = getNextDemoMove(this.dest, this.demoMoves2);
            }
        }
    }

    Vector3 getRandomNextMove(Vector3 currVect)
    {
        Hashtable Vecthash = this.board.getHash();
        float key = this.board.getKey(currVect);
        ArrayList vectArr = (ArrayList)Vecthash[key];
        Vector3[] vects = board.getVects();

        int ranNum = Random.Range(0, vectArr.Count);

        //Don't allow the last position pacman moves to be selected.
        while ((Vector3)vectArr[ranNum] == this.lastMove || (Vector3)vectArr[ranNum] == (Vector3)vects[69])
        {
            ranNum = Random.Range(0, vectArr.Count);
        }

        this.lastMove = this.dest;

        return (Vector3)vectArr[ranNum];

    }

    Vector3 getNextDemoMove(Vector3 currPos, Vector3[] demoMoves)
    {
        for (int i = 0; i < demoMoves.Length; i++)
        {
            if (demoMoves[i] == currPos && i != demoMoves.Length - 1)
            {
                return gAI.AStar(currPos, demoMoves[i + 1], ref this.lastMove, this.board);
            }
        }

        return gAI.AStar(currPos, demoMoves[0], ref this.lastMove, this.board);
    }

    public Vector3 getLastMove()
    {
        return this.lastMove;
    }
}

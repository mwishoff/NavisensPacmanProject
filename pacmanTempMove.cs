using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacmanTempMove : MonoBehaviour {

    public Vector3 dest;
    public Vector3 lastMove;
    public BoardData board;

    // Use this for initialization
    void Start()
    {
        this.dest = new Vector3(-2.34f, 3.01f, -2.45f);
        this.lastMove = this.dest;
        this.board = new BoardData();
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioManager.AMinstance.audioSources[0].isPlaying == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, this.dest, 0.3f);

            if (Vector3.Distance(transform.position, this.dest) <= 0.1f)
            {
                this.dest = getRandomNextMove(this.dest);
            }
        }
    }

    Vector3 getRandomNextMove(Vector3 currVect)
    {
        Hashtable Vecthash = this.board.getHash();
        float key = this.board.getKey(currVect);
        ArrayList vectArr = (ArrayList)Vecthash[key];

        int ranNum = Random.Range(0, vectArr.Count);

        //Don't allow the last position pacman moves to be selected.
        while ((Vector3)vectArr[ranNum] == this.lastMove)
        {
            ranNum = Random.Range(0, vectArr.Count);
        }

        this.lastMove = this.dest;

        return (Vector3)vectArr[ranNum];

    }

    public Vector3 getLastMove()
    {
        return this.lastMove;
    }
}

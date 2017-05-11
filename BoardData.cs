using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData {
    
    private Hashtable boardVectors = new Hashtable();
    private Vector3[] vects = new Vector3[100];

    /*
     * public BoardData():
     * Constructor to initialize the board can't do this in start() or awake()
     * because this script isn't attached to an object and therefore those two fucntions aren't called.
     */
    public BoardData()
    {
        initVectArray();
        initBoardData();
    }

    /*
     * public Hashtable getHash():
     * returns the Hash graph of Vector3's
     */
    public Hashtable getHash()
    {
        return this.boardVectors;
    }

    /*
     * public Vector3[] getVects():
     * returns the list of vector3's used to 
     * created the Hash Graph of Vector3's
     */
    public Vector3[] getVects()
    {
        return this.vects;
    }

    /*
     * private void initBoardData()
     * Initializees the Hash Graph 
     * each vertex has at most 4 neighbors
     * each vertex has at minimum 2 neighbors
     * I used a hash so that I can have O(1) reterival performance isn't impacted 
     */
    private void initBoardData()
    {
        //Portal across map is disabled, look at vects 28,29,33, 32 if you want to reenable it.
        this.boardVectors.Add(getKey(this.vects[1]), new ArrayList { this.vects[2], this.vects[7] });
        this.boardVectors.Add(getKey(this.vects[2]), new ArrayList { this.vects[1], this.vects[8], this.vects[3] });
        this.boardVectors.Add(getKey(this.vects[3]), new ArrayList { this.vects[2], this.vects[14] });
        this.boardVectors.Add(getKey(this.vects[4]), new ArrayList { this.vects[10], this.vects[5] });
        this.boardVectors.Add(getKey(this.vects[5]), new ArrayList { this.vects[4], this.vects[12], this.vects[6] });
        this.boardVectors.Add(getKey(this.vects[6]), new ArrayList { this.vects[5], this.vects[13] });
        this.boardVectors.Add(getKey(this.vects[7]), new ArrayList { this.vects[1], this.vects[8], this.vects[15] });
        this.boardVectors.Add(getKey(this.vects[8]), new ArrayList { this.vects[2], this.vects[7], this.vects[16], this.vects[9] });
        this.boardVectors.Add(getKey(this.vects[9]), new ArrayList { this.vects[8], this.vects[17], this.vects[14] });
        this.boardVectors.Add(getKey(this.vects[10]), new ArrayList { this.vects[14], this.vects[4], this.vects[11] });
        this.boardVectors.Add(getKey(this.vects[11]), new ArrayList { this.vects[10], this.vects[20], this.vects[12] });
        this.boardVectors.Add(getKey(this.vects[12]), new ArrayList { this.vects[11], this.vects[5], this.vects[13], this.vects[21] });
        this.boardVectors.Add(getKey(this.vects[13]), new ArrayList { this.vects[6], this.vects[12], this.vects[22] });
        this.boardVectors.Add(getKey(this.vects[14]), new ArrayList { this.vects[9], this.vects[3], this.vects[10] });
        this.boardVectors.Add(getKey(this.vects[15]), new ArrayList { this.vects[7], this.vects[16] });
        this.boardVectors.Add(getKey(this.vects[16]), new ArrayList { this.vects[8], this.vects[15], this.vects[29] });
        this.boardVectors.Add(getKey(this.vects[17]), new ArrayList { this.vects[9], this.vects[18] });
        this.boardVectors.Add(getKey(this.vects[18]), new ArrayList { this.vects[17], this.vects[24] });
        this.boardVectors.Add(getKey(this.vects[19]), new ArrayList { this.vects[20], this.vects[25] });
        this.boardVectors.Add(getKey(this.vects[20]), new ArrayList { this.vects[19], this.vects[11] });
        this.boardVectors.Add(getKey(this.vects[21]), new ArrayList { this.vects[12], this.vects[22], this.vects[32] });
        this.boardVectors.Add(getKey(this.vects[22]), new ArrayList { this.vects[21], this.vects[13] });
        this.boardVectors.Add(getKey(this.vects[23]), new ArrayList { this.vects[24], this.vects[30]});
        this.boardVectors.Add(getKey(this.vects[24]), new ArrayList { this.vects[18], this.vects[23], this.vects[27]});
        this.boardVectors.Add(getKey(this.vects[25]), new ArrayList { this.vects[27], this.vects[19], this.vects[26]});
        this.boardVectors.Add(getKey(this.vects[26]), new ArrayList { this.vects[25], this.vects[31]});
        this.boardVectors.Add(getKey(this.vects[27]), new ArrayList { this.vects[24], this.vects[25], this.vects[69]});
        this.boardVectors.Add(getKey(this.vects[28]), new ArrayList { this.vects[29]});
        this.boardVectors.Add(getKey(this.vects[29]), new ArrayList { this.vects[16], this.vects[30], this.vects[37]});
        this.boardVectors.Add(getKey(this.vects[30]), new ArrayList { this.vects[29], this.vects[23], this.vects[34]});
        this.boardVectors.Add(getKey(this.vects[31]), new ArrayList { this.vects[26], this.vects[32], this.vects[35]});
        this.boardVectors.Add(getKey(this.vects[32]), new ArrayList { this.vects[31], this.vects[21], this.vects[42]});
        this.boardVectors.Add(getKey(this.vects[33]), new ArrayList { this.vects[32]});
        this.boardVectors.Add(getKey(this.vects[34]), new ArrayList { this.vects[30], this.vects[35], this.vects[38]});
        this.boardVectors.Add(getKey(this.vects[35]), new ArrayList { this.vects[34], this.vects[31], this.vects[41]});
        this.boardVectors.Add(getKey(this.vects[36]), new ArrayList { this.vects[37], this.vects[44]});
        this.boardVectors.Add(getKey(this.vects[37]), new ArrayList { this.vects[29], this.vects[36], this.vects[38], this.vects[46]});
        this.boardVectors.Add(getKey(this.vects[38]), new ArrayList { this.vects[34], this.vects[37], this.vects[39]});
        this.boardVectors.Add(getKey(this.vects[39]), new ArrayList { this.vects[38], this.vects[48]});
        this.boardVectors.Add(getKey(this.vects[40]), new ArrayList { this.vects[49], this.vects[41]});
        this.boardVectors.Add(getKey(this.vects[41]), new ArrayList { this.vects[40], this.vects[35], this.vects[42]});
        this.boardVectors.Add(getKey(this.vects[42]), new ArrayList { this.vects[32], this.vects[41], this.vects[43], this.vects[51]});
        this.boardVectors.Add(getKey(this.vects[43]), new ArrayList { this.vects[42], this.vects[53]});
        this.boardVectors.Add(getKey(this.vects[44]), new ArrayList { this.vects[36], this.vects[45]});
        this.boardVectors.Add(getKey(this.vects[45]), new ArrayList { this.vects[44], this.vects[55]});
        this.boardVectors.Add(getKey(this.vects[46]), new ArrayList { this.vects[37], this.vects[47], this.vects[56]});
        this.boardVectors.Add(getKey(this.vects[47]), new ArrayList { this.vects[46], this.vects[57], this.vects[48]});
        this.boardVectors.Add(getKey(this.vects[48]), new ArrayList { this.vects[47], this.vects[39], this.vects[49]});
        this.boardVectors.Add(getKey(this.vects[49]), new ArrayList { this.vects[48], this.vects[40], this.vects[50]});
        this.boardVectors.Add(getKey(this.vects[50]), new ArrayList { this.vects[49], this.vects[60], this.vects[51]});
        this.boardVectors.Add(getKey(this.vects[51]), new ArrayList { this.vects[50], this.vects[42], this.vects[61]});
        this.boardVectors.Add(getKey(this.vects[52]), new ArrayList { this.vects[53], this.vects[62]});
        this.boardVectors.Add(getKey(this.vects[53]), new ArrayList { this.vects[52], this.vects[43]});
        this.boardVectors.Add(getKey(this.vects[54]), new ArrayList { this.vects[55], this.vects[64]});
        this.boardVectors.Add(getKey(this.vects[55]), new ArrayList { this.vects[54], this.vects[45], this.vects[56]});
        this.boardVectors.Add(getKey(this.vects[56]), new ArrayList { this.vects[55], this.vects[46]});
        this.boardVectors.Add(getKey(this.vects[57]), new ArrayList { this.vects[47], this.vects[58]});
        this.boardVectors.Add(getKey(this.vects[58]), new ArrayList { this.vects[57], this.vects[65]});
        this.boardVectors.Add(getKey(this.vects[59]), new ArrayList { this.vects[66], this.vects[60]});
        this.boardVectors.Add(getKey(this.vects[60]), new ArrayList { this.vects[59], this.vects[50]});
        this.boardVectors.Add(getKey(this.vects[61]), new ArrayList { this.vects[51], this.vects[62]});
        this.boardVectors.Add(getKey(this.vects[62]), new ArrayList { this.vects[52], this.vects[61], this.vects[63]});
        this.boardVectors.Add(getKey(this.vects[63]), new ArrayList { this.vects[62], this.vects[67]});
        this.boardVectors.Add(getKey(this.vects[64]), new ArrayList { this.vects[54], this.vects[65]});
        this.boardVectors.Add(getKey(this.vects[65]), new ArrayList { this.vects[64], this.vects[58], this.vects[66]});
        this.boardVectors.Add(getKey(this.vects[66]), new ArrayList { this.vects[65], this.vects[59], this.vects[67]});
        this.boardVectors.Add(getKey(this.vects[67]), new ArrayList { this.vects[66], this.vects[63]});
        this.boardVectors.Add(getKey(this.vects[68]), new ArrayList { this.vects[48], this.vects[49]});
        this.boardVectors.Add(getKey(this.vects[69]), new ArrayList { this.vects[27]});
    }

    /*
     * public float getKey(Vector3 vect):
     * Creating the keys like this so that I can access the hashtable with a vector3 instead of a seperate identifier.
     * using a prime number you can safely generate unique keys consistently for each vector3.
     */
    public float getKey(Vector3 vect)
    {
        float x = Mathf.RoundToInt(vect.x);
        float y = Mathf.RoundToInt(vect.y);
        float z = Mathf.RoundToInt(vect.z);

        return (x* 293 + z) * z + y;
    }

    /*
     * private void initVectArray():
     * Init the board of vectors in an array like this so that when I init the hashtable of vectors it's clean,
     * and if the location of a vector changes I can change it in one spot instead of a bunch of spots.
     */
    private void initVectArray()
    {
        this.vects[1] = new Vector3(36.54f, 3.01f, -90.23f);
        this.vects[2] = new Vector3(22.14f, 3.01f, -90.23f);
        this.vects[3] = new Vector3(2.67f, 3.01f, -90.23f);
        this.vects[4] = new Vector3(-7.43f, 3.01f, -90.23f);
        this.vects[5] = new Vector3(-27.57f, 3.01f, -90.23f);
        this.vects[6] = new Vector3(-43.05f, 3.01f, -90.23f);
        this.vects[7] = new Vector3(36.54f, 3.01f, -69f);
        this.vects[8] = new Vector3(22.14f, 3.01f, -69f);
        this.vects[9] = new Vector3(11.54f, 3.01f, -69f);
        this.vects[10] = new Vector3(-7.43f, 3.01f, -69f);
        this.vects[11] = new Vector3(-16.17f, 3.01f, -69f);
        this.vects[12] = new Vector3(-27.57f, 3.01f, -69f);
        this.vects[13] = new Vector3(-43.05f, 3.01f, -69f);
        this.vects[14] = new Vector3(2.67f, 3.01f, -69f);
        this.vects[15] = new Vector3(36.54f, 3.01f, -53.11f);
        this.vects[16] = new Vector3(22.14f, 3.01f, -53.11f);
        this.vects[17] = new Vector3(11.54f, 3.01f, -56.9f);
        this.vects[18] = new Vector3(2.49f, 3.01f, -56.9f);
        this.vects[19] = new Vector3(-7.57f, 3.01f, -56.9f);
        this.vects[20] = new Vector3(-16.17f, 3.01f, -56.9f);
        this.vects[21] = new Vector3(-27.57f, 3.01f, -54.14f);
        this.vects[22] = new Vector3(-43.05f, 3.01f, -54.14f);
        this.vects[23] = new Vector3(11.12f, 3.01f, -44.33f);
        this.vects[24] = new Vector3(2.49f, 3.01f, -44.33f);
        this.vects[25] = new Vector3(-7.57f, 3.01f, -44.33f);
        this.vects[26] = new Vector3(-16.14f, 3.01f, -44.33f);
        this.vects[27] = new Vector3(-2.11f, 3.01f, -44.33f);
        this.vects[28] = new Vector3(36.09f, 3.01f, -32.88f);
        this.vects[29] = new Vector3(22.14f, 3.01f, -32.88f);
        this.vects[30] = new Vector3(11.12f, 3.01f, -32.88f);
        this.vects[31] = new Vector3(-16.14f, 3.01f, -32.88f);
        this.vects[32] = new Vector3(-27.57f, 3.01f, -32.88f);
        this.vects[33] = new Vector3(-43.61f, 3.01f, -32.88f);
        this.vects[34] = new Vector3(11.12f, 3.01f, -22.55f);
        this.vects[35] = new Vector3(-16.14f, 3.01f, -22.55f);
        this.vects[36] = new Vector3(36.45f, 3.01f, -12.52f);
        this.vects[37] = new Vector3(22.14f, 3.01f, -12.52f);
        this.vects[38] = new Vector3(11.12f, 3.01f, -12.52f);
        this.vects[39] = new Vector3(2.58f, 3.01f, -12.52f);
        this.vects[40] = new Vector3(-7.5f, 3.01f, -12.52f);
        this.vects[41] = new Vector3(-16.14f, 3.01f, -12.52f);
        this.vects[42] = new Vector3(-27.57f, 3.01f, -12.52f);
        this.vects[43] = new Vector3(-42.97f, 3.01f, -12.52f);
        this.vects[44] = new Vector3(36.45f, 3.01f, -2.67f);
        this.vects[45] = new Vector3(31.81f, 3.01f, -2.67f);
        this.vects[46] = new Vector3(22.14f, 3.01f, -2.67f);
        this.vects[47] = new Vector3(10.96f, 3.01f, -2.67f);
        this.vects[48] = new Vector3(2.58f, 3.01f, -2.67f);
        this.vects[49] = new Vector3(-7.5f, 3.01f, -2.67f);
        this.vects[50] = new Vector3(-16.27f, 3.01f, -2.67f);
        this.vects[51] = new Vector3(-27.57f, 3.01f, -2.67f);
        this.vects[52] = new Vector3(-37.67f, 3.01f, -2.67f);
        this.vects[53] = new Vector3(-42.97f, 3.01f, -2.67f);
        this.vects[54] = new Vector3(36.64f, 3.01f, 7.18f);
        this.vects[55] = new Vector3(31.91f, 3.01f, 7.18f);
        this.vects[56] = new Vector3(22.14f, 3.01f, 7.18f);
        this.vects[57] = new Vector3(10.96f, 3.01f, 7.18f);
        this.vects[58] = new Vector3(2.62f, 3.01f, 7.18f);
        this.vects[59] = new Vector3(-7.62f, 3.01f, 7.18f);
        this.vects[60] = new Vector3(-16.27f, 3.01f, 7.18f);
        this.vects[61] = new Vector3(-27.57f, 3.01f, 7.18f);
        this.vects[62] = new Vector3(-37.67f, 3.01f, 7.18f);
        this.vects[63] = new Vector3(-42.75f, 3.01f, 7.18f);
        this.vects[64] = new Vector3(36.64f, 3.01f, 17.63f);
        this.vects[65] = new Vector3(2.62f, 3.01f, 17.63f);
        this.vects[66] = new Vector3(-7.62f, 3.01f, 17.63f);
        this.vects[67] = new Vector3(-42.75f, 3.01f, 17.63f);
        this.vects[68] = new Vector3(-2.34f, 3.01f, -2.45f);
        this.vects[69] = new Vector3(-1.69f, 3.01f, -32.81f);
    }
}

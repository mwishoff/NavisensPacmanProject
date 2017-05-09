using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager AMinstance;
    public AudioSource[] audioSources;
    private GameObject plane;

    /*
     * 0 PacIntro
     * 1 PacChomp
     * 2 PacDeath
     * 3 EatFruit
     * 4 EatGhost
     * 5 ExtraPacLife
     * 6 PacWin
     * */


    /*
     * void Awake():
     * I'm taking care of the initialization of the singleton in the Awake function
     * because the order in which functions call start is unclear. If I need the AM
     * in a start function there is a chance it will get me a null reference exception
     * so initializing the instance in Awake() is safer.
     */
    void Awake()
    {
        if (AMinstance != null && AMinstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            AMinstance = this;
        }
    }

    /*
     * void Start ():
     * I decided to attach all the audio to the plane gameObject
     * because it's one of the objects that will never be destroyed during
     * the duration of the game.
     */
    void Start ()
    {
        plane = GameObject.Find("Plane");
        audioSources = plane.GetComponents<AudioSource>();
        playIntroSong();
    }

    /*
     * private void playIntroSong():
     * Plays the pacman intro music.
     * The ghosts will stay in place till it is over.
     */
    private void playIntroSong()
    {
        //Play opening music.
        audioSources[0].Play();
    }
}

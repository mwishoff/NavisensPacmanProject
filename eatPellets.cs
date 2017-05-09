using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eatPellets : MonoBehaviour {

    AudioSource[] sounds;
    AudioSource chomp;
    AudioSource pacDie;
    private int score;
    public Text scoreText;

    /*
     * void Start ()
     * 
     */
    void Start ()
    {
        //chomp = AudioManager.AMinstance.audioSources[1];
        //pacDie = AudioManager.AMinstance.audioSources[2];
        sounds = GetComponents<AudioSource>();
        chomp = sounds[0];
        pacDie = sounds[1];


        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    /*
     * void OnTriggerEnter(Collider other)
     * Checks to see what object pacman has collided with
     * and then takes the appropriate action.
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Pellet") == true)
        {
            pelletCollision(other);
        }

        if (other.gameObject.name.Contains("Energizer") == true)
        {
            energizerCollision(other);
        }
        
        if (other.gameObject.name == "Blinky" || other.gameObject.name == "Pinky" ||
            other.gameObject.name == "Clyde" || other.gameObject.name == "Inky")
        {
            ghostCollision();
        }
    }

    /*
     * void pelletCollision(Collider pellet)
     * Pacman has collided with a pellet, logic and
     * game object management takes place here.
     */
    void pelletCollision(Collider pellet)
    {
        score += 10;
        if (chomp.isPlaying == false)
        {
            chomp.Play();
        }

        //Subtract one pellet from the GameManager.
        GameManager.GMinstance.PelletsOnBoard -= 1;

        Destroy(pellet.gameObject);
        setScoreText();
    }

    /*
     * void energizerCollision(Collider energizer)
     * Pacman has collided with a energizer, logic and
     * game object management takes place here.
     */
    void energizerCollision(Collider energizer)
    {
        score += 40;
        setScoreText();
        GameManager.GMinstance.setGhostMode(false);

        Destroy(energizer.gameObject);
    }

    /*
     * void pacmanCollision()
     * Pacman has collided with a ghost, logic and
     * game object management takes place here.
     */
    void ghostCollision()
    {
        pacDie.Play();
    }

    /*
     * void setScoreText()
     * Sets the score in the UI of the game.
     */
    void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;

    public AudioClip menu, inGame, gameOver, clickButton, expAsteroid, expSpaceShip, bulletEnemy, bulletPlayer;

    public void playMenu()
    {
        source.clip = menu;
        source.Play();
    }

    public void playInGame()
    {
        source.clip = inGame;
        source.Play();
    }

    public void playClickButton()
    {
        source.clip = clickButton;
        source.Play();
    }

    public void playExpAsteroid()
    {
        source.clip = expAsteroid;
        source.Play();
    }

    public void playExpSpaceShip()
    {
        source.clip = expSpaceShip;
        source.Play();
    }

    public void playBulletPlayer()
    {
        source.clip = bulletPlayer;
        source.Play();
    }

    public void playBulletEnemy()
    {
        source.clip = bulletEnemy;
        source.Play();
    }

    public void playGameOver()
    {
        source.clip = gameOver;
        source.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerX : MonoBehaviour
{
    public AudioSource clickSound;

    public void Restart()
    {
        clickSound.Play();
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        clickSound.Play();
        SceneManager.LoadScene(0);
    }

    public void MenuGameOver()
    {
        clickSound.Play();
        BGMusic.instance.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        clickSound.Play();
        BGMusic.instance.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void CharacterSelection()
    {
        clickSound.Play();
        SceneManager.LoadScene(2);
    }
}

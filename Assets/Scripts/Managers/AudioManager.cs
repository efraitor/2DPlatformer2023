using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] soundEffects;

    //Referencias a la m�sica del juego
    public AudioSource bgm, levelEndMusic, bossMusic;

    //Hacemos el Singleton de este script
    public static AudioManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    //M�todo para reproducir los sonidos
    public void PlaySFX(int soundToPlay)
    {
        //Si ya se estaba reproduciendo este sonido, lo paramos
        soundEffects[soundToPlay].Stop();
        //Alteramos un poco el sonido cada vez que se vaya a reproducir
        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);
        //Reproducir el sonido pasado por par�metro
        soundEffects[soundToPlay].Play();
    }

    //M�todo para reproducir la m�sica del Boss Final
    public void PlayBossMusic()
    {
        //Paramos la m�sica de fondo
        bgm.Stop();
        //Reproducimos la m�sica del jefe
        bossMusic.Play();
    }
    //M�todo para reproducir la m�sica despu�s del Boss Final
    public void StopBossMusic()
    {
        //Paramos la m�sica del jefe
        bossMusic.Stop();
        //Reproducimos la m�sica de fondo
        bgm.Play();
    }
}

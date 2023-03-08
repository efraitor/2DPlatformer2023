using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] soundEffects;

    //Hacemos el Singleton de este script
    public static AudioManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    //Método para reproducir los sonidos
    public void PlaySFX(int soundToPlay)
    {
        //Si ya se estaba reproduciendo este sonido, lo paramos
        soundEffects[soundToPlay].Stop();
        //Reproducir el sonido pasado por parámetro
        soundEffects[soundToPlay].Play();
    }
}

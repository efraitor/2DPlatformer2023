using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    //Referencia al objeto de efecto de muerte del enemigo
    public GameObject deathEffect;

    //Método para la muerte del enemigo
    public void EnemyDeathController()
    {
        //Desactivamos al enemigo padre
        transform.gameObject.SetActive(false);
        //Instanciamos el efecto de muerte del enemigo en la posición del primer hijo
        Instantiate(deathEffect, transform.GetChild(0).position, transform.GetChild(0).rotation);
    }
}

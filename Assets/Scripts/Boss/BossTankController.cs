using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    //Lista de estados por los que puede pasar el jefe final (Máquina de estados)
    public enum bossStates { shooting, hurt, moving, ended };
    //Creamos una referencia al estado actual del jefe final
    public bossStates currentState;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Movement")]
    //Velocidad del jefe final
    public float moveSpeed;
    //Puntos entre los que se mueve el enemigo
    public Transform leftPoint, rightPoint;
    //Para conocer hacia donde se mueve
    private bool movingRight;
    //Referencia a la mina que deja caer el enemigo
    public GameObject mine;
    //Referencia al punto de caída de las minas
    public Transform minePoint;
    //Tiempo entre minas
    public float timeBetweenMines;
    //Contador de tiempo entre minas
    private float mineCounter;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Shooting")]
    //Referencia a los proyectiles del enemigo
    public GameObject bullet;
    //Punto desde el que salen los proyectiles
    public Transform firePoint;
    //Tiempo entre disparos
    public float timeBetweenShots;
    //Contador de tiempo entre disparos
    private float shotCounter;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Hurt")]
    //Tiempo de daño del enemigo
    public float hurtTime;
    //Contador de tiempo de daño
    private float hurtCounter;
    //Referencia a la zona de daño del jefe final
    public GameObject hitBox;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Health")]
    //Vida del enemigo
    public int health = 3;
    //Referencia al efecto de explosión del enemigo y a los objetos que aparecerán tras su muerte
    public GameObject explosion, winPlatform;
    //Variable para conocer si el enemigo ha sido derrotado
    private bool isDefeated;
    //Variables para controlar el tiempo entre disparos y entre minas
    public float shotSpeedUp, mineSpeedUp;

    //Posición del Boss
    public Transform theBoss;
    //Referencia al Animator del jefe final
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el Animator del jefe final
        anim = GetComponentInChildren<Animator>();
        //Inicializamos el estado en el que empieza el jefe final
        currentState = bossStates.shooting; //currentState = bossStates.shooting <=> currentState = 0
    }

    // Update is called once per frame
    void Update()
    {
        //En base a los cambios en el valor del enum generamos los cambios de estado
        switch (currentState)
        {
            //En el caso en el que currentState = 0
            case bossStates.shooting:

                //Hacemos decrecer el contador entre disparos
                shotCounter -= Time.deltaTime;

                //Si el contador de tiempo entre disparos se ha vaciado
                if (shotCounter <= 0)
                {
                    //Reiniciamos el contador de tiempo entre disparos
                    shotCounter = timeBetweenShots;
                    //Instanciamos la bala pero en una nueva referencia cada vez
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    //Como cada bala estará referenciada (será única) puedo aplicarle los cambios que queramos
                    //En este caso le diré a cada bala hacia donde debe apuntar según hacia donde mira el jefe final
                    newBullet.transform.localScale = theBoss.localScale;
                }

                break;
        }

    }
}

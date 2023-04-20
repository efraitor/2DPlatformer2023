using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    //Lista de estados por los que puede pasar el jefe final (M�quina de estados)
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
    //Referencia al punto de ca�da de las minas
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
    //Tiempo de da�o del enemigo
    public float hurtTime;
    //Contador de tiempo de da�o
    private float hurtCounter;
    //Referencia a la zona de da�o del jefe final
    public GameObject hitBox;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Health")]
    //Vida del enemigo
    public int health = 3;
    //Referencia al efecto de explosi�n del enemigo y a los objetos que aparecer�n tras su muerte
    public GameObject explosion, winPlatform;
    //Variable para conocer si el enemigo ha sido derrotado
    private bool isDefeated;
    //Variables para controlar el tiempo entre disparos y entre minas
    public float shotSpeedUp, mineSpeedUp;

    //Posici�n del Boss
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
                    //Como cada bala estar� referenciada (ser� �nica) puedo aplicarle los cambios que queramos
                    //En este caso le dir� a cada bala hacia donde debe apuntar seg�n hacia donde mira el jefe final
                    newBullet.transform.localScale = theBoss.localScale;
                }

                break;

            //En el caso en el que currentState = 1
            case bossStates.hurt:

                //Si el contador de tiempo de da�o a�n no est� vac�o
                if (hurtCounter > 0)
                {
                    //Hacemos decrecer el contador de tiempo de da�o
                    hurtCounter -= Time.deltaTime;

                    //Si el contador de tiempo de da�o se ha vaciado
                    if (hurtCounter <= 0)
                    {
                        //El jefe final pasar�a al estado de movimiento
                        currentState = bossStates.moving;
                        //Reiniciamos el contador de tiempo entre minas
                        mineCounter = 0;

                        //Si el enemigo ha sido derrotado
                        if (isDefeated)
                        {
                            //Desactivamos al tanque
                            theBoss.gameObject.SetActive(false);
                            //Instanciamos el efecto de explosi�n
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            //Activamos los objetos tras derrotar al jefe final
                            winPlatform.SetActive(true);
                            //Llamamos al m�tod que restaura la m�sica del juego
                            AudioManager.sharedInstance.StopBossMusic();
                            //El enemigo pasa al estado de muerte
                            currentState = bossStates.ended;
                        }
                    }
                }

                break;

            //En el caso en el que currentState = 2
            case bossStates.moving:

                //Si el enemigo se mueve a la derecha
                if (movingRight)
                {
                    //Movemos al enemigo a una velocidad hacia la derecha
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    //Si el enemigo ha llegado al punto de la derecha
                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        //Usando la escala puedo hacer que gire todo el objeto
                        theBoss.localScale = new Vector3(1f, 1f, 1f); //new Vector3(1, 1, 1) = Vector3.one
                        //Dejar� de moverse a la derecha
                        movingRight = false;
                        //Llamamos al m�todo que frena al enemigo
                        EndMovement();
                    }
                }
                //Si por el contrario el enemigo se mueve a la izquierda
                else
                {
                    //Movemos al enemigo a una velocidad hacia la izquierda
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    //Si el enemigo ha llegado al punto de la izquierda
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        //Usando la escala puedo hacer que gire todo el objeto
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        //Dejar� de moverse a la izquierda
                        movingRight = true;
                        //Llamamos al m�todo que frena al enemigo
                        EndMovement();
                    }
                }

                //Hacemos decrecer el contador de tiempo entre minas
                mineCounter -= Time.deltaTime;

                //Si el contador de tiempo entre minas se ha vaciado
                if (mineCounter <= 0)
                {
                    //Reiniciamos el contador de tiempo entre minas
                    mineCounter = timeBetweenMines;
                    //Instanciamos una mina
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }

                break;
        }

        //Para que este input solo funcione en el editor de Unity, no en la Build
#if UNITY_EDITOR
        //Si pulsamos la tecla H
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Llamamos al m�todo que hace da�o al jefe final
            TakeHit();
        }
#endif
    }

    //M�todo para cuando el jefe final recibe da�o
    public void TakeHit()
    {
        //El boss final cambiar� al estado de recibir da�o
        currentState = bossStates.hurt;
        //Inicialiamos el contador de tiempo de da�o
        hurtCounter = hurtTime;
        //Activamos el trigger de la animaci�n de da�o
        anim.SetTrigger("Hit");

        //Busca y mete en este momento todas las minas que hayan en la escena en ese array
        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        //Si el array de minas contiene alguna mina
        if (mines.Length > 0)
        {
            //Recorremos todo el array de minas
            foreach (BossTankMine foundMine in mines)
            {
                //Llamamos al m�todo que explota las minas
                foundMine.Explode();
            }
        }

        //Hacemos que el enemigo pierda una vida
        health--;
        //Si no le quedan vidas al enemigo
        if (health <= 0)
        {
            //El enemigo ha sido derrotado
            isDefeated = true;
        }
        //Si no ha sido derrotado
        else
        {
            //Disminuimos el tiempo entre disparos
            timeBetweenShots /= shotSpeedUp;
            //Disminuimos el tiempo entre minas
            timeBetweenMines /= mineSpeedUp;
        }

    }

    //M�todo para finalizar el movimiento del jefe final
    public void EndMovement()
    {
        //El enemigo pasar� al estado de ataque
        currentState = bossStates.shooting;
        //Ponemos a 0 o reiniciamos el contador de tiempo entre disparos
        shotCounter = 0f;
        //Inicializamos el contador de tiempo entre disparos
        shotCounter = timeBetweenShots;
        //Activamos el trigger de la animaci�n de parada de movimiento
        anim.SetTrigger("StopMoving");
        //Al terminar el movimiento volvemos a activar la HitBox del enemigo
        hitBox.SetActive(true);
    }
}

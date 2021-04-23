using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
  Este código controla la maquina de estados finita del agente, así mi
  indica en que estado se encuentra ejecutando únicamente dicho estado
*/

//Los estados en que puede estar el agente
public enum AgentState { Idle, Rotation, Attack}
public class TorretaEstatica : MonoBehaviour
{
    //Una variable para saber el estado del agente
    public AgentState currentState;

    //Una variable para saber que estado se esta ejecutando
    public InstruccionesFSM curState;

    [Header("Elementos del sensor")]
    public LayerMask playerMask;        //Para la detección del target a quien queremos detectar
    public float radiusDetection = 2f;  //Un radio de detección
    public Transform sensorPosition;    //La posición del sensor
    public bool playerDetected = false; //Un variable con dos posibles valores para saber si esta en rango el taget

    [Header("Elementos del agente")]
    public float speedRotation = 5f; //Velocidad de rotación
    public float timeIdle = 10f;     //Tiempo de espera
    public Vector3[] angles;        //Un arreglo de vectores que guarda los angulos que debe pas
    public int angleIndex;          //Una variable para recorrer el arreglo uno por uno

    public GameObject playerTarget;
    public GameObject prefabBullet;
    public Transform canion;

    public readonly EstadoEspera idleState = new EstadoEspera();
    public readonly EstadoRotacion rotateState = new EstadoRotacion();
    public readonly EstadoAtacar attackState = new EstadoAtacar();

    // Este metodo se manda llamar cuando se ejecuta el proyecto
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");

        TransitionToState(idleState); //Su primer estado es el Idle
    }

    // Este metodo se manda llamar cada frame durante la ejecución del proyecto
    void Update()
    {
        curState.UpdateState(this); //Le estamos dando el metodo de actualización del estado actual del agente
    }
    //Este metodo se utiliza para trabajar con físicas en Unity, se ejecuta cada frame
    private void FixedUpdate()
    {
        TargetDetected(); //Metodo para saber si se detecto al juagador, puede ser en cualquie
    }
    /*
        se utiliza un sensor para la detección del target haciendo uso de un colisionador que 
    */
    public virtual void TargetDetected()
    {
        Collider[] colliders = Physics.OverlapSphere(sensorPosition.position, radiusDetection, playerMask); //El colisionador guarda en un arreglo los objetos que este detectando y que tengan el layer
        if (colliders.Length == 0) //Si no hay objetos en los colisionadore
        {
            playerDetected = false; //No hay target
        }
        else //Si no
        {
            playerDetected = true; //El target ha sido detectado
        }
    }
    //Metodo para realizar la transicion de un estado a otro, se recibe de parametro el agente
    public void TransitionToState(InstruccionesFSM state)
    {
        curState = state;
        curState.EnterState(this);
    }
    //Metodo para ejecutar corutinas y nos permitan ejecutar ciertos procesos en un cantidad d
    //en este caso la espera para depues de rotar
    public void Coroutine(IEnumerator thisCoroutine)
    {
        StartCoroutine(thisCoroutine);
    }

    public void FireBullet()
    {
        Instantiate(prefabBullet, canion.position, canion.rotation);
    }

    //Metodo para poder visualizar el colisionador invisible que esta en el metodo TargetDetet
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sensorPosition.position, radiusDetection);
    }
}

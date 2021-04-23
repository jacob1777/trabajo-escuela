using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoEspera : InstruccionesFSM
{
    public override void EnterState(TorretaEstatica torreta)
    {
        Debug.Log("Inicio Estado de espera!");
        torreta.currentState = AgentState.Idle;
        torreta.Coroutine(Wait(torreta));
    }

    IEnumerator Wait(TorretaEstatica agent)
    {
        Debug.Log("Inicio conteo");
        yield return new WaitForSeconds(agent.timeIdle);
        ///Se ejecuta despues de X segundos
        Debug.Log("Cambio a estado de rotar!");
        agent.TransitionToState(agent.rotateState);
    }

    public override void UpdateState(TorretaEstatica torreta)
    {
        if(torreta.playerDetected == true)
        {
            torreta.StopAllCoroutines();
            torreta.TransitionToState(torreta.attackState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoRotacion : InstruccionesFSM
{
    public override void EnterState(TorretaEstatica torreta)
    {
        Debug.Log("Entro a estado de rotación");
        torreta.currentState = AgentState.Rotation;
    }

    public override void UpdateState(TorretaEstatica torreta)
    {
        if (torreta.playerDetected == true)
        {
            torreta.TransitionToState(torreta.attackState);
        }
        else
        {
            torreta.transform.rotation = Quaternion.Slerp(torreta.transform.rotation,
                Quaternion.Euler(torreta.angles[torreta.angleIndex]), Time.deltaTime * torreta.speedRotation);
            if (torreta.transform.eulerAngles.y >= (torreta.angles[torreta.angleIndex].y - 1))
            {
                Debug.Log("Llegue al angulo destino");
                torreta.angleIndex = (torreta.angleIndex + 1) % torreta.angles.Length;
                torreta.TransitionToState(torreta.idleState);
            }
        }
    }
}

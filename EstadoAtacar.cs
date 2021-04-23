using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAtacar : InstruccionesFSM
{
    float nextFire;
    float rateFire = 1f;

    public override void EnterState(TorretaEstatica torreta)
    {
        Debug.Log("Entro estado atacar!");
        torreta.currentState = AgentState.Attack;

    }

    public override void UpdateState(TorretaEstatica torreta) //comentario
    {
        if(torreta.playerDetected == true)
        {
            //comentariuo
            var lookRotation = Quaternion.LookRotation(torreta.playerTarget.transform.position - torreta.transform.position);
            //comentario
            torreta.transform.rotation = Quaternion.Slerp(torreta.transform.rotation, lookRotation, torreta.speedRotation * Time.deltaTime);

            //Disparar
            if(nextFire < Time.time)
            {
                torreta.FireBullet();
                nextFire = Time.time + rateFire;
            }
        }
        else
        {
            torreta.TransitionToState(torreta.idleState);
        }
    }
}
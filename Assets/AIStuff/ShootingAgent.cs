using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ShootingAgent : Agent
{
    public Transform shootingPoint;
    public int minStepsBetweenShots = 50;
    public int damage = 100;
    public Vector3 startingPosition;
    public int deathCountdownStart = 1000;

    private bool shotAvaliable = true;
    private int StepsUntilShotIsAvaliable = 0;
    private Rigidbody Rb;
    private int stepsUntilDeath;

    private void Shoot()
    {
        if (!shotAvaliable)
            return;

        int layerMask = 1 << LayerMask.NameToLayer("enemy");
        Vector3 direction = transform.forward;

        Debug.Log("Shot");
        Debug.DrawRay(shootingPoint.position, direction, Color.green, 2f);

        if (Physics.Raycast(shootingPoint.position, direction, out var hit, 200f, layerMask))
        {
            hit.transform.GetComponent<Enemy>().GetShot(damage, this);
        }

        shotAvaliable = false;
        StepsUntilShotIsAvaliable = minStepsBetweenShots;
    }

    private void FixedUpdate()
    {
        if (!shotAvaliable)
        {
            StepsUntilShotIsAvaliable--;

            if (StepsUntilShotIsAvaliable <= 0)
            {
                shotAvaliable = true;
            }
        }

        stepsUntilDeath++;

        if (deathCountdownStart <= stepsUntilDeath)
        {
            AddReward(-1.0f);
            EndEpisode();
        }

        RequestDecision();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.RoundToInt(vectorAction[0]) >= 1)
        {
            Shoot();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
    }

    public override void Initialize()
    {
        transform.position = startingPosition;
        Rb = GetComponent<Rigidbody>();
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetKey(KeyCode.P) ? 1f : 0f;
        //transform.rotation.SetLookRotation();
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode begin");
        transform.position = startingPosition;
        Rb.velocity = Vector3.zero;
        shotAvaliable = true;
    }

    public void registerKill() 
    {
        AddReward(1.0f);
        EndEpisode();
    }
}

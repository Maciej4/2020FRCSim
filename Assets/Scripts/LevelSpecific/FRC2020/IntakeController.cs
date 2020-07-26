using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntakeController : MonoBehaviour {
    public Transform ballContainer;
    public Transform nmfContainer;
    public Transform robot;
    public Rigidbody robotRigidbody;
    public List<GameObject> clip;

    public float upPower = 6f;
    public float fwdPower = 6f;
    public float shooterError = 0.5f;
    public Vector3 firePos = new Vector3(0f, 0.485f, -0.094f);

    public Vector3[] nmfPos = new Vector3[] {
        new Vector3(0.037f, 0.008f, 0.164f),
        new Vector3(0.168f, 0.008f, 0.012f),
        new Vector3(0.059f, 0.008f, -0.142f),
        new Vector3(-0.131f, 0.008f, -0.104f),
        new Vector3(-0.143f, 0.008f, 0.079f)
    };

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            clip.Add(null);
        }

        robotRigidbody = robot.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.StartsWith("Ball"))
        {
            for (int i = 0; i < 5; i++)
            {
                if (clip[i] is null)
                {
                    clip[i] = (other.gameObject);
                    clip[i].GetComponent<Rigidbody>().isKinematic = true;
                    clip[i].GetComponent<SphereCollider>().isTrigger = true;
                    clip[i].transform.SetParent(nmfContainer);
                    clip[i].transform.localPosition = nmfPos[i];
                    return;
                }
            }
        }
    }

    public void shoot(Transform ball) 
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        ball.SetParent(robot);
        ball.localPosition = firePos;
        ball.SetParent(ballContainer);
        ball.GetComponent<SphereCollider>().isTrigger = false;
        clip[clip.IndexOf(ball.gameObject)] = null;
        rb.isKinematic = false;
        rb.velocity = robotRigidbody.velocity;
        rb.AddForce(transform.up * upPower + transform.forward * fwdPower, ForceMode.Impulse);

        //Random added velocity
        rb.AddForce(transform.up * Random.Range(-shooterError,shooterError) + 
            transform.forward * Random.Range(-shooterError, shooterError) + 
            transform.right * Random.Range(-shooterError, shooterError),
            ForceMode.Impulse);

        //Random spin
        rb.AddTorque(Random.insideUnitCircle.normalized * 3f);
    }
}


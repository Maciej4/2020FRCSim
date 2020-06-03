using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntakeController : MonoBehaviour {
    public Transform ballContainer;
    public List<GameObject> clip;
    public float reloadStart = 0;
    public float reloadTime = 0.1f;
    public bool reloading = false;

    public float upPower = 300f;
    public float fwdPower = 300f;
    public float shooterError = 10f;
    public float distancePower;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.StartsWith("Ball") && clip.Count < 5)
        {
            clip.Add(other.gameObject);
            clip[clip.Count - 1].GetComponent<Rigidbody>().isKinematic = true;
            clip[clip.Count - 1].transform.SetParent(this.transform);
            if (clip.Count == 1)
            {
                clip[clip.Count - 1].transform.localPosition = new Vector3(0, 1.5f, -0.5f);
            }
            else
            {
                clip[clip.Count - 1].transform.localPosition = new Vector3(0.5f * (clip.Count - 1), 1.5f, -0.5f);
            }
        }
    }

    private void Update()
    {
        if (clip.Count != 0)
        {
            
            clip[clip.Count - 1].transform.rotation = transform.rotation;
            clip[clip.Count - 1].GetComponent<Rigidbody>().isKinematic = true;

            if (reloading && Time.time - reloadStart >= reloadTime)
            {
                reload();
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")))
                {
                    Rigidbody rb = clip[0].GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    clip[0].transform.SetParent(null);
                    clip.RemoveAt(0);
                    rb.AddForce(transform.up * upPower + transform.forward * fwdPower, ForceMode.Acceleration);
                    rb.AddForce(transform.up * Random.Range(-shooterError,shooterError) + 
                        transform.forward * Random.Range(-shooterError, shooterError) + 
                        transform.right * Random.Range(-shooterError, shooterError));
                    rb.AddTorque(Random.insideUnitCircle.normalized * 3f); //Random spin

                    reloadStart = Time.time;
                    reloading = true;
                }
                else if (Input.GetKeyDown(KeyCode.LeftBracket))
                {
                    Vector3 goalPos = new Vector3(-1.722f, 0.0f, -8.041f);
                    float positionAwayFromGoal = (float)(System.Math.Sqrt((System.Math.Pow(transform.position.x - goalPos.x, 2)) + (System.Math.Pow(transform.position.z - goalPos.z, 2))));
                    distancePower = positionAwayFromGoal  / 4.3f;
                    Debug.Log("distancePower: " + distancePower);
                    Rigidbody rb = clip[0].GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    clip[0].transform.SetParent(ballContainer);
                    clip.RemoveAt(0);
                    rb.AddForce(transform.up * upPower + transform.forward * fwdPower * distancePower, ForceMode.Acceleration);
                    rb.AddTorque(Random.insideUnitCircle.normalized * 3f); //Random spin

                    reloadStart = Time.time;
                    reloading = true;
                }
                else
                {
                    for (int i = clip.Count - 1; i >= 0; i--)
                    {
                        //Debug.Log("i: " + i + ", clipCount: " + clip.Count);
                        if (i == 0)
                        {
                            clip[i].transform.localPosition = new Vector3(0, 1.5f, -0.5f);
                        }
                        else
                        {
                            clip[i].transform.localPosition = new Vector3(0.5f * (i), 1.5f, -0.5f);
                        }
                    }
                }
            }
        }
    }

    public void reload()
    {
        reloading = false;

        int i = 0;

        foreach (GameObject tempBall in clip)
        {
            tempBall.transform.localPosition = new Vector3(0.5f * i, 1.5f, -0.5f);

            i++;
        }
    }
}


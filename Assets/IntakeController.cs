using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntakeController : MonoBehaviour {
    public List<GameObject> clip;
    public float reloadStart = 0;
    public float reloadTime = 0.1f;
    public bool reloading = false;

    public float upPower = 300f;
    public float fwdPower = 300f;
    public float shooterError = 28f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.StartsWith("Ball") && clip.Count < 5)
        {
            clip.Add(other.gameObject);
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


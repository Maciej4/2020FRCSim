using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public Transform telescope;
    public Transform hook;
    public Rigidbody hookRigidbody;
    public SpringJoint ropeSpringJoint;
    public LineRenderer ropeRenderer;
    public TelescopeBox telescopeBox;
    private FixedJoint hookJoint;

    void Start()
    {
        ropeSpringJoint.spring = 0f;
        ropeRenderer.enabled = false;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    ropeSpringJoint.maxDistance = 0.01f;
        //}

        if (telescopeBox.isHooked) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                telescopeBox.isHooked = false;
                ropeSpringJoint.spring = 0f;
                ropeSpringJoint.maxDistance = 1f;
                ropeRenderer.enabled = false;
                Destroy(hookJoint);
                hookJoint = null;
                hook.SetParent(telescope);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.name.Equals("ClimbBar") || telescopeBox.isHooked)
        {
            return;
        }

        telescopeBox.isHooked = true;
        ropeRenderer.enabled = true;
        hookJoint = hook.gameObject.AddComponent<FixedJoint>();
        hookJoint.connectedBody = other.GetComponent<Rigidbody>();
        hook.SetParent(null);
    }
}

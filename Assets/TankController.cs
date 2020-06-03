using UnityEngine;

public class TankController : MonoBehaviour
{
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    public float m_Speed = 4f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    public float m_sidewaysSpeed = 0.03f;
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_SidewaysInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.


    public ZMQClient zmqClient;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
        m_SidewaysInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }



    private void FixedUpdate()
    {
        if (!(zmqClient.zmqThread.robotPacket is null) && zmqClient.zmqThread.connectionStatus)
        {
            tankDrive(zmqClient.zmqThread.robotPacket.leftPower, zmqClient.zmqThread.robotPacket.rightPower);
            zmqClient.zmqThread.unityPacket.heading = this.transform.rotation.eulerAngles.y - 180f;
            
        }
        else
        {
            m_MovementInputValue = Input.GetAxis("Vertical");
            m_TurnInputValue = Input.GetAxis("Horizontal");
        }

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
        }
        
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }


    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime + transform.right * m_SidewaysInputValue * m_sidewaysSpeed;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

    private void tankDrive(double leftPower, double rightPower) 
    {
        m_MovementInputValue = (float)(leftPower - rightPower) * 0.5f;
        m_TurnInputValue = -(float)(leftPower + rightPower) * 0.5f;
    }
}
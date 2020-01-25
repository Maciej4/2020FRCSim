using UnityEngine;

public class TankController : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_Speed = 4f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    //public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
    public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.
    public float m_sidewaysSpeed = 0.03f;


    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_SidewaysInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.


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


    private void Update()
    {
        // Store the value of both input axes.
        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");
        /*m_SidewaysInputValue = Input.GetAxis("Sideways");*/

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
        }
    }



    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightBracket))
        {
            cheat();
        }
        else
        {
            // Adjust the rigidbodies position and orientation in FixedUpdate.
            Move();
            Turn();
        }
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

    private void cheat()
    {
        Vector3 shotPoint = new Vector3(-1.680274f, 0.3f, -2.951602f);
        Vector3 finalTarget = new Vector3(-1.722f, 0.3f, -8.120f);

        // Determine which direction to rotate towards
        Vector3 targetDirection = shotPoint - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = 2f * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        /*
        if (Quaternion.Dot(transform.rotation, Quaternion.LookRotation(newDirection)) <= 0.999999f && Vector3.Distance(transform.position, shotPoint) > 0.05f)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if (Vector3.Distance(transform.position, shotPoint) > 0.05f)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
            m_Rigidbody.MovePosition(m_Rigidbody.position + transform.forward * m_Speed * Time.deltaTime);
        }
        else
        {
            */
            Vector3 shotDirection = finalTarget - transform.position;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newShotDir = Vector3.RotateTowards(transform.forward, shotDirection, singleStep, 0.0f);

            transform.rotation = Quaternion.LookRotation(newShotDir);
        //}
        
    }
}
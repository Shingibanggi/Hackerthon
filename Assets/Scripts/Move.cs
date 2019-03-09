using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class Move : MonoBehaviour
{

    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_WalkSpeed;
    // [SerializeField] private float m_RunSpeed;
    // [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    //[SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_StickToGroundForce;
    [SerializeField] private float m_GravityMultiplier;
    [SerializeField] private MouseLook m_MouseLook;
    [SerializeField] private bool m_UseFovKick;
    [SerializeField] private FOVKick m_FovKick = new FOVKick();
    [SerializeField] private bool m_UseHeadBob;
    [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
    [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
    [SerializeField] private float m_StepInterval;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    [SerializeField] private AudioClip m_LandSound;

    public GameObject fade;
    public Slider outslider;
    public GameObject locker;

    public GameObject m_player;
    //private Transform m_player;
    //public GameObject reticle;
    //private Transform m_reticle;

    private Camera m_Camera;
    private float m_YRotation;
    private Vector2 m_Input;
    private Vector3 m_MoveDir = Vector3.zero;
    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;
    private bool m_PreviouslyGrounded;
    private Vector3 m_OriginalCameraPosition;
    private float m_StepCycle;
    private float m_NextStep;
    private AudioSource m_AudioSource;


    private Transform camTr;
    public static bool isStopped = true;
    public static bool inlocker;
    private float go_out;
    private float m_time;
    private bool wait;
    public static bool first = true;
 //   private float rotateSpeed = 3.0f;

    public PlayerHealth playerHealth;
    public AudioSource itemEating;

    public GameObject start;
    public GameObject tutorial1;
    public GameObject tutorial2;


    // Use this for initialization
    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();    //cc
        m_Camera = Camera.main;
        m_OriginalCameraPosition = m_Camera.transform.localPosition;
        m_FovKick.Setup(m_Camera);
        m_HeadBob.Setup(m_Camera, m_StepInterval);
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        m_AudioSource = GetComponent<AudioSource>();
        m_MouseLook.Init(transform, m_Camera.transform);
        camTr = m_Camera.GetComponent<Transform>();
        inlocker = false;
        wait = false;
        outslider.value = 0;
        fade.SetActive(false);
        //go_out = 30;
        //time = 30;

        //Debug.Log("false로 셋팅");
        //m_player = player.transform;
        //m_reticle = reticle.transform;

        playerHealth = FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
        itemEating = GetComponent<AudioSource>();

        start.gameObject.SetActive(true);

        tutorial1.gameObject.SetActive(true);
        Invoke("tutorial1.gameObject.SetActive(false)", 2.5f);

    }




    // Update is called once per frame
    private void Update()
    {
        if(inlocker)
        {
            m_time += Time.deltaTime;
            //if (m_time > 1f)
            //{ 
                if (outslider.value < 3f)
                    {
                        outslider.value += 0.01f;
                        m_time = 0;
                    }
                    else
                    {
                        fade.SetActive(false);
                        isStopped = false;
                        inlocker = false;
                        locker.SetActive(true);
                       // outslider.value = 0;
                        wait = true;
                        outslider.value = 0f;
                        m_player.SetActive(true);
                    }
            //}
            Debug.Log(inlocker);
            //go_out = (int)(time % 31);
            //go_out = time % 31;
            //time -= Time.deltaTime;
            //outslider.value = go_out;
       
            //if(outslider.value > 30)
            //{
            //    fade.SetActive(false);
            //    isStopped = false;
            //    inlocker = false;
            //    locker.SetActive(true);
            //    outslider.value = 0;
            //    wait = true;
            //}
        }

        if (first)
            return;

        if (isStopped)
            return;

        //RotateView();

        if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
        {
            StartCoroutine(m_JumpBob.DoBobCycle());
            PlayLandingSound();
            m_MoveDir.y = 0f;
        }
        if (!m_CharacterController.isGrounded && m_PreviouslyGrounded)
        {
            m_MoveDir.y = 0f;
        }

        m_PreviouslyGrounded = m_CharacterController.isGrounded;

    }


    private void PlayLandingSound()
    {
        m_AudioSource.clip = m_LandSound;
        m_AudioSource.Play();
        m_NextStep = m_StepCycle + .5f;
    }


    private void FixedUpdate()
    {

        if (first)
            return;

        if (isStopped)
            return;

        //RotateView();

        float speed;
        GetInput(out speed);
        // always move along the camera forward as it is the direction that it being aimed at
        //Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;
        Vector3 desiredMove = camTr.TransformDirection(Vector3.forward);

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.z = desiredMove.z * speed;


        if (m_CharacterController.isGrounded)
        {
            m_MoveDir.y = -m_StickToGroundForce;


        }
        else
        {
            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
        }
        m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        ProgressStepCycle(speed);
        UpdateCameraPosition(speed);



        //m_MouseLook.UpdateCursorLock();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            PlayerHealth.healed = true;
            other.gameObject.SetActive(false);
            itemEating.Play();

        }

        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);

            KeyImageChange.keyCount++;
            print("KeyCounting: " + KeyImageChange.keyCount);
        }

        if (other.gameObject.CompareTag("Locker"))
        {
            if (!wait)
            {
                AI_Example.isaware = false;
                locker = other.gameObject;
                locker.SetActive(false);
                fade.gameObject.SetActive(true);
                isStopped = true;
                inlocker = true;
                m_player.SetActive(false);
            }
            //   playerHealth.Healing();
        }

    }

    //private void PlayJumpSound()
    //{
    //    m_AudioSource.clip = m_JumpSound;
    //    m_AudioSource.Play();
    //}


    private void ProgressStepCycle(float speed)
    {
        if (isStopped)
            return;

        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * 1f)) *
                         Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio();
    }


    private void PlayFootStepAudio()
    {
        if (!m_CharacterController.isGrounded)
        {
            return;
        }
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        //        Debug.Log(n);
        //xDebug.Log(m_FootstepSounds);
        m_AudioSource.clip = m_FootstepSounds[n];
        //m_AudioSource.PlayOneShot(m_AudioSource.clip);
        m_AudioSource.Play();
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }


    private void UpdateCameraPosition(float speed)
    {

        if (isStopped)
            return;

        Vector3 newCameraPosition;
        if (!m_UseHeadBob)
        {
            return;
        }
        if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
        {
            m_Camera.transform.localPosition =
                m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                  (speed * 1f));
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
        }
        else
        {
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
        }
        m_Camera.transform.localPosition = newCameraPosition;
    }


    private void GetInput(out float speed)
    {

        // Read input
        //float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        //float vertical = CrossPlatformInputManager.GetAxis("Vertical");
        float horizontal = camTr.TransformDirection(Vector3.forward).x;
        float vertical = camTr.TransformDirection(Vector3.forward).y;

        bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
        // On standalone builds, walk/run speed is modified by a key press.
        // keep track of whether or not the character is walking or running
        m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
        // set the desired speed to be walking or running
        speed = m_WalkSpeed;
        m_Input = new Vector2(horizontal, vertical);
        // m_Input = new Vector2

        // normalize input if it exceeds 1 in combined length:
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }

        // handle speed change to give an fov kick
        // only if the player is going to a run, is running and the fovkick is to be used
        if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
        {
            StopAllCoroutines();
            StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
        }
    }




    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isStopped)
            return;

        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (m_CollisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }





    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Locker"))
        {
            wait = false;
        }
    }


}

//    public float speed = 1.0f;
//  public float damping = 3.0f;

//  private Transform tr;
//  private Transform camTr;
//  private CharacterController cc;

//  void Start () {
//      tr = GetComponent<Transform>();
//      camTr = Camera.main.GetComponent<Transform>();
//      cc = GetComponent<CharacterController>();

//  }

//  // Update is called once per frame
//  void Update () {
//      MoveLookAt();
//  }

//  void MoveLookAt(){

//      Vector3 dir = camTr.TransformDirection(Vector3.forward);
//      cc.SimpleMove(dir*speed);
//  }

//}


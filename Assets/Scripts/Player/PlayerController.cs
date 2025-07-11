using SceneTransition;
using UnityEngine;

[AddComponentMenu("Player/Top down Controller")]
public class PlayerController : Singleton<PlayerController>
{
    /* Player controller internal data */
    private PlayerStateMachine stateMachine;
    private PlayerDirection direction;


    /* Player modules */
    public PlayerMovement Movement { get; private set; }
    public PlayerAnimationController AnimationController { get; private set; }
    public PlayerInputManager InputManager { get; private set; }
    public PlayerCameraController CameraController { get; private set; }
    public PlayerInteractions Interactions { get; private set; }
    public PlayerSockLauncher SockLauncher { get; private set; }

    public PlayerMeleeWeapon MeleeWeapon { get; private set; }

    /* Component references */
    public Collider2D ColliderComponent { get; private set; }
    public Rigidbody2D RigidBodyComponent { get; private set; }
    public Animator AnimatorComponent { get; private set; }
    public SpriteRenderer SpriteRendererComponent { get; private set; }

    private void Awake()
    {
        InitialisePlayerData();
        FetchUnityComponentes();
        FetchPlayerModules();
    }

    private void Start()
    {
        InitialiseCameraController();
        DisableCharacterControllerOnTransition();
    }

    private void Update()
    {
        UpdatePlayerInput();

        ManagePlayerSockLauncher();
        ManageInteractions();

        PlayerBeginAttack();

        UpdateCurrentCharacterState();
        UpdateCharacterDirection();

        UpdateAnimation();
    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }

    private void FixedUpdate()
    {
        ManagePlayerMeleeAtack();
        ManageMovement();
    }

    /* Awake */
    private void InitialisePlayerData()
    {
        stateMachine = new PlayerStateMachine();
        direction = new PlayerDirection();
    }

    private void FetchUnityComponentes()
    {
        /* Initialise components */
        ColliderComponent = GetComponentInChildren<Collider2D>();
        RigidBodyComponent = GetComponentInChildren<Rigidbody2D>();
        AnimatorComponent = GetComponentInChildren<Animator>();
        SpriteRendererComponent = GetComponentInChildren<SpriteRenderer>();
    }

    private void FetchPlayerModules()
    {
        /* Initialise Player modules */
        Movement = GetComponentInChildren<PlayerMovement>();
        AnimationController = GetComponentInChildren<PlayerAnimationController>();
        InputManager = GetComponentInChildren<PlayerInputManager>();
        CameraController = GetComponentInChildren<PlayerCameraController>();
        Interactions = GetComponentInChildren<PlayerInteractions>();

        SockLauncher = GetComponentInChildren<PlayerSockLauncher>();
        MeleeWeapon = GetComponentInChildren<PlayerMeleeWeapon>();
    }

    /* Start */
    private void InitialiseCameraController()
    {
        CameraController.InitialiseCameraController(transform.position);
    }

    private void DisableCharacterControllerOnTransition()
    {
        if (!SceneTransitionManager.instance.IsTransitionComplete()) {

            void ReEnableMovement()
            {
                Movement.enabled = true;
            }

            Movement.enabled = false;
            SceneTransitionManager.instance.onTransitionEndOnce += ReEnableMovement;
        }
    }

    /* Update */
    private void UpdatePlayerInput()
    {
        Movement.UpdatePlayerInput(InputManager.GetLeftStick());
    }

    private void UpdateCurrentCharacterState()
    {
        Vector2 movementSpeed = RigidBodyComponent.linearVelocity;


        if(movementSpeed.sqrMagnitude > 0.05)
        {
            stateMachine.state = PlayerStateMachine.State.Run;
        }
        else
        {
            stateMachine.state = PlayerStateMachine.State.Iddle;

        }
    }

    private void UpdateCharacterDirection()
    {
        Movement.UpdateMovementDirection(direction);
    }

    private void UpdateAnimation()
    {
        AnimationController.UpdateAnimation(AnimatorComponent, SpriteRendererComponent, stateMachine, direction);
    }

    private void ManagePlayerSockLauncher()
    {
        if (SockLauncher.CanShoot(InputManager.WantsToShoot())){

            SockLauncher.Shoot(Movement.LastDirectionalInput());
        }
    }

    private void ManageInteractions()
    {
        if (Interactions.CanInteract(InputManager.WantsToInteract())){

            Interactions.Interact();
        }
    }


    private void PlayerBeginAttack()
    {
        MeleeWeapon.BeginAttack(InputManager.WantsToAttack(), Movement.LastDirectionalInput());
    }

    private void ManagePlayerMeleeAtack()
    {
        MeleeWeapon.UpdateAttack();
    }

    /* Fixed Update */
    private void ManageMovement()
    {
        Movement.ApplyMovement(RigidBodyComponent);
    }

    /* Late Update */

    private void UpdateCameraPosition()
    {
        CameraController.UpdateCameraPosition(transform.position, InputManager.GetLeftStick());
    }


    public void SetAllControls(bool value)
    {
        Movement.enabled = value;
        SockLauncher.enabled = value;
    }
}

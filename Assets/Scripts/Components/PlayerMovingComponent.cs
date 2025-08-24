using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;


using StateType = StateComponent.StateType;

public enum EvadeDirection
{
	Forward = 0, Backward, Left, Right,
}

public class PlayerMovingComponent : MonoBehaviour
{
	[SerializeField]
	private float walkSpeed = 2.0f;

	[SerializeField]
	private float runSpeed = 4.0f;

	[SerializeField]
	private float sensitivity = 10.0f;

	[SerializeField]
	private float deadZone = 0.001f;

	[SerializeField]
	private string followTargetName = "FollowTarget";

	[SerializeField]
	private Vector2 mouseSensitivity = new Vector2(0.1f, 0.1f);

	[SerializeField]
	private Vector2 limitPitchAngle = new Vector2(65, 329);

	[SerializeField]
	private float mouseRotationLerp = 0.25f;


	private bool bCanMove = true;

	private Animator animator;
	private WeaponComponent weapon;
	private StateComponent state;


	private bool bRun;

	private Vector2 inputMove;
	public Vector2 MoveValue { get => inputMove; }
	private Vector2 currInputMove;

	private Vector2 inputLook;


    public void Move()
    {
		bCanMove = true;
    }

	public void Stop()
	{
		bCanMove = false;
	}
	
	private Transform followTargetTransform;

	private void Awake()
    {
		animator = GetComponent<Animator>();
		weapon = GetComponent<WeaponComponent>();
		
		state = GetComponent<StateComponent>();
		state.OnStateTypeChanged += OnStateTypeChanged;

		Awake_BindPlayerInput();
	}

	private void Awake_BindPlayerInput()
    {
		PlayerInput input = GetComponent<PlayerInput>();
		InputActionMap actionMap = input.actions.FindActionMap("Player");

		InputAction moveAction = actionMap.FindAction("Move");
		moveAction.performed += Input_Move_Performed;
		moveAction.canceled += Input_Move_Cancled;

		InputAction lookAction = actionMap.FindAction("Look");
		lookAction.performed += Input_Look_Performed;
		lookAction.canceled += Input_Look_Cancled;


		InputAction runAction = actionMap.FindAction("Run");
		runAction.started += Input_Run_Started;
        runAction.canceled += Input_Run_Cancled;
        
		actionMap.FindAction("Evade").started += context =>
		{
			if (weapon.UnarmedMode == false)
				return;

			if (state.IdleMode == false)
				return;

			state.SetEvadeMode();
		};
	}

    private void Input_Move_Performed(InputAction.CallbackContext context)
    {
		inputMove = context.ReadValue<Vector2>();
	}

	private void Input_Move_Cancled(InputAction.CallbackContext context)
	{
		inputMove = Vector3.zero;
	}

	private void Input_Run_Started(InputAction.CallbackContext context)
	{
		bRun = true;
	}

	private void Input_Run_Cancled(InputAction.CallbackContext context)
	{
		bRun = false;
	}


	private void Input_Look_Performed(InputAction.CallbackContext context)
    {
		inputLook = context.ReadValue<Vector2>();
	}

	private void Input_Look_Cancled(InputAction.CallbackContext context)
	{
		inputLook = Vector2.zero;
	}

	private void Start()
    {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		followTargetTransform = transform.FindChildByName(followTargetName);
    }


    private Vector2 velocity;
	private Quaternion rotation;
	public Quaternion Rotation { set => rotation = value; }

	private void Update()
    {
		currInputMove = Vector2.SmoothDamp(currInputMove, inputMove, ref velocity, 1.0f / sensitivity);

		if (bCanMove == false)
			return;
		
		rotation *= Quaternion.AngleAxis(inputLook.x * mouseSensitivity.x, Vector3.up);
		rotation *= Quaternion.AngleAxis(-inputLook.y * mouseSensitivity.y, Vector3.right);
		followTargetTransform.rotation = rotation;


		Vector3 angles = followTargetTransform.localEulerAngles;
		angles.z = 0.0f;

		
		float xAngle = followTargetTransform.localEulerAngles.x;

		if (xAngle < 180.0f && xAngle > limitPitchAngle.x)
			angles.x = limitPitchAngle.x;
		else if (xAngle > 180.0f && xAngle < limitPitchAngle.y)
			angles.x = limitPitchAngle.y;		

		followTargetTransform.localEulerAngles = angles;

		rotation = Quaternion.Lerp(followTargetTransform.rotation, rotation, mouseRotationLerp * Time.deltaTime);

		transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
		
		followTargetTransform.localEulerAngles = new Vector3(angles.x, 0, 0);


		Vector3 direction = Vector3.zero;

		float speed = bRun ? runSpeed : walkSpeed;
		if (currInputMove.magnitude > deadZone)
        {
			direction = (Vector3.right * currInputMove.x) + (Vector3.forward * currInputMove.y);
			direction = direction.normalized * speed;
		}

		transform.Translate(direction * Time.deltaTime);
		//controller.Move(direction * Time.deltaTime);

		
		if(weapon.UnarmedMode)
        {
			animator.SetFloat("SpeedY", direction.magnitude);

			return;
		}

		animator.SetFloat("SpeedX", currInputMove.x * speed);
		animator.SetFloat("SpeedY", currInputMove.y * speed);
	}

	
	private Quaternion? evadeRotation = null;

	private void OnStateTypeChanged(StateType prevType, StateType newType)
	{
		switch (newType)
		{
			case StateType.Evade:
			{
				Vector2 value = MoveValue;

				EvadeDirection direction = EvadeDirection.Forward;
				if (value.y == 0.0f)
				{
					direction = EvadeDirection.Forward;

					if (value.x < 0.0f)
						direction = EvadeDirection.Left;
					else if (value.x > 0.0f)
						direction = EvadeDirection.Right;

				}
				else if (value.y >= 0.0f)
				{
					direction = EvadeDirection.Forward;

					if (value.x < 0.0f)
					{
						evadeRotation = transform.rotation;
						transform.Rotate(Vector3.up, -45.0f);
					}
					else if (value.x > 0.0f)
					{
						evadeRotation = transform.rotation;
						transform.Rotate(Vector3.up, +45.0f);
					}
				}
				else
				{
					direction = EvadeDirection.Backward;
				}

				animator.SetInteger("Direction", (int)direction);
				animator.SetTrigger("Evade");
			}
			return;
		}
	}

	private void End_Evade()
	{
		state.SetIdleMode();
	}
}
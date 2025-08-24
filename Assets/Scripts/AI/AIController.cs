using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PerceptionComponent))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class AIController : MonoBehaviour
{
    [SerializeField]
    protected float attackRange = 1.5f;

    [SerializeField]
    private float attackDelay = 2.0f;

    [SerializeField]
    private float attackDelayRandom = 0.5f;

    [SerializeField]
    private float damagedDelay = 1.5f;

    [SerializeField]
    private float damagedDelayRandom = 0.5f;


    // [SerializeField]
    // private string uiStateName = "EnemyAIState";


    protected abstract void FixedUpdate();


    public enum Type
    {
        Wait = 0, Patrol, Approach, Equip, Action, Damaged,
    }
    private Type type = Type.Wait;

    public event Action<Type, Type> OnAIStateTypeChanged;

    public bool WaitMode { get => type == Type.Wait; }
    public bool PatrolMode { get => type == Type.Patrol; }
    public bool ApproachMode { get => type == Type.Approach; }
    public bool EquipMode { get => type == Type.Equip; }
    public bool ActionMode { get => type == Type.Action; }
    public bool DamagedMode { get => type == Type.Damaged; }


    protected PerceptionComponent perception;
    protected NavMeshAgent navMeshAgent;
    private Animator animator;
    protected WeaponComponent weapon;

    private TextMeshProUGUI userInterface;
    private Canvas uiStateCanvas;

    [SerializeField]
    private float currentCoolTime = 0.0f;

    protected virtual void Awake()
    {
        perception = GetComponent<PerceptionComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        weapon = GetComponent<WeaponComponent>();
        weapon.OnEndEquip += OnEndEquip;
        weapon.OnEndDoAction += OnEndDoAction;
    }

    private void Start()
    {
        //uiStateCanvas = UIHelpers.CreateBillboardCanvas(uiStateName, transform, Camera.main);

        // Transform t = uiStateCanvas.transform.FindChildByName("Text_AIState");
        // userInterface = t.GetComponent<TextMeshProUGUI>();

        SetWaitMode();
    }

    private void Update()
    {
        if (uiStateCanvas == null)
            return;


        //string str = "";
        //str += type.ToString();
        //str += $"({currentCoolTime.ToString("f2")})";

        //userInterface.text = str;
        //uiStateCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    protected bool CheckCoolTime()
    {
        if (WaitMode == false)
            return false;

        if (currentCoolTime <= 0.0f)
            return false;


        currentCoolTime -= Time.fixedDeltaTime;


        bool bCheckCoolTimeZero = false;
        bCheckCoolTimeZero |= (currentCoolTime <= 0.0f);
        bCheckCoolTimeZero |= (perception.GetPercievedPlayer() == null);

        if (bCheckCoolTimeZero)
        {
            currentCoolTime = 0.0f;

            return false;
        }

        return true;
    }

    protected bool CheckMode()
    {
        bool bCheck = false;
        bCheck |= (EquipMode == true);
        bCheck |= (ActionMode == true);
        bCheck |= (DamagedMode == true);

        return bCheck;
    }

    private void LateUpdate()
    {
        LateUpdate_SetSpeed();
        LateUpdate_Approach();
    }

    private void LateUpdate_SetSpeed()
    {
        switch(type)
        {
            case Type.Wait:
            case Type.Action:
            case Type.Damaged:
            {
                animator.SetFloat("SpeedY", 0.0f);
            }
            break;

            case Type.Patrol:
            case Type.Approach:
            {
                animator.SetFloat("SpeedY", navMeshAgent.velocity.magnitude);
            }
            break;
        }
    }

    private void LateUpdate_Approach()
    {
        if (ApproachMode == false)
            return;


        GameObject player = perception.GetPercievedPlayer();

        if (player == null)
            return;


        navMeshAgent.SetDestination(player.transform.position);
    }

    protected void SetWaitMode()
    {
        if (WaitMode == true)
            return;

        navMeshAgent.isStopped = true;
        ChangeType(Type.Wait);
    }

    protected void SetApproachMode()
    {
        if (ApproachMode == true)
            return;

        navMeshAgent.isStopped = false;
        ChangeType(Type.Approach);
    }

    protected void SetEquipMode(WeaponType type)
    {
        if (EquipMode == true)
            return;

        ChangeType(Type.Equip);
        //navMeshAgent.isStopped = true;


        switch(type)
        {
            case WeaponType.Sword: weapon.SetSwordMode(); break;
            case WeaponType.FireBall: weapon.SetFireBallMode(); break;
            case WeaponType.Warp: weapon.SetWarpMode(); break;

            default: Debug.Assert(false); break;
        }
    }

    protected void SetActionMode()
    {
        if (ActionMode == true)
            return;

        navMeshAgent.isStopped = true;
        ChangeType(Type.Action);

        
        GameObject player = perception.GetPercievedPlayer();
        
        if (player != null)
            transform.LookAt(player.transform);

        weapon.DoAction();
    }

    public void SetDamageMode()
    {
        if(EquipMode == true)
        {
            animator.Play("Arms", 1);

            if (weapon.IsEquippingMode() == false)
                weapon.Begin_Equip();

            weapon.End_Equip();
        }

        if(ActionMode == true)
        {
            animator.Play($"{weapon.Type}.Blend Tree", 0);

            if (animator.GetBool("IsAction") == true)
                weapon.End_DoAction();
        }


        bool bCancledCoolTime = false;
        bCancledCoolTime |= EquipMode;
        bCancledCoolTime |= ApproachMode;
        bCancledCoolTime |= (perception.GetPercievedPlayer() == null);

        if (bCancledCoolTime == true)
            currentCoolTime = -1.0f;


        if (DamagedMode == true)
            return;

        navMeshAgent.isStopped = true;
        ChangeType(Type.Damaged);
    }

    protected void ChangeType(Type type)
    {
        Type prevType = this.type;
        this.type = type;

        OnAIStateTypeChanged?.Invoke(prevType, type);
    }

    public void End_Damage()
    {
        SetCoolTime(damagedDelay, damagedDelayRandom);
        
        SetWaitMode();
    }

    private void OnEndEquip()
    {
        SetWaitMode();
    }

    private void OnEndDoAction()
    {
        SetCoolTime(attackDelay, attackDelayRandom);

        SetWaitMode();
    }

    private void SetCoolTime(float delayTime, float randomTime)
    {
        if(currentCoolTime < 0.0f)
        {
            currentCoolTime = 0.0f;

            return;
        }


        float time = 0.0f;
        time += delayTime;
        time += UnityEngine.Random.Range(-randomTime, +randomTime);

        currentCoolTime = time;
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (Selection.activeGameObject != gameObject)
            return;

        GUILayout.Label($"{gameObject.name} / {type}");
    }
#endif
}
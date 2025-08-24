using Cinemachine;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Melee : Weapon
{
	private bool bEnable;
	private bool bExist;
	private int index;

    protected Collider[] colliders;
    private List<GameObject> hittedList;

    protected CinemachineImpulseSource impulse;
    protected CinemachineBrain brain;

    private PlayerMovingComponent moving;

    protected override void Awake()
    {
        base.Awake();

        colliders = GetComponentsInChildren<Collider>();
        hittedList = new List<GameObject>();

        impulse = GetComponent<CinemachineImpulseSource>();
        brain = Camera.main.GetComponent<CinemachineBrain>();

        moving = rootObject.GetComponent<PlayerMovingComponent>();
    }

    protected override void Start()
    {
        base.Start();

        End_Collision();
    }

    public virtual void Begin_Collision(AnimationEvent e)
    {
        foreach (Collider collider in colliders)
            collider.enabled = true;
    }

    public virtual void End_Collision()
    {
        foreach (Collider collider in colliders)
            collider.enabled = false;

        hittedList.Clear();
    }

    public void Begin_Combo()
    {
		bEnable = true;
    }

	public void End_Combo()
	{
		bEnable = false;
	}

    public override void DoAction()
    {
        if(bEnable)
        {
            bEnable = false;
            bExist = true;

            return;
        }


        if (state.IdleMode == false)
            return;

        base.DoAction();
    }

    public override void Begin_DoAction()
    {
        base.Begin_DoAction();

        if (bExist == false)
            return;


        bExist = false;

        index++;
        animator.SetTrigger("NextCombo");

        
        if (doActionDatas[index].bCanMove)
        {
            Move();

            return;
        }

        CheckStop(index);
    }

    public override void End_DoAction()
    {
        base.End_DoAction();

        index = 0;
        bEnable = false;
    }

    public virtual void Play_Impulse()
    {
        if (impulse == null)
            return;

        if (doActionDatas[index].ImpulseSettings == null)
            return;

        if (doActionDatas[index].ImpulseDirection.magnitude <= 0.0f)
            return;

        CinemachineVirtualCamera camera = brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if(camera != null)
        {
            CinemachineImpulseListener listener = camera.GetComponent<CinemachineImpulseListener>();
            listener.m_ReactionSettings.m_SecondaryNoise = doActionDatas[index].ImpulseSettings;
        }

        impulse.GenerateImpulse(doActionDatas[index].ImpulseDirection);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == rootObject)
            return;


        if (hittedList.Contains(other.gameObject) == true)
            return;

        hittedList.Add(other.gameObject);

		UnityEngine.Debug.Log("충돌 객체 : " + other.gameObject, other.gameObject);

		IDamagable damage = other.gameObject.GetComponent<IDamagable>();

        if (damage == null)
            return;


        Vector3 hitPoint = Vector3.zero;

        Collider enabledCollider = null;
        foreach(Collider collider in colliders)
        {
            if (collider.enabled == true)
            {
                enabledCollider = collider;

                break;
            }
        }
        
        
        hitPoint = enabledCollider.ClosestPoint(other.transform.position);
        hitPoint = other.transform.InverseTransformPoint(hitPoint);

        if(moving != null)
        {
            Vector3 direction = other.gameObject.transform.position - rootObject.transform.position;
            Quaternion q = Quaternion.FromToRotation(rootObject.transform.forward, direction.normalized);

            rootObject.transform.rotation *= Quaternion.Euler(0, q.eulerAngles.y, 0);
            moving.Rotation = rootObject.transform.rotation;
        }


        damage.OnDamage(rootObject, this, hitPoint, doActionDatas[index]);
    }
}
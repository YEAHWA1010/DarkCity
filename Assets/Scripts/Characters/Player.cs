using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovingComponent))]
public class Player : Character, IDamagable
{
    protected override void Awake()
    {
        base.Awake();

        PlayerInput input = GetComponent<PlayerInput>();
        InputActionMap actionMap = input.actions.FindActionMap("Player");

        actionMap.FindAction("Fist").started += context =>
        {
            weapon.SetFistMode();
        };

        actionMap.FindAction("Sword").started += context =>
        {
            weapon.SetSwordMode();
        };

        actionMap.FindAction("Hammer").started += context =>
        {
            weapon.SetHammerMode();
        };

        actionMap.FindAction("FireBall").started += context =>
        {
            weapon.SetFireBallMode();
        };

        actionMap.FindAction("Warp").started += context =>
        {
            weapon.SetWarpMode();
        };


        actionMap.FindAction("Action").started += context =>
        {
            weapon.DoAction();
        };
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            healthPoint.Damage(20);
    }

    public void OnDamage(GameObject attacker, Weapon causer, Vector3 hitPoint, DoActionData data)
    {
        healthPoint.Damage(data.Power);

        //StartCoroutine(Change_Color(changeColorTime));
        MovableStopper.Instance.Start_Delay(data.StopFrame);
        
        if (healthPoint.Dead == false)
            transform.LookAt(attacker.transform, Vector3.up);

        if(data.HitParticle != null)
        {
            GameObject obj = Instantiate<GameObject>(data.HitParticle, transform, false);
            obj.transform.localPosition = hitPoint + data.HitParticlePositionOffset;
            obj.transform.localScale = data.HitParticleScaleOffset;
        }

        if(healthPoint.Dead == false)
        {
            //aiController?.SetDamageMode();
            state.SetDamagedMode();

            animator.SetInteger("ImpactType", (int)causer.Type);
            animator.SetInteger("ImpactIndex", data.HitImpactIndex);
            animator.SetTrigger("Impact");

            rigidbody.isKinematic = false;

            float launch = rigidbody.drag * data.Distance * 10.0f;
            rigidbody.AddForce(-transform.forward * launch);

            StartCoroutine(Change_IsKinemetics(30));

            return;
        }

        
        state.SetDeadMode();

        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        animator.SetTrigger("Dead");

        Destroy(gameObject, 5);
    }
    private void OnGUI()
    {
        
    }
    
    private IEnumerator Change_IsKinemetics(int frame)
    {
        for (int i = 0; i < frame; i++)
            yield return new WaitForFixedUpdate();

        rigidbody.isKinematic = true;
    }
}
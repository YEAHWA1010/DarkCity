using UnityEngine;

public class AIController_Patrol : AIController
{
    private PatrolComponent patrol;

    protected override void Awake()
    {
        base.Awake();

        patrol = GetComponent<PatrolComponent>();
    }

    private void SetPatrolMode()
    {
        if (PatrolMode == true)
            return;

        ChangeType(Type.Patrol);

        navMeshAgent.isStopped = false;
        patrol.StartMove();
    }

    protected override void FixedUpdate()
    {
        if (CheckCoolTime())
            return;

        if (CheckMode())
            return;


        GameObject player = perception.GetPercievedPlayer();

        if (player == null)
        {
            if (weapon.UnarmedMode == false)
                weapon.SetUnarmedMode();


            if (patrol == null)
            {
                SetWaitMode();

                return;
            }

            SetPatrolMode();

            return;
        }

        if (weapon.UnarmedMode == true)
        {
            SetEquipMode(WeaponType.Sword);

            return;
        }


        float temp = Vector3.Distance(transform.position, player.transform.position);
        if (temp < attackRange)
        {
            if (weapon.SwordMode)
                SetActionMode();

            return;
        }


        SetApproachMode();
    }
}
using UnityEngine;

public class Warp : Weapon
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float traceDistance = 100;

    [SerializeField]
    private GameObject cursorPrefab;
    private GameObject cursorObject;

    private AIController aiController;

    protected override void Reset()
    {
        base.Reset();

        type = WeaponType.Warp;
    }

    protected override void Awake()
    {
        base.Awake();

        aiController = rootObject.GetComponent<AIController>();
    }

    protected override void Start()
    {
        base.Start();

        if (aiController != null)
            return;

        if(cursorPrefab != null)
        {
            cursorObject = Instantiate<GameObject>(cursorPrefab);
            WorldCursor cursor = cursorObject.GetComponent<WorldCursor>();
            cursor.TraceDistance = traceDistance;
            cursor.Mask = layerMask;
            cursorObject.SetActive(false);
        }
    }

    protected override void Update()
    {
        base.Update();

        bool bCheck = false;
        bCheck |= (bEquipped == false);
        bCheck |= (cursorObject == null);

        if (bCheck)
            return;


        bCheck = true;
        bCheck &= CameraHelpers.GetCusorLocation(traceDistance, layerMask);
        bCheck &= state.ActionMode == false;

        cursorObject.SetActive(bCheck);
    }

    public override void End_Equip()
    {
        base.End_Equip();

        cursorObject?.SetActive(true);
    }

    public override void UnEquip()
    {
        base.UnEquip();

        cursorObject?.SetActive(false);
    }

    public override void Play_Particle()
    {
        base.Play_Particle();

        
    }

    private Vector3 moveToPosition;
    public Vector3 MoveToPosition { set => moveToPosition = value; }

    public override bool CanDoAction()
    {
        if (base.CanDoAction() == false)
            return false;

        if(aiController == null)
        {
            if (cursorObject == null)
                return false;

            bool bCheck = CameraHelpers.GetCusorLocation(out moveToPosition, traceDistance, layerMask);
            return bCheck;
        }

        return true;
    }

    public override void DoAction()
    {
        base.DoAction();
    }

    public override void Begin_DoAction()
    {
        base.Begin_DoAction();

        rootObject.transform.position = moveToPosition;
    }

    public override void End_DoAction()
    {
        base.End_DoAction();

        moveToPosition = Vector3.zero;
    }
}
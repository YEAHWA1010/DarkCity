using System;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
	Unarmed = 0, Fist, Sword, Hammer, FireBall, Warp, Max,
}

public class WeaponComponent : MonoBehaviour
{
	[SerializeField]
	private GameObject[] originPrefabs;


	private Animator animator;
	private StateComponent state;


	private WeaponType type = WeaponType.Unarmed;
	public WeaponType Type { get => type; }


	public event Action<WeaponType, WeaponType> OnWeaponTyeChanged;

	public event Action OnEndEquip;
	public event Action OnEndDoAction;


	public bool UnarmedMode { get => type == WeaponType.Unarmed; }
	public bool FistMode { get => type == WeaponType.Fist; }
	public bool SwordMode { get => type == WeaponType.Sword; }
	public bool HammerMode { get => type == WeaponType.Hammer; }
	public bool FireBallMode { get => type == WeaponType.FireBall; }
	public bool WarpMode { get => type == WeaponType.Warp; }

	public bool IsEquippingMode()
    {
		if (UnarmedMode)
			return false;

		Weapon weapon = weaponTable[type];
		if (weapon == null)
			return false;

		return weapon.Equipping;
    }	


	private Dictionary<WeaponType, Weapon> weaponTable;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		state = GetComponent<StateComponent>();
	}

	private void Start()
	{
		weaponTable = new Dictionary<WeaponType, Weapon>();

		for (int i = 0; i < (int)WeaponType.Max; i++)
			weaponTable.Add((WeaponType)i, null);


		for (int i = 0; i < originPrefabs.Length; i++)
		{
			GameObject obj = Instantiate<GameObject>(originPrefabs[i], transform);
			Weapon weapon = obj.GetComponent<Weapon>();
			obj.name = weapon.Type.ToString();

			weaponTable[weapon.Type] = weapon;
		}
	}

	public void SetFistMode()
	{
		if (state.IdleMode == false)
			return;

		SetMode(WeaponType.Fist);
	}

	public void SetSwordMode()
	{
		if (state.IdleMode == false)
			return;

		SetMode(WeaponType.Sword);
	}

	public void SetHammerMode()
	{
		if (state.IdleMode == false)
			return;

		SetMode(WeaponType.Hammer);
	}

	public void SetFireBallMode()
	{
		if (state.IdleMode == false)
			return;

		SetMode(WeaponType.FireBall);
	}

	public void SetWarpMode()
	{
		if (state.IdleMode == false)
			return;

		SetMode(WeaponType.Warp);
	}

	public void SetUnarmedMode()
	{
		if (state.IdleMode == false)
			return;

		
		animator.SetInteger("WeaponType", (int)WeaponType.Unarmed);

		if (weaponTable[type] != null)
			weaponTable[type].UnEquip();

		
		ChangeType(WeaponType.Unarmed);
	}

	private void SetMode(WeaponType type)
	{
		if (this.type == type)
		{
			SetUnarmedMode();

			return;
		}
		else if (UnarmedMode == false)
		{
			weaponTable[this.type].UnEquip();
		}

		if (weaponTable[type] == null)
		{
			SetUnarmedMode();

			return;
		}


		animator.SetBool("IsEquipping", true);
		animator.SetInteger("WeaponType", (int)type);

		weaponTable[type].Equip();


		ChangeType(type);
	}

	private void ChangeType(WeaponType type)
	{
		if (this.type == type)
			return;


		WeaponType prevType = this.type;
		this.type = type;

		OnWeaponTyeChanged?.Invoke(prevType, type);
	}

	public void Begin_Equip()
	{
		weaponTable[type].Begin_Equip();
	}

	public void End_Equip()
    {
		animator.SetBool("IsEquipping", false);

		weaponTable[type].End_Equip();
		OnEndEquip?.Invoke();
	}

	public void DoAction()
    {
		if (weaponTable[type] == null)
			return;

		if (weaponTable[type].CanDoAction() == false)
			return;

		animator.SetBool("IsAction", true);
		weaponTable[type].DoAction();
	}

	public void Begin_DoAction()
    {
		weaponTable[type].Begin_DoAction();
	}

	public void End_DoAction()
	{
		//finishType
		//0 - Animation, 1 - Damage

		animator.SetBool("IsAction", false);

		weaponTable[type].End_DoAction();
		OnEndDoAction?.Invoke();
	}

	private void Begin_Combo()
    {
		Melee melee = weaponTable[type] as Melee;

		melee?.Begin_Combo();
    }

	private void End_Combo()
	{
		Melee melee = weaponTable[type] as Melee;

		melee?.Begin_Combo();
	}

	private void Begin_Collision(AnimationEvent e)
	{
		Melee melee = weaponTable[type] as Melee;

		melee?.Begin_Collision(e);
	}

	private void End_Collision()
	{
		Melee melee = weaponTable[type] as Melee;

		melee?.End_Collision();
	}

	private void Play_DoAction_Particle()
    {
		weaponTable[type].Play_Particle();
    }

	private void Play_Impulse()
    {
		Melee melee = weaponTable[type] as Melee;

		melee?.Play_Impulse();
	}

	public void SetWarpPosition(Vector3 position)
    {
		Warp warp = weaponTable[type] as Warp;

		if (warp == null)
			return;

		warp.MoveToPosition = position;
    }
}
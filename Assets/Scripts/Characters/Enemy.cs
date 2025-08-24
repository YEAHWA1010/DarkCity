using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Enemy 
    : Character
    , IDamagable
{
    [SerializeField]
    private Color damageColor = Color.red;

    [SerializeField]
    private float changeColorTime = 0.15f;

	private Color originColor;
    private Material skinMaterial;

    private AIController aiController;

	[SerializeField] string[] targetRendererNames = { "Skeleton", "Cylinders" };
	

	protected override void Awake()
    {
        base.Awake();

        aiController = GetComponent<AIController>();

		//Transform surface = transform.FindChildByName("Skeleton");
		//      skinMaterial = surface.GetComponent<SkinnedMeshRenderer>().material;
		//      originColor = skinMaterial.color;

		//skinMaterial.color = damageColor;

		// �� ���� ������ ������
		Color rand = UnityEngine.Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);

		string[] names = { "Skeleton", "Cylinders" };
		foreach (var name in names)
		{
			var tf = transform.Find(name);
			if (!tf) continue;

			var smr = tf.GetComponent<SkinnedMeshRenderer>();
			if (!smr) continue;

			var mats = smr.materials;          // �ν��Ͻ�ȭ�� �迭
			for (int i = 0; i < mats.Length; i++)
				mats[i].color = rand;          // �׳� color�� �ٲ�(���� �ܼ�)
			smr.materials = mats;              // ��� ���Ҵ�
		}
	}

	protected override void Start()
    {
        base.Start();

		//Color rand = UnityEngine.Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);

		//foreach (var name in targetRendererNames)
		//{
		//	if (string.IsNullOrEmpty(name)) continue;

		//	var tf = transform.Find(name);                       // �ڽ� ã��
		//	if (!tf) continue;

		//	var smr = tf.GetComponent<SkinnedMeshRenderer>();    // ��Ű�� ������
		//	if (!smr) continue;

		//	// �� �������� ���� ��� ��Ƽ���� �� ����
		//	var mats = smr.materials;                            // �ν��Ͻ� �迭
		//	for (int i = 0; i < mats.Length; i++)
		//		mats[i].color = rand;
		//	smr.materials = mats;                                // (��� ���Ҵ�)
		//}
		//RuntimeAnimatorController controller = animator.runtimeAnimatorController;
		//AnimationClip[] clips = controller.animationClips;
		//foreach(AnimationClip clip in clips)
		//{
		//    if (clip.name.Equals("Sword_Impact") == false)
		//        continue;

		//    print(clip.name);
		//    print(clip.frameRate);
		//    print(clip.length);
		//    print(clip.apparentSpeed);
		//    print(clip.averageSpeed);
		//}
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
            aiController?.SetDamageMode();
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

    private IEnumerator Change_Color(float time)
    {
        skinMaterial.color = damageColor;

        yield return new WaitForSeconds(time);

        skinMaterial.color = originColor;
    }

    private IEnumerator Change_IsKinemetics(int frame)
    {
        for (int i = 0; i < frame; i++)
            yield return new WaitForFixedUpdate();

        rigidbody.isKinematic = true;
    }

    protected override void End_Damaged()
    {
        base.End_Damaged();

        animator.SetInteger("ImpactIndex", 0);
        state.SetIdleMode();

        aiController?.End_Damage();
    }
}
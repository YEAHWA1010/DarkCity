using UnityEngine;

public class Test_Curve : MonoBehaviour
{
	[SerializeField]
	private AnimationCurve movingCurve;

    [SerializeField]
    private AnimationCurve movingCurveY;

    private float curveValue;

    private float originY = 0.0f;

    private void Start()
    {
        print(movingCurve[movingCurve.length - 1].time);

        print(movingCurveY[0].time);
        print(movingCurveY[movingCurveY.length - 1].time);

        originY = transform.position.y;
    }

    private float time = 0.0f;
    private float timeY = 0.0f;

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= movingCurve[movingCurve.length - 1].time)
            time -= time;

        timeY += Time.deltaTime;
        if (timeY >= movingCurveY[movingCurveY.length - 1].time)
            timeY -= timeY;

        curveValue = movingCurve.Evaluate(time);

        Vector3 position = transform.position;
        position.x = curveValue;
        position.y = originY + movingCurveY.Evaluate(timeY);

        transform.position = position;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 200, 200, 20), curveValue.ToString());
    }
}
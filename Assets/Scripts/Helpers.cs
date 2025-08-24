using UnityEngine;

public static class Extend_TransformHelpers
{
    public static Transform FindChildByName(this Transform transform, string name)
    {
        Transform[] transforms = transform.GetComponentsInChildren<Transform>();

        foreach(Transform t in transforms)
        {
            if (t.gameObject.name.Equals(name))
                return t;
        }

        return null;
    }
}

public static class UIHelpers
{
    public static Canvas CreateBillboardCanvas(string resourceName, Transform transform, Camera camera)
    {
        GameObject prefab = Resources.Load<GameObject>(resourceName);
        GameObject obj = GameObject.Instantiate<GameObject>(prefab, transform);

        Canvas canvas = obj.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;

        return canvas;
    }
}

public static class CameraHelpers
{
    public static bool GetCusorLocation(float distance, LayerMask mask)
    {
        Vector3 position;
        Vector3 normal;

        return GetCusorLocation(out position, out normal, distance, mask);
    }

    public static bool GetCusorLocation(out Vector3 position, float distance, LayerMask mask)
    {
        Vector3 normal;

        return GetCusorLocation(out position, out normal, distance, mask);
    }

    public static bool GetCusorLocation(out Vector3 position, out Vector3 normal, float distance, LayerMask mask)
    {
        position = Vector3.zero;
        normal = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, distance, mask))
        {
            position = hit.point;
            normal = hit.normal;

            return true;
        }

        return false;
    }
}
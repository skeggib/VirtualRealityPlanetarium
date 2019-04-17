using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardZooming : MonoBehaviour
{
    public List<GameObject> Planets;
    public Camera Camera;
    public Light Light;

    private float _startRange;

    // Start is called before the first frame update
    void Start()
    {
        _startRange = Light.range / transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject grabbingPlanet = null;
        foreach (var planet in Planets)
        {
            var grabCamera = planet.GetComponent<GrabCamera>();
            if (!(grabCamera is null) && grabCamera.Grabbing)
            {
                grabbingPlanet = planet;
                break;
            }
        }

        var pivot = grabbingPlanet?.transform.position ?? Camera.transform.position;

        var one = new Vector3(1, 1, 1);
        var scale = Time.deltaTime * 3;
        var mult = new Vector3(scale, scale, scale);
        if (Input.GetKey(KeyCode.O))
            ScaleAround(transform, pivot, one + mult);
        else if (Input.GetKey(KeyCode.I) && transform.localScale.x > 0.1)
            ScaleAround(transform, pivot, one - mult);

        if (transform.localScale.x > 5000)
            ScaleAround(transform, pivot, new Vector3(5000 / transform.localScale.x, 5000 / transform.localScale.x, 5000 / transform.localScale.x));
        if (transform.localScale.x < 0.1)
            ScaleAround(transform, pivot, new Vector3(0.1f / transform.localScale.x, 0.1f / transform.localScale.x, 0.1f / transform.localScale.x));
        
        Light.range = _startRange * transform.localScale.x;
    }

    public static void ScaleAround(Transform target, Vector3 pivot, Vector3 scale) {

        var targetParent = target.parent;
        var empty = new GameObject();
        empty.transform.Translate(pivot);
        target.parent = empty.transform;
        var temp = empty.transform.localScale;
        temp.Scale(scale);
        empty.transform.localScale = temp;
        target.parent = targetParent;
        Destroy(empty);
    }
}

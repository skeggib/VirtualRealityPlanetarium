using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardZooming : MonoBehaviour
{
    public GameObject Globals;
    private Globals _globalsScript;

    public float MaxScale = 5000;
    public float MinScale = 0.1f;

    private float _startRange;

    // Start is called before the first frame update
    void Start()
    {
        _globalsScript = Globals.GetComponent<Globals>();
        _startRange = _globalsScript.Sun.GetComponent<Light>().range / transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject grabbingPlanet = _globalsScript.Planets.Find(planet => planet.GetComponent<GrabCamera>()?.IsGrabbing ?? false);

        var pivot = grabbingPlanet?.transform.position ?? transform.position;

        var one = new Vector3(1, 1, 1);
        var scale = Time.deltaTime * 3;
        var mult = new Vector3(scale, scale, scale);
        if (Input.GetKey(KeyCode.O))
            ScaleAround(transform, pivot, one + mult);
        else if (Input.GetKey(KeyCode.I))
            ScaleAround(transform, pivot, one - mult);

        if (transform.localScale.x > MaxScale)
            ScaleAround(transform, pivot, new Vector3(MaxScale / transform.localScale.x, MaxScale / transform.localScale.y, MaxScale / transform.localScale.z));
        if (transform.localScale.x < MinScale)
            ScaleAround(transform, pivot, new Vector3(MinScale / transform.localScale.x, MinScale / transform.localScale.y, MinScale / transform.localScale.z));
        
        _globalsScript.Sun.GetComponent<Light>().range = _startRange * transform.localScale.x;
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

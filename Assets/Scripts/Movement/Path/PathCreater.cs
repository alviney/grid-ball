using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreater : MonoBehaviour
{
    public PathRunner pathRunner;
    public float timeout = 0.1f;
    public float force = 1f;
    public float pointSpeed = 2f;
    public int maxPoints = 3;
    float timer = 0f;
    bool runStarted = false;
    private Vector3 recorder = Vector3.zero;
    public Transform markers;
    public GameObject markerPrefab;
    private Vector3 markerOffset = new Vector3(0,-.35f,0);
    public List<Vector3> points = new List<Vector3>();
    private float outerTimer = 2f;

    public void Create(JoystickEvent jEvent)
    {        
        float mag = jEvent.magnitude;

        if (outerTimer > 1) {
            if (!runStarted && mag > 0.3f) BeginPath();
            else if (runStarted && mag < 0.3f || (runStarted && points.Count > maxPoints)) EndPath();

            if (runStarted)
            {
                timer -= mag * pointSpeed / 100;
                if (timer <= 0)
                {   
                    recorder += jEvent.normalizedDirection * force;
                    RecordPoint();
                    timer = timeout;
                }

                DisplayPoints();
            }
        }

        // CheckForCancel(); 

        outerTimer += Time.deltaTime;
    }

    // private void CheckForCancel() {
    //     if (Mathf.Clamp01(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude) > 0.2f && !runStarted && outerTimer > 1) {
    //         points.Clear();
    //         DisplayPoints();
    //     }
    // }

    private void RecordPoint()
    {
        if (points.Count - 1 < maxPoints) {
            points.Add(recorder + markerOffset);
        }
    }

    private void DisplayPoints()
    {
        foreach (Transform marker in markers) Destroy(marker.gameObject);

        for (int i = 0; i < points.Count - 1; i++)
        {
            GameObject instance = Instantiate(markerPrefab, markers);
            instance.transform.position = this.transform.position + points[i];
        }
    }

    private void BeginPath() {
        points.Clear();
        runStarted = true;
        timer = timeout;
        recorder = Vector3.zero;

        Time.timeScale = 0.5f;
    }
    private void EndPath() {
        DisplayPoints();
        runStarted = false;
        Time.timeScale = 1f;

        List<(Vector3 pos, Transform marker)> pathPoints = new List<(Vector3 pos, Transform marker)>();

        for (int i = 0; i < points.Count; i++)
        {
            points[i] = transform.position + points[i];
            pathPoints.Add((points[i], markers.GetChild(i)));
        }

        outerTimer = 0;

        pathRunner.Run(pathPoints, () => {});
    }
}

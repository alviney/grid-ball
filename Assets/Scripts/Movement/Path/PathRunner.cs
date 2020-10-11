using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRunner : MonoBehaviour
{
    public Translater translater;
    public float runSpeed = 10;
    private List<(Vector3 pos, Transform marker)> points;
    private Action callback;

    private void Update() {
        if (points != null && points.Count > 0) RunToNextPoint();
        else if (callback != null) FinishRun();
    }

    private void RunToNextPoint()
    {
        Vector3 nextPoint = points[0].pos;
        Vector3 dir = (nextPoint - this.transform.position);

        translater.MoveTo(this.transform.position + dir.normalized * runSpeed * Time.deltaTime);

        if (dir.magnitude < 0.5f) {
            points.RemoveAt(0);
            Destroy(points[0].marker);
        }
    }

    private void FinishRun() {
        this.callback();
        this.callback = null;
    }

    public void Run(List<(Vector3 pos, Transform marker)> points, Action callback ) {
        this.points = points;   
        this.callback = callback;
    }
}

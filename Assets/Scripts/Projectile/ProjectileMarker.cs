using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMarker : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform shadow;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, -Vector3.up * hit.distance, Color.yellow);
            
            SetShadowTransforms(hit);
        }
    }

    private void SetShadowTransforms(RaycastHit hit) {
        shadow.position = hit.point;
        float scale = hit.distance * 2;
        shadow.localScale = new Vector3(scale, scale, scale);
    }
}

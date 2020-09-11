using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Transform nextNode;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.1f);

        if (nextNode != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, nextNode.position);
        }
    }
}

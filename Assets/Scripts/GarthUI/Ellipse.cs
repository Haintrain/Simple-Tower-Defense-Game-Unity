using UnityEngine;
 using System.Collections;
 
 [ExecuteInEditMode]
 [RequireComponent (typeof(LineRenderer))]
 public class Ellipse : MonoBehaviour 
 {
     public float xRadius;
     private Vector2 radius;
     public float width = 1f;
     public float rotationAngle = 45;
     public int resolution = 500;
 
     private Vector3[] positions;
     private LineRenderer line;
     
     
     void OnValidate()
     {
         radius.x = xRadius;
         radius.y = (xRadius * 4.98f)/8.64f;
         UpdateEllipse();
     }

     void Start()
     {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
     }
     
     public void UpdateEllipse()
     {
         if ( line == null)
             line = GetComponent<LineRenderer>();
             
         line.positionCount = resolution + 3;
         
         line.startWidth = width;
         line.endWidth = width;
         
         
         AddPointToLineRenderer(0f, 0);
         for (int i = 1; i <= resolution + 1; i++) 
         {
             AddPointToLineRenderer((float)i / (float)(resolution) * 2.0f * Mathf.PI, i);
         }
         AddPointToLineRenderer(0f, resolution + 2);
     }
     
     void AddPointToLineRenderer(float angle, int index)
     {
         Quaternion pointQuaternion = Quaternion.AngleAxis (rotationAngle, Vector3.forward);
         Vector3 pointPosition;
         
         pointPosition = new Vector3(radius.x * Mathf.Cos (angle), radius.y * Mathf.Sin (angle)-0.1f, 0.0f);
         pointPosition = pointQuaternion * pointPosition;
         
         line.SetPosition(index, pointPosition);        
     }
 }
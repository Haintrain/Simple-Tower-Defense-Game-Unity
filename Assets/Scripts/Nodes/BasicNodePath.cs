using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNodePath : MonoBehaviour
{
    public List<Transform> nodePath;

    public GameObject node, objective;

    public Transform startNode;
    public Transform endNode;

    private Transform nextNode;

    public void CreateNewNode()
    {
        if (startNode != null)
        {
            nextNode = Instantiate(node, endNode.position, Quaternion.identity).transform;
            endNode.GetComponent<Node>().nextNode = nextNode.transform;
            endNode = nextNode;            
        }
        else
        {
            nextNode = Instantiate(node, transform.position, Quaternion.identity).transform;
            startNode = endNode = nextNode;
        }
        
        nextNode.SetParent(this.transform);
        nextNode.name = "Node" + nodePath.Count;
        nodePath.Add(nextNode.transform);
    }

    public void CreateObjective()
    {
        if(startNode != null)
        {
            nextNode = Instantiate(objective, endNode.position, Quaternion.identity).transform;
            endNode.GetComponent<Node>().nextNode = nextNode.transform;
            endNode = nextNode;

            nextNode.SetParent(this.transform);
            nextNode.name = "Node" + nodePath.Count;
            nodePath.Add(nextNode.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
  Transform[] childObjects;
  public List<Transform> routeNodes = new List<Transform> { };

  private void Awake()
  {
    FillNodes();
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.green;

    for (int i = 0; i < routeNodes.Count; i++)
    {
      Vector3 currentPos = routeNodes[i].position;

      if (i > 0)
      {
        Vector3 prevPos = routeNodes[i - 1].position;
        Gizmos.DrawLine(prevPos, currentPos);
      }
    }
  }

  void FillNodes()
  {
    routeNodes.Clear();

    childObjects = GetComponentsInChildren<Transform>();

    foreach (Transform child in childObjects)
    {
      if (child != this.transform)
      {
        routeNodes.Add(child);
      }
    }
  }
  public int selectNodeIndex(Transform nodeTransform)
  {
    return routeNodes.IndexOf(nodeTransform);
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodos : MonoBehaviour
{
    [SerializeField] public bool occupied;
    [SerializeField] public int IdNode;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, 0.5f);




    }
}
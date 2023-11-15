using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public struct Aristas
{
    [SerializeField] public int origen;
    [SerializeField] public int destino;
    [SerializeField] public int peso;
}

[System.Serializable]
public struct Nodos
{
    [SerializeField] public bool occupied;
    [SerializeField] public GameObject gameObject;
}

public class GrafoManager : MonoBehaviour
{
    GrafoMA grafo = new GrafoMA();
    [SerializeField] List<Aristas> aristas = new();
    [SerializeField] int cantVertices = 12;
    [SerializeField] Nodos[] nodos;
    AlgDijkstra algDijkstra;
    int i = 5;


    private void Start()
    {
        nodos = new Nodos[cantVertices];
        grafo.InicializarGrafo();
        for (int i = 0; i < cantVertices; i++)
        {
            grafo.AgregarVertice(i);
        }

        foreach (var arista in aristas)
        {
            grafo.AgregarArista(arista.origen, arista.destino, arista.peso);

        }

        for (int i = 0; i < cantVertices; i++)
        {
            nodos[i].gameObject = GameObject.Find("Nodo" + i);
        }

    }

    public int NextPosAvailable(int currentPos)
    {
        int newPos = 0;
        for (int i = 0; i < cantVertices; i++)
        {
            if (grafo.ExisteArista(currentPos, i))
            {
                newPos = i;
                break;
            }
        }

        return newPos;
    }

    public Vector3 GetPosition(Vector3 vector, int nodo)
    {

        vector = nodos[nodo].gameObject.transform.position;

        return vector;
    }

    public Vector3 PathFinding(Vector3 vector, int currentNode)
    {
        AlgDijkstra.Dijkstra(grafo, 0);

        vector = GetPosition(vector, currentNode);
        Debug.Log(AlgDijkstra.nodos[5]);
        return vector;
    }

}

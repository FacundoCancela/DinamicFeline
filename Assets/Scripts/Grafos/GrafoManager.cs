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



public class GrafoManager : MonoBehaviour
{
    GrafoMA grafo = new GrafoMA();
    [SerializeField] List<Aristas> aristas = new();
    [SerializeField] int cantVertices = 12;
    [SerializeField] public nodos[] nodos;
    public nodos[] camino;
    int nextPos = 6;
    AlgDijkstra algDijkstra;


    private void Awake()
    {
        nodos = new nodos[cantVertices];
        grafo.InicializarGrafo();

        for (int i = 0; i < cantVertices; i++)
        {
            nodos[i] = GameObject.Find("Nodo" + i).GetComponent<nodos>();
        }

        for (int i = 0; i < cantVertices; i++)
        {
            grafo.AgregarVertice(nodos[i].IdNode);
        }

        foreach (var arista in aristas)
        {
            grafo.AgregarArista(arista.origen, arista.destino, arista.peso);

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

    public Vector2 GetPosition(Vector2 vector, int nodo)
    {

        vector = nodos[nodo].gameObject.transform.position;

        return vector;
    }

    public nodos[] PathFinding( int currentNode)
    {


        camino = new nodos[cantVertices];
        camino = AlgDijkstra.Dijkstra(grafo, currentNode, nodos, nextPos);;
        
        return camino;
    }

}

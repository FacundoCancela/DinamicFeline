using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPower_Up : MonoBehaviour, Ilistener
{
    [SerializeField] private List<GameObject> powerUps = new();
    [SerializeField] private string onEnemyDeath;
    private int objectiveNode;
    private int killsCounter;
    [SerializeField] private int neededKills = 3;
    [SerializeField] List<nodos> nodos;
    GrafoManager grafo;
    EventsManager eventsManager => EventsManager.Instance;

   

    void Start()
    {
        GameObject gameObject = GameObject.Find("GrafoManager");
        grafo = gameObject.GetComponent<GrafoManager>();
        eventsManager.AddListener(onEnemyDeath, this);

        foreach (var nodo in grafo.nodos)
        {
            nodos.Add(nodo);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }

    public void OnEventDispatch()
    {
        killsCounter++;
        Debug.Log("Kills:" + killsCounter);
        if (killsCounter >= neededKills)
        {
            var i = Random.Range(0, powerUps.Count);
            objectiveNode = Random.Range(0, nodos.Count);
            GameObject powerUp = powerUps[i];
            Vector3 pos = new Vector3();
            pos = grafo.GetPosition(pos, nodos[objectiveNode].IdNode);
            pos += Vector3.up * 10;
            Quaternion rot = transform.rotation;
            Instantiate(powerUp, pos, rot);
            //powerUp.GetComponent<PowerUp>().getObjectivePosition(nodos[objectiveNode].gameObject.transform.position);
            killsCounter = 0;
        }


    }


    public GameObject ReturnNode()
    {
        GameObject gameobject = nodos[objectiveNode].gameObject;

        return gameObject;

    }
}

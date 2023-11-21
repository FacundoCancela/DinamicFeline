using UnityEngine;

public class Puntajes : MonoBehaviour
{
    public string Jugador { get; set; }
    public int Puntaje { get; set; }

    public Puntajes(string jugador, int puntaje)
    {
        this.Jugador = jugador;
        this.Puntaje = puntaje;
    }
}

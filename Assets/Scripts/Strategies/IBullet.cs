using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    int Damage { get;}
    int Speed { get;}
    float LifeTime { get;}
    bool EnemyDamaged { get;}
    bool TravelRight { get;}
}

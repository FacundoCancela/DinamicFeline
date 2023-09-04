using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    float MovementSpeed { get; }

    void Move(Vector2 direction, float minX, float maxX);
}


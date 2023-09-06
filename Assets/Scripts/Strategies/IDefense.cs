using System.Collections;
using UnityEngine;

public interface IDefense
{
    void Block(Transform armTransform, bool facingRight);
    void ReleaseBlock(Transform armTransform, bool facingRight);
    int ReduceDamage();
    bool IsBlocking { get; }
}

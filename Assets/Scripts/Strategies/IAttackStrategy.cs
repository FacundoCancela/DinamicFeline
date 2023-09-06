using System.Collections;
using UnityEngine;

public interface IAttackStrategy
{
    void Attack(Transform armTransform, bool facingRight);
    int GetDamage();
    float GetAttackDuration();
    bool IsAttacking { get; }
}

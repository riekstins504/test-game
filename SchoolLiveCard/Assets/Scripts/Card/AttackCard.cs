using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : MonoBehaviour
{
    public AttackCardSO AttackCardSo;

    public bool DoAttack(IDamagable attackTarget)
    {
        return attackTarget.TakeDamage(AttackCardSo.damage);
    }
}

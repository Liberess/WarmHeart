using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private int originHp = 100;

    [SerializeField] private int currentHp;
    public int CurrentHp
    {
        get => currentHp;

        set
        {
            currentHp = value;

            if (currentHp > originHp)
                currentHp = originHp;
            else if (currentHp <= 0)
                DeathAction();

            Debug.Log("ch");

            ChangedHpValueAction();
        }
    }

    public UnityAction DeathAction;
    public UnityAction ChangedHpValueAction;

    private void Awake()
    {
        currentHp = originHp;
    }

    public virtual void ApplyDamage(DamageMessage dmgMsg)
    {
        currentHp -= dmgMsg.damageAmount;
    }

    public virtual void CureHealthPoint(int cureAmount = 0)
    {
        currentHp += cureAmount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] protected int originHp = 100;

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
                Die();

            ChangedHpValueAction?.Invoke();
        }
    }

    [SerializeField] protected float minTimeBetDamaged = 0.1f;
    private float lastDamagedTime;

    protected bool IsDamageable
    {
        get
        {
            if (Time.time >= lastDamagedTime + minTimeBetDamaged)
                return true;

            return false;
        }
    }

    public bool IsDead { get; protected set; }

    public UnityAction DeathAction;
    public UnityAction ChangedHpValueAction;

    private void OnEnable()
    {
        IsDead = false;
        currentHp = originHp;
    }

    public virtual void ApplyDamage(DamageMessage dmgMsg)
    {
        if (IsDead) return;

        if (!IsDamageable) return;

        lastDamagedTime = Time.time;
        CurrentHp -= dmgMsg.damageAmount;
    }

    public virtual void CureHealthPoint(int cureAmount = 0)
    {
        if (IsDead) return;

        CurrentHp += cureAmount;
    }

    public virtual void Die()
    {
        DeathAction?.Invoke();
        IsDead = true;
    }
}

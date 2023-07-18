using Assets.Script.Entity.Attack;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public List<AbstractAttack> attacks;
    public int currentAttackId = 0;
    public bool AttackListIsEmpty => attacks == null || attacks.Count == 0;
    public AbstractAttack CurrentAttack => (AttackListIsEmpty) ? null : attacks[currentAttackId];

    public void RegisterAttacks(List<AbstractAttack> registeredAttacks)
    {
        if (registeredAttacks == null || registeredAttacks.Count == 0)
        {
            Debug.LogWarning("Registering a null attack list!");
            this.attacks = new List<AbstractAttack>()
            {
                new EmptyAttack(gameObject,null),
            };
        }
        else
        {
            this.attacks = registeredAttacks;
        }
    }

    // Start is called before the first frame update

    public void SwitchAttack(Utilities.Direction direction)
    {
        if (AttackListIsEmpty)
        {
            Debug.LogWarning("You HAVENT registered attacks before using");
            Debug.LogWarning("GameObject: " + gameObject.name);
            return;
        }
        if (direction == Utilities.Direction.Forward)
        {
            currentAttackId++;
            if (currentAttackId >= attacks.Count)
            {
                currentAttackId = 0;
            }
        }
        else
        {
            currentAttackId--;
            if (currentAttackId < 0)
            {
                currentAttackId = attacks.Count - 1;
            }
        }
    }

    public void DoAttack()
    {
        if (AttackListIsEmpty)
        {
            Debug.LogWarning("You HAVENT registered attacks before using");
            Debug.LogWarning("GameObject: " + gameObject.name);
            return;
        }
        CurrentAttack.DoAttack();
    }
}
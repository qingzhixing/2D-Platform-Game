using Assets.Script.Entity.Attack;
using UnityEngine;

public class SickleAttack : AbstractAttack
{
    public SickleAttack(GameObject source, GameObject bindAttackObject) : base(source, bindAttackObject, 1f, "Sickle Attack")
    {
    }

    public override void AttackContent()
    {
        AudioController.PlaySickleThrow();
        GameObject sickle = Object.Instantiate(bindAttackObject, source.transform.position, source.transform.rotation);
        SickleController controller = sickle.GetComponent<SickleController>();
        controller.direction = source.GetComponent<EntityController>().facingDirection;
    }
}
using Assets.Script.Entity.Attack;
using UnityEngine;

public class BombAttack : AbstractAttack
{
    public BombAttack(GameObject source, GameObject bindAttackObject) : base(source, bindAttackObject, 1f, "Bomb Attack")
    {
    }

    public override void AttackContent()
    {
        GameObject bomb = Object.Instantiate(bindAttackObject, Vector3.up * 2 + source.transform.position, source.transform.rotation);
        BombController controller = bomb.GetComponent<BombController>();
        controller.initSpeed = new Vector2(5, 3);
    }
}
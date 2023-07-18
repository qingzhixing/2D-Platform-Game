﻿using UnityEngine;

namespace Assets.Script.Entity.Attack
{
    internal class EmptyAttack : AbstractAttack
    {
        public EmptyAttack(GameObject source, GameObject bindAttackObject) : base(source, bindAttackObject, "Empty Attack")
        {
        }

        public override void AttackContent()
        {
            Debug.Log("Empty Attack Called");
        }
    }
}
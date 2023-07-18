using System.Collections;
using UnityEngine;

namespace Assets.Script.Entity.Attack
{
    internal class PlayerNormalAttack : AbstractAttack
    {
        private Animator sourceAnimator;
        private PolygonCollider2D bindObjectPolygonCollider2D;

        public PlayerNormalAttack(GameObject source, GameObject bindAttackObject) : base(source, bindAttackObject, "Player Normal Attack")
        {
            sourceAnimator = source.GetComponent<Animator>();
            bindObjectPolygonCollider2D = bindAttackObject.GetComponent<PolygonCollider2D>();
        }

        public override void AttackContent()
        {
            enableAttack = false;
            sourceAnimator.SetTrigger("Attack");
            Utilities.StartCoroutine(StartAttack(), 0.5f);

            IEnumerator StartAttack()
            {
                yield return new WaitForSeconds(0.33f);
                bindObjectPolygonCollider2D.enabled = true;
                AudioController.PlayPlayerAttack();
                Utilities.StartCoroutine(DisableHitBox(), 0.1f);
            }
            IEnumerator DisableHitBox()
            {
                yield return new WaitForSeconds(0.03f);
                bindObjectPolygonCollider2D.enabled = false;
                Utilities.StartCoroutine(EnableAttack(), 0.1f);
            }

            IEnumerator EnableAttack()
            {
                yield return new WaitForSeconds(interval);
                enableAttack = true;
            }
        }
    }
}
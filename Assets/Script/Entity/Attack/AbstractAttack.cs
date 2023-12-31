﻿using UnityEngine;

namespace Assets.Script.Entity.Attack
{
    public abstract class AbstractAttack
    {
        public string attackName;

        public float damage = 0;

        public float interval = 0;

        public GameObject bindAttackObject;

        //攻击结束前不能进行攻击,对最短攻击时间有要求的攻击使用该项,否则不动
        protected bool enableAttack = true;

        protected GameObject source;

        private float lastAttackTime = -1e9f;

        public AbstractAttack(GameObject source, GameObject bindAttackObject, float interval, string attackName)
        {
            this.source = source;
            this.attackName = attackName;
            this.bindAttackObject = bindAttackObject;
            this.interval = interval;
        }

        public AbstractAttack(GameObject source, GameObject bindAttackObject, float interval) : this(source, bindAttackObject, interval, "No Name Attack")
        {
        }

        public abstract void AttackContent();

        public void DoAttack()
        {
            /*Debug.Log("enbaleAttack: " + enableAttack);*/
            if (!enableAttack) return;
            if (Time.time - lastAttackTime < interval || !enableAttack) return;
            lastAttackTime = Time.time;
            AttackContent();
        }
    }
}
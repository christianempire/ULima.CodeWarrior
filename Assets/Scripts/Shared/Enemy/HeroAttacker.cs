﻿using Assets.Scripts.Constants;
using Assets.Scripts.Shared.Hero;
using Asyncoroutine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableEnemy))]
    public class HeroAttacker : MonoBehaviour
    {
        public int Damage = 150;
        public GameObject Hero;

        #region Properties
        private const float TIME_BEFORE_ATTACKING = 0.16f;
        private const float TIME_AFTER_ATTACKING = 0.57f;

        private Animator animator;
        private KillableEnemy killableEnemyActor;
        private KillableHero killableHeroActor;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        public async Task AttackHeroAsync()
        {
            if (killableEnemyActor.IsDead() || killableHeroActor.IsDead())
                return;

            animator.SetTrigger(EnemyAnimatorConstants.AttackParameter);

            await new WaitForSeconds(TIME_BEFORE_ATTACKING);

            if (!killableEnemyActor.IsDead() || killableHeroActor.IsDead())
                Hero.GetComponent<KillableHero>().TakeDamage(Damage);

            await new WaitForSeconds(TIME_AFTER_ATTACKING);
        }

        #region Helpers
        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            killableEnemyActor = GetComponent<KillableEnemy>();
            killableHeroActor = Hero.GetComponent<KillableHero>();
        }
        #endregion
    }
}
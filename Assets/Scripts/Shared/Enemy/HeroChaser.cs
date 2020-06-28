﻿using Assets.Scripts.Constants;
using Assets.Scripts.Constants.Enemy;
using Asyncoroutine;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableEntity))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class HeroChaser : MonoBehaviour
    {
        #region Properties
        private Animator animator;
        private KillableEntity killableEntity;
        private bool mustChaseHero;
        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        void Update()
        {
            if (mustChaseHero)
                ChaseHero();
        }

        public async Task ChaseHeroAsync()
        {
            const float ChasedDistance = 1.0f;

            if (killableEntity.IsDead())
                return;

            mustChaseHero = true;

            await new WaitUntil(() => HeroIsChased() || killableEntity.IsDead());

            mustChaseHero = false;

            StopChasing();

            bool HeroIsChased() => Vector2.Distance(GetClosestHeroObjectPosition(), transform.position) <= ChasedDistance;
        }

        #region Helpers
        private void ChaseHero()
        {
            const float Speed = 5.0f;

            var velocity = (GetClosestHeroObjectPosition() - transform.position).normalized * Speed;

            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x < -0.1f;
            animator.SetBool(EnemyAnimatorConstants.IsRunningParameter, true);
        }

        private Vector3 GetClosestHeroObjectPosition()
        {
            var closestHeroObject = GameObject.FindGameObjectsWithTag(TagConstants.HeroTag)
            .Select(heroObject => new
            {
                HeroObject = heroObject,
                Distance = Vector2.Distance(heroObject.transform.position, transform.position)
            })
            .Where(heroObjectData => !heroObjectData.HeroObject.GetComponent<KillableEntity>().IsDead())
            .OrderBy(heroObjectData => heroObjectData.Distance)
            .First()
            .HeroObject;

            return closestHeroObject.transform.position;
        }

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            killableEntity = GetComponent<KillableEntity>();
            mustChaseHero = false;
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void StopChasing()
        {
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool(EnemyAnimatorConstants.IsRunningParameter, false);
        }
        #endregion
    }
}

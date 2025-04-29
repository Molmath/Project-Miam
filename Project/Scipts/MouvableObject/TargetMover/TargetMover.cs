using Com.MathysMoles.Extension;
using Godot;
using System;
using System.Collections.Generic;

// Author : Mathys Moles

namespace Com.IsartDigital.Miam.Movable
{

    public partial class TargetMover : Movable
    {
        [Export] private float stopMarge = 10f;
        [Export] private float speedDown = 500f;
        [Export] private float speedUpDuration = 1f;

        [Export] private float distanceBounce = 15f;
        [Export] private float enterBounceDuration = 0.1f;
        [Export] private float exitBounceDuration = 0.1f;
        private Vector2 _target;
        public Vector2 Target
        {
            get { return _target; }
            set
            {
                _target = value;
                Velocity = (value - GlobalPosition).Normalized();
                SetTarget();
            }
        }
        private bool OnTarget()
        {
            return Target.DistanceTo(GlobalPosition) <= stopMarge;
        }
        Tween bounceTween;
        private void GoToTarget(float pDelta)
        {
            if (!OnTarget()) return;
            processAction -= GoToTarget;

            Vector2 lDir = Velocity;
            Velocity = Vector2.Zero;
           // speed = 0;
            bounceTween = CreateTween();
            bounceTween.TweenProperty(this, Prop.GLOBAL_POSITION, GlobalPosition + lDir * distanceBounce, enterBounceDuration);//.SetTrans(Tween.TransitionType.Back);
            bounceTween.TweenProperty(this, Prop.GLOBAL_POSITION, Target, exitBounceDuration);//.SetTrans(Tween.TransitionType.Elastic).SetEase(Tween.EaseType.Out);
            bounceTween.TweenCallback(Callable.From(StopBounce));
            //processAction += DownBounce;
        }


        private void StopBounce()
        {
            //if (processAction.Contain(out List<Action<float>> lSpeedToZero, DownBounce, ReturnBounce)) foreach (Action<float> lAction in lSpeedToZero) processAction -= lAction;
            //speed = initSpeed;
            bounceTween?.Kill();
        }


        private void DownBounce(float pDelta)
        {
            if (speed - speedDown * pDelta <= 0)
            {
                processAction -= DownBounce;
                processAction += ReturnBounce;
                speed = 0;

                return;
            }
            speed -= speedDown * pDelta;
        }

        private void ReturnBounce(float pDelta)
        {
            if (OnTarget())
            {
                processAction -= ReturnBounce;
                   Velocity = Vector2.Zero;
                   speed = initSpeed;
            }
            speed -= speedDown * pDelta;
        }

        protected virtual void SetTarget()
        {
            if (!processAction.Contain(nameof(GoToTarget), out Action<float> pOnTarget)) processAction += GoToTarget;
            StopBounce();
        }

    }
}

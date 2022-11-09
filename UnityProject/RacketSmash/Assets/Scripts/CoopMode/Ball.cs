using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coop
{
    public class Ball : BaseSquash.BallBase
    {
        [SerializeField] LevelManager levelManager;
        protected override void OnGroundHit()
        {
            base.OnGroundHit();
            if (BounceCount == 2)
            {
                levelManager.OnBallBounceTwice();
            }
        }
    }
}

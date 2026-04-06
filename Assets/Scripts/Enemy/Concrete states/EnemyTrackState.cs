    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.AI;

    public class EnemyTrackState : BaseState
    {
        EnemyController _enemy;
        GoapAgent _goap;

        public EnemyTrackState(EnemyController enemy, GoapAgent goap) : base(enemy)
        {
            _enemy = enemy;
            _goap = goap;
        }

        public override void OnEnter()
        {
            // GOAP will plan KillPlayer → needs PlayerInAttackRange → runs ChasePlayer
            _goap.CurrentGoal = null;
            _goap.CurrentAction = null;
        }

        public override void Update()
        {
            // GoapAgent.Update() already runs in EnemyController — nothing needed here
        }

        public override void OnExit()
        {
            _goap.CurrentAction?.Stop();
            _goap.CurrentAction = null;
            _goap.CurrentGoal = null;
        }


    }

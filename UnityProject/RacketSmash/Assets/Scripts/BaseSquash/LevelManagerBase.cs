using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseSquash
{

    public class LevelManagerBase : MonoBehaviour
    {
        [Header("Read Only")]
        [SerializeField] RacketBase currentTurn;
        [Header("References")]
        [SerializeField] RacketBase[] players;
        [SerializeField] FrontWallBase frontWall;

        protected FrontWallBase FrontWall { get { return frontWall; } }
        protected RacketBase [] Players { get { return players; } }

        int currentTurnIndex = 0;
        private void Awake()
        { 
            currentTurn = players[currentTurnIndex];
            currentTurn.IsCurrentTurn = true;
        }
        public virtual void OnBallHitWall()
        {
            ChangeTurn();
        }
        public void ChangeTurn()
        {
            currentTurn.IsCurrentTurn = false;
            currentTurnIndex = (currentTurnIndex+1) % players.Length;
            currentTurn = players[currentTurnIndex];
            currentTurn.IsCurrentTurn = true;
        }

        public int GetCurrentTurn()
        {
            return currentTurnIndex;
        }
    }

}
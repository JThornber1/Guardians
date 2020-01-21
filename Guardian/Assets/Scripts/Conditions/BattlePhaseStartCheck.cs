using JT.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
    [CreateAssetMenu(menuName = "Turns/BattlePhaseStartCheck")]
    public class BattlePhaseStartCheck : Condition
    {
        public override bool IsValid()
        {
            GameManager gm = GameManager.singleton;

            PlayerHolder p = gm.currentPlayer;

            int count = gm.currentPlayer.cardsDown.Count;

            for (int i = 0; i < p.cardsDown.Count; i++)
            {
                if (p.cardsDown[i].isFlatfooted)
                    count--;
            }
            
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

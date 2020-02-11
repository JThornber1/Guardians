using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
    [CreateAssetMenu(menuName = "Actions/Player Actions/Reset Flat Foot")]
    public class ResetFlatFootedCards : PlayerAction
    {
        public override void Execute(PlayerHolder player)
        {
            foreach (CardInstance c in player.cardsDown)
            {
                if (c.isFlatfooted)
                {
					c.SetFlatFooted(false);
                }
            }
        }
    }
}

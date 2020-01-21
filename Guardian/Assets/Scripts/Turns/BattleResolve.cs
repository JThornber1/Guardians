using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	[CreateAssetMenu(menuName = "Turns/BattleResolve")]
	public class BattleResolve : Phase
	{
		public Elements attackElements;
		public Elements defenceElements;

		public override bool IsComplete()
		{
			if (forceExit)
			{
				forceExit = false;
				return true;
			}

			return false;
		}

		public override void OnEndPhase()
		{
			
		}

		public override void OnStartPhase()
		{
			PlayerHolder p = Settings.gameManager.currentPlayer;
			PlayerHolder e = Settings.gameManager.GetEnemyOf(p);

			if (p.attackingCards.Count == 0)
			{
				forceExit = true;
				return;
			}

			for (int i = 0; i < p.attackingCards.Count; i++)
			{
				CardInstance inst = p.attackingCards[i];
				Card c = inst.visual.card;
				CardProperties attack = c.GetProperty(attackElements);

				if (attack == null)
				{
					Debug.LogError("You are attacking with a card that can't attack");
					continue;
				}

				e.DoDamage(attack.intValue);
			}

			forceExit = true;
		}
	}
}

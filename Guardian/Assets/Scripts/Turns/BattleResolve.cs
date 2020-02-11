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
			PlayerHolder p = Settings.gameManager.currentPlayer;
			PlayerHolder e = Settings.gameManager.GetEnemyOf(p);

			if (p.attackingCards.Count == 0)
			{
				return true;
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

				p.DropCard(inst, false);
				p.currentHolder.SetCardDown(inst);

				inst.SetFlatFooted(true);

				e.DoDamage(attack.intValue);
			}

			p.attackingCards.Clear();

			return true;
		}

		public override void OnEndPhase()
		{
			
		}

		public override void OnStartPhase()
		{ 

		}
	}
}

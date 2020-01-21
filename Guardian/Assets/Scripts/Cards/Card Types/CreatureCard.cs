using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	[CreateAssetMenu(menuName = "Card Types/Creature")]
	public class CreatureCard : CardType
	{
		public override void OnSetType(CardVisual visual)
		{
			base.OnSetType(visual);

			visual.statsHolder.SetActive(true);
		}
	}
}
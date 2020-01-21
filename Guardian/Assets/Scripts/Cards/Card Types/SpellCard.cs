using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	[CreateAssetMenu(menuName = "Card Types/Spell")]
	public class SpellCard : CardType
	{
		public override void OnSetType(CardVisual visual)
		{
			base.OnSetType(visual);

			visual.statsHolder.SetActive(false);
		}
	}
}
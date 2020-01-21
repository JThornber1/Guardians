using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	[CreateAssetMenu(menuName = "Card Types/Resource")]
	public class ResourceCard : CardType
	{
		public override void OnSetType(CardVisual visual)
		{
			base.OnSetType(visual);

			visual.statsHolder.SetActive(false);
			visual.resourceHolder.SetActive(false);
		}
	}
}
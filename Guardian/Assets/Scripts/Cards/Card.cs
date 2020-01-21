using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	[CreateAssetMenu(menuName = "Cards")]

	public class Card : ScriptableObject
	{
		public CardType cardType;

		public int cost;

		public CardProperties[] properties;

		public CardProperties GetProperty(Elements e)
		{
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].elements == e)
				{
					return properties[i];
				}
			}

			return null;
		}
	}
}

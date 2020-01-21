using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JT
{
	public class CardVisual2 : MonoBehaviour
	{
		public Card card;

		public CardVisualProperties[] properties;

		public GameObject statsHolder;

		public void LoadCard(Card c)
		{
			if (c == null)
				return;

			card = c;

			for (int i = 0; i < c.properties.Length; i++)
			{
				CardProperties cp = c.properties[i];

				CardVisualProperties p = GetProperty(cp.elements);

				if (p == null)
				continue;
				
				if (cp.elements is ElementsInt)
				{
					p.text.text = cp.intValue.ToString();
					Debug.Log("Reached Int");
				}
				else
				if (cp.elements is ElementsText)
				{
					p.text.text = cp.stringValue;
					Debug.Log("Reached Text");
				}
				else
				if (cp.elements is ElementsImage)
				{
					p.image.sprite = cp.sprite;
					Debug.Log("Reached Image");
				}
			}

		}

		public CardVisualProperties GetProperty(Elements e)
		{
			CardVisualProperties result = null;
			
			for (int i = 0; i < properties.Length; i++)
			{
				result = properties[i];
				break;
			}

			return result;
		}
	}
}
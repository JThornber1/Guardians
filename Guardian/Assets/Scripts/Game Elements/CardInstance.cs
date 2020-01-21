using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	public class CardInstance : MonoBehaviour, IClicker
	{
		public CardVisual visual;
		public JT.GameElements.GameElementLogic currentLogic;
        public bool isFlatfooted;

		void Start()
		{
			visual = GetComponent<CardVisual>();
		}

		public bool CanAttack()
		{
			bool result = true;

			if (isFlatfooted)
				result = false;

			if (visual.card.cardType.TypeAllowsForAttack(this))
			{
				result = true;
			}

			return result;
		}

		public void OnClick()
		{
			if (currentLogic == null)
			return;

			currentLogic.OnClick(this);
		}

		public void OnHighlight()
		{
			if (currentLogic == null)
			return;
			
			Debug.Log("Highlights");
			currentLogic.OnHighlight(this);
			
			/*Vector3 s = Vector3.one * 2;
			this.transform.localScale = s;

			Debug.Log(this.gameObject.name);*/
		}
	}
}
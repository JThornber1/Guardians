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
		private bool isSelected = false;

		public void SetFlatFooted(bool isFlat)
		{
			isFlatfooted = isFlat;

			if (isFlatfooted)
				transform.localEulerAngles = new Vector3(0, 0, 90);
			else
				transform.localEulerAngles = Vector3.zero;
		}

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

			//isSelected = !isSelected;

			currentLogic.OnClick(this);
		}

		public void OnHighlight()
		{
			if (currentLogic == null)
			return;
			
			//Debug.Log("Highlights");
			currentLogic.OnHighlight(this);
			
			/*Vector3 s = Vector3.one * 2;
			this.transform.localScale = s;

			Debug.Log(this.gameObject.name);*/
		}
	}
}
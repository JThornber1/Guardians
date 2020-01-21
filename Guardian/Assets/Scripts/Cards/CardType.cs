using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	public abstract class CardType : ScriptableObject
	{
		public string typeName;
		public bool canAttack;

		//public typelogic logic

		public virtual void OnSetType(CardVisual visual)
		{
			Elements t = Settings.GetResourcesManager().typeElement;
			CardVisualProperties type = visual.GetProperty(t);
			type.text.text = typeName;
		}

		public bool TypeAllowsForAttack(CardInstance inst)
		{

			///e.g Flying type can attack even if flatfooted
			///bool r = logic.Execute(inst) =-> if(inst.isFlatfooted)/inst.isFlatfooted = false return true

			if (canAttack)
			{
				return true;
			}
			else
				return false;
		}
	}
}
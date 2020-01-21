using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
	[CreateAssetMenu(menuName = "Turns/Control Phase Player")]
	public class ControlPhasePlayer : Phase
	{
		public GameStates.State playerControlState;
		
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
			if (isInit)
			{
				Settings.gameManager.SetState(null);

				isInit = false;
			}
		}

		public override void OnStartPhase()
		{
			if (!isInit)
			{
				Debug.Log(this.name + "starts");

				Settings.gameManager.SetState(playerControlState);
				Settings.gameManager.onPhaseChanged.Raise();

				isInit = true;
			}
		}
	}
}
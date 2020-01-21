using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JT
{
	public class PlayerStatsUI : MonoBehaviour
	{
		public PlayerHolder player;
		public Image playerPotrait;
		public Text health;
		public Text username;

		public void UpdateAll()
		{
			UpdateUsername();
			UpdateHealth();
		}

		public void UpdateUsername()
		{
			username.text = player.username;
			playerPotrait.sprite = player.potrait;
		}

		public void UpdateHealth()
		{
			health.text = player.health.ToString();
		}
	}
}
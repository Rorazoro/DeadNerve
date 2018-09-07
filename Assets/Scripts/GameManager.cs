﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	private static Dictionary<string, Player>  players = new Dictionary<string, Player>();

	public void RegisterPlayer (string netID, Player player) {
		string playerID = "Player " + netID;
		players.Add(playerID, player);
		player.transform.name = playerID;
	}
}
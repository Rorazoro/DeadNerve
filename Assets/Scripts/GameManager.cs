using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public HostSettings hostSettings;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
		else {
			Debug.Log("More than one GameManager instance!");
		}
	}

	#region Player Register
		private static Dictionary<string, Player>  players = new Dictionary<string, Player>();

		public static void RegisterPlayer (string netID, Player player) {
			string playerID = "Player " + netID;
			players.Add(playerID, player);
			player.transform.name = playerID;
		}

		public static void UnRegisterPlayer (string playerID) {
			players.Remove(playerID);
		}

		public static Player GetPlayer (string playerID) {
			return players[playerID];
		}
	#endregion
}
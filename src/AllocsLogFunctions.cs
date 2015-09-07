using AllocsFixes.PersistentData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AllocsFixes
{
	public class AllocsLogFunctions
	{
		public static void RequestToSpawnPlayer (ClientInfo _cInfo, int _chunkViewDim, PlayerProfile _playerProfile)
		{
			try {
				Log.Out ("Player connected" +
					", entityid=" + _cInfo.entityId +
					", name=" + _cInfo.playerName +
					", steamid=" + _cInfo.playerId +
					", ip=" + _cInfo.ip
				);

				PersistentContainer.Instance.Players [_cInfo.playerId, true].SetOnline (_cInfo);
				PersistentData.PersistentContainer.Instance.Save ();
			} catch (Exception e) {
				Log.Out ("Error in AllocsLogFunctions.RequestToSpawnPlayer: " + e);
			}
		}

		public static void PlayerDisconnected (ClientInfo _cInfo, bool _bShutdown)
		{
			try {
				Player p = PersistentContainer.Instance.Players [_cInfo.playerId, true];
				if (p != null) {
					p.SetOffline ();
				} else {
					Log.Out ("Disconnected player not found in client list...");
				}
				PersistentData.PersistentContainer.Instance.Save ();
			} catch (Exception e) {
				Log.Out ("Error in AllocsLogFunctions.PlayerDisconnected: " + e);
			}
		}
	}
}

using AllocsFixes.PersistentData;
using System;
using System.Collections.Generic;

namespace AllocsFixes
{
	public class PlayerDataStuff
	{

		public static void GM_SavePlayerData (ClientInfo _cInfo, PlayerDataFile _playerDataFile)
		{
			try {
				PersistentContainer.Instance.Players[_cInfo.playerId, true].Update (_playerDataFile);
			} catch (Exception e) {
				Log.Out ("Error in GM_SavePlayerData: " + e);
			}
		}


	}
}

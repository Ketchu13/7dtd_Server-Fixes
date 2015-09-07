using System;

namespace AllocsFixes
{
	public class API : ModApiAbstract {

		public override void GameAwake () {
			StateManager.Awake ();
		}

		public override void GameShutdown () {
			StateManager.Shutdown ();
		}
		
		public override void SavePlayerData (ClientInfo _cInfo, PlayerDataFile _playerDataFile) {
			PlayerDataStuff.GM_SavePlayerData (_cInfo, _playerDataFile);
		}

		public override void PlayerLogin (ClientInfo _cInfo, string _compatibilityVersion) {
		}
		
		public override void PlayerSpawning (ClientInfo _cInfo, int _chunkViewDim, PlayerProfile _playerProfile) {
			AllocsLogFunctions.RequestToSpawnPlayer (_cInfo, _chunkViewDim, _playerProfile);
		}
		
		public override void PlayerDisconnected (ClientInfo _cInfo, bool _bShutdown) {
			AllocsLogFunctions.PlayerDisconnected (_cInfo, _bShutdown);
		}

		public override bool ChatMessage (ClientInfo _cInfo, string _message, string _playerName) {
			return ChatHookExample.Hook (_cInfo, _message, _playerName);
		}
	}
}


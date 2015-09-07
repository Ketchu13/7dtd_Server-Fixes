using System;
using System.IO;
namespace AllocsFixes
{
	public class ChatHookExample {
		private const string BBFILTER = "[ffffffff][/url][/b][/i][/u][/s][/sub][/sup][ff]";
		//private const string ANSWER = "     [ff0000]I[-] [ff7f00]W[-][ffff00]A[-][80ff00]S[-] [00ffff]H[-][0080ff]E[-][0000ff]R[-][8b00ff]E[-]";
		public static bool executeIt(string [] cmd, ClientInfo _cInfo, string _message, string _playerName)
		{
			foreach (string s in cmd) {
				if (_message.ToLower ().Contains(s) == true) {
					if (_cInfo != null) {
						Log.Out ("TOPARSE {0}#{1}#{2}", _cInfo.playerId, _message, _cInfo.playerName);
						return false;
						//_cInfo.SendPackage (new NetPackageGameMessage (ANSWER, ""));
					} else {
						//Log.Out ("Error TOPARSE {0}#", _message);
					}

				}
			}
			return true;
		}
		public static bool Hook (ClientInfo _cInfo, string _message, string _playerName) {
			if (!string.IsNullOrEmpty (_message)) {
				if (_message.EndsWith (BBFILTER + BBFILTER)) {
					_message = _message.Remove (_message.Length - 2 * BBFILTER.Length);
				}
				string[] cmd1 = null;
				try
				{
					if (File.Exists("E:\\cmd.txt") == true) {
						cmd1 = File.ReadAllLines("E:\\cmd.txt");
					}
				}catch( Exception e) {
					Log.Out ("Error in hook chat read file " + e.Message);
				}
				string[] cmd2 = {
					"/heure",
					"/time",
					"/teamspeak",
					"/ts",
					"/now",
					"/location",
					"/admin",
					"/help",
					"/proche",
					"/aide",
					"/pr",
					"/suicide",
					"/site",
					"/random",
					"/saytoteam",
					"/sayt",
					"/join",
					"/leave",
					"/listteams",
					"/respawn",
					"/rpm",
					"/tpfriend",
					"/tptofriend",
					"/mygate1",
					"/mygate2",
					"/lockgate",
					"/unlockgate",
					"/country",
					"/secure",
					"/alarm",
					"/turret",
					"/mymoney",
					"/givemoney",
					"/steal",
					"/loto",
					"/myshop",
					"/show",
					"/shopsystem"
				};//				"/addpoi",
				try
				{
					if (cmd1 == null) {
						return executeIt (cmd2, _cInfo, _message, _playerName);
					}
					else{
						return executeIt (cmd1, _cInfo, _message, _playerName);
					}
				}catch (Exception e) {
					Log.Out ("Error in hook chat " + e.Message);
				}
			}
			return true;
		}
	}
}

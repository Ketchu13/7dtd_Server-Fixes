using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace AllocsFixes.PersistentData
{
	[Serializable]
	public class Player
	{
		private readonly string steamId;
		private int entityId;
		private string name;
		private string ip;
		private long totalPlayTime;
		[OptionalField]
		private DateTime
			lastOnline;
		private Inventory inventory;
		[OptionalField]
		private int
			lastPositionX, lastPositionY, lastPositionZ;
		[OptionalField]
		private uint experience;
		[NonSerialized]
		private ClientInfo
			clientInfo;

		public string SteamID {
			get { return steamId; }
		}

		public int EntityID {
			get { return entityId; }
		}

		public string Name {
			get { return name == null ? string.Empty : name; }
		}

		public string IP {
			get { return ip == null ? string.Empty : ip; }
		}

		public Inventory Inventory {
			get {
				if (inventory == null)
					inventory = new Inventory ();
				return inventory;
			}
		}

		public bool IsOnline {
			get { return clientInfo != null; }
		}

		public ClientInfo ClientInfo {
			get { return clientInfo; }
		}

		public EntityPlayer Entity {
			get {
				if (IsOnline) {
					return GameManager.Instance.World.Players.dict [clientInfo.entityId];
				} else {
					return null;
				}
			}
		}

		public long TotalPlayTime {
			get {
				if (IsOnline) {
					return totalPlayTime + (long)(Time.timeSinceLevelLoad - Entity.CreationTimeSinceLevelLoad);
				} else {
					return totalPlayTime;
				}
			}
		}

		public DateTime LastOnline {
			get {
				if (IsOnline)
					return DateTime.Now;
				else
					return lastOnline;
			}
		}

		public Vector3i LastPosition {
			get {
				if (IsOnline)
					return new Vector3i (Entity.GetPosition ());
				else
					return new Vector3i (lastPositionX, lastPositionY, lastPositionZ);
			}
		}

		public uint Experience {
			get {
				return experience;
			}
		}

		public float Level {
			get {
				float perc = (float)experience / 600000;
				perc = Mathf.Sqrt (perc);
				return Mathf.Clamp ((perc * 60) + 1, 1, 60);
			}
		}

		public void SetOffline ()
		{
			if (clientInfo != null) {
				Log.Out ("Player set to offline: " + steamId);
				lastOnline = DateTime.Now;
				try {
					Vector3i lastPos = new Vector3i (Entity.GetPosition ());
					lastPositionX = lastPos.x;
					lastPositionY = lastPos.y;
					lastPositionZ = lastPos.z;
					totalPlayTime += (long)(Time.timeSinceLevelLoad - Entity.CreationTimeSinceLevelLoad);
				} catch (NullReferenceException) {
					Log.Out ("Entity not available. Something seems to be wrong here...");
				}
				clientInfo = null;
			}
		}

		public void SetOnline (ClientInfo ci)
		{
			Log.Out ("Player set to online: " + steamId);
			clientInfo = ci;
			entityId = ci.entityId;
			name = ci.playerName;
			ip = ci.ip;
			lastOnline = DateTime.Now;
		}

		public void Update (PlayerDataFile _pdf) {
			experience = _pdf.experience;
			inventory.Update (_pdf);
		}

		public Player (string steamId)
		{
			this.steamId = steamId;
			this.inventory = new Inventory ();
		}


	}
}

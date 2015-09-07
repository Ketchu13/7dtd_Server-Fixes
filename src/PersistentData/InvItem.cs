using System;
using System.Runtime.Serialization;

namespace AllocsFixes.PersistentData
{
	[Serializable]
	public class InvItem
	{
		public string itemName;
		public int count;
		public int quality;
		public InvItem[] parts;

		public InvItem (string itemName, int count, int quality = -1)
		{
			this.itemName = itemName;
			this.count = count;
			this.quality = quality;
		}
	}
}


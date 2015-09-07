using System;
using System.Collections.Generic;

namespace AllocsFixes
{
	public class ItemList
	{
		private static ItemList instance;

		public static ItemList Instance {
			get {
				if (instance == null) {
					instance = new ItemList ();
				}
				return instance;
			}
		}

		private ItemList ()
		{
		}

		private SortedDictionary<string, ItemValue> items = new SortedDictionary<string, ItemValue> ();

		public List<string> ItemNames {
			get { return new List<string> (items.Keys); }
		}

		public ItemValue GetItemValue (string itemName)
		{
			if (items.ContainsKey (itemName)) {
				return items [itemName].Clone ();
			} else {
				itemName = itemName.ToLower ();
				foreach (KeyValuePair<string, ItemValue> kvp in items) {
					if (kvp.Key.ToLower ().Equals (itemName)) {
						return kvp.Value.Clone ();
					}
				}
				return null;
			}
		}

		public void Init ()
		{
			NGuiInvGridCreativeMenu cm = new NGuiInvGridCreativeMenu ();
			foreach (ItemStack invF in cm.GetAllItems()) {
				ItemClass ib = ItemClass.list [invF.itemValue.type];
				string name = ib.GetItemName ();
				if (name != null && name.Length > 0) {
					if (!items.ContainsKey (name)) {
						items.Add (name, invF.itemValue);
					} else {
						//Log.Out ("Item \"" + name + "\" already in list!");
					}
				}
			}
			foreach (ItemStack invF in cm.GetAllBlocks()) {
				ItemClass ib = ItemClass.list [invF.itemValue.type];
				string name = ib.GetItemName ();
				if (name != null && name.Length > 0) {
					if (!items.ContainsKey (name)) {
						items.Add (name, invF.itemValue);
					} else {
						//Log.Out ("Item \"" + name + "\" already in list!");
					}
				}
			}
		}
	}
}


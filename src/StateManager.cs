using System;
using System.Reflection;

namespace AllocsFixes
{
	public class StateManager
	{
		public static void Awake ()
		{
			try {
				ItemList.Instance.Init ();

				PersistentData.PersistentContainer.Load ();
			} catch (Exception e) {
				Log.Out ("Error in StateManager.Awake: " + e);
			}
		}

		public static void Shutdown ()
		{
			try {
				Log.Out ("Server shutting down!");
				PersistentData.PersistentContainer.Instance.Save ();
			} catch (Exception e) {
				Log.Out ("Error in StateManager.Shutdown: " + e);
			}
		}
	}
}


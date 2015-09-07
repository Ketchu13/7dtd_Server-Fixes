using System;

namespace AllocsFixes.JSON
{
	public abstract class JSONNode
	{
		public abstract string ToString(bool prettyPrint = false, int currentLevel = 0);
	}
}


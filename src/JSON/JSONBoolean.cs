using System;

namespace AllocsFixes.JSON
{
	public class JSONBoolean : JSONNode
	{
		private bool value;

		public JSONBoolean (bool value)
		{
			this.value = value;
		}

		public bool GetBool ()
		{
			return value;
		}

		public override string ToString (bool prettyPrint = false, int currentLevel = 0)
		{
			return value.ToString (System.Globalization.CultureInfo.InvariantCulture).ToLower ();
		}

		public static JSONBoolean Parse (string json, ref int offset)
		{
			//Log.Out ("ParseBool enter (" + offset + ")");

			if (json.Substring (offset, 4).Equals ("true")) {
				//Log.Out ("JSON:Parsed Bool: true");
				offset += 4;
				return new JSONBoolean (true);
			} else if (json.Substring (offset, 5).Equals ("false")) {
				//Log.Out ("JSON:Parsed Bool: false");
				offset += 5;
				return new JSONBoolean (false);
			} else {
				throw new MalformedJSONException ("No valid boolean found");
			}
		}

	}
}


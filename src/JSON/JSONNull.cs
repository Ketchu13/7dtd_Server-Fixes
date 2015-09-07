using System;

namespace AllocsFixes.JSON
{
	public class JSONNull : JSONNode
	{
		public JSONNull ()
		{
		}

		public override string ToString (bool prettyPrint = false, int currentLevel = 0)
		{
			return "null";
		}

		public static JSONNull Parse (string json, ref int offset)
		{
			//Log.Out ("ParseNull enter (" + offset + ")");

			if (json.Substring (offset, 4).Equals ("null")) {
				//Log.Out ("JSON:Parsed Null");
				offset += 4;
				return new JSONNull ();
			} else {
				throw new MalformedJSONException ("No valid null value found");
			}
		}

	}
}


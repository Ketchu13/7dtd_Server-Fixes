using System;
using System.Collections.Generic;
using System.Text;

namespace AllocsFixes.JSON
{
	public class JSONArray : JSONNode
	{
		private List<JSONNode> nodes = new List<JSONNode> ();

		public JSONNode this [int index] {
			get { return nodes [index]; }
			set { nodes [index] = value; }
		}

		public int Count {
			get { return nodes.Count; }
		}

		public void Add (JSONNode node)
		{
			nodes.Add (node);
		}

		public override string ToString (bool prettyPrint = false, int currentLevel = 0)
		{
			StringBuilder sb = new StringBuilder ("[");
			if (prettyPrint)
				sb.Append ('\n');
			foreach (JSONNode n in nodes) {
				if (prettyPrint)
					sb.Append (new String ('\t', currentLevel + 1));
				sb.Append (n.ToString (prettyPrint, currentLevel + 1));
				sb.Append (",");
				if (prettyPrint)
					sb.Append ('\n');
			}
			if (sb.Length > 1)
				sb.Remove (sb.Length - (prettyPrint ? 2 : 1), 1);
			if (prettyPrint)
				sb.Append (new String ('\t', currentLevel));
			sb.Append ("]");
			return sb.ToString ();
		}

		public static JSONArray Parse (string json, ref int offset)
		{
			//Log.Out ("ParseArray enter (" + offset + ")");
			JSONArray arr = new JSONArray ();

			bool nextElemAllowed = true;
			offset++;
			while (true) {
				Parser.SkipWhitespace (json, ref offset);

				switch (json [offset]) {
					case ',':
						if (!nextElemAllowed) {
							nextElemAllowed = true;
							offset++;
						} else
							throw new MalformedJSONException ("Could not parse array, found a comma without a value first");
						break;
					case ']':
						offset++;
						//Log.Out ("JSON:Parsed Array: " + arr.ToString ());
						return arr;
					default:
						arr.Add (Parser.ParseInternal (json, ref offset));
						nextElemAllowed = false;
						break;
				}
			}
		}

	}
}


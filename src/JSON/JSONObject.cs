using System;
using System.Collections.Generic;
using System.Text;

namespace AllocsFixes.JSON
{
	public class JSONObject : JSONNode
	{
		private Dictionary<string, JSONNode> nodes = new Dictionary<string, JSONNode> ();

		public JSONNode this [string name] {
			get { return nodes [name]; }
			set { nodes [name] = value; }
		}

		public int Count {
			get { return nodes.Count; }
		}

		public List<string> Keys {
			get { return new List<string> (nodes.Keys); }
		}

		public bool ContainsKey (string name)
		{
			return nodes.ContainsKey (name);
		}

		public void Add (string name, JSONNode node)
		{
			nodes.Add (name, node);
		}

		public override string ToString (bool prettyPrint = false, int currentLevel = 0)
		{
			StringBuilder sb = new StringBuilder ("{");
			if (prettyPrint)
				sb.Append ('\n');
			foreach (KeyValuePair<string, JSONNode> kvp in nodes) {
				if (prettyPrint)
					sb.Append (new String ('\t', currentLevel + 1));
				sb.Append (String.Format ("\"{0}\":", kvp.Key));
				if (prettyPrint)
					sb.Append (" ");
				sb.Append (kvp.Value.ToString (prettyPrint, currentLevel + 1));
				sb.Append (",");
				if (prettyPrint)
					sb.Append ('\n');
			}
			if (sb.Length > 1)
				sb.Remove (sb.Length - (prettyPrint ? 2 : 1), 1);
			if (prettyPrint)
				sb.Append (new String ('\t', currentLevel));
			sb.Append ("}");
			return sb.ToString ();
		}

		public static JSONObject Parse (string json, ref int offset)
		{
			//Log.Out ("ParseObject enter (" + offset + ")");
			JSONObject obj = new JSONObject ();

			bool nextElemAllowed = true;
			offset++;
			while (true) {
				Parser.SkipWhitespace (json, ref offset);
				switch (json [offset]) {
					case '"':
						if (nextElemAllowed) {
							JSONString key = JSONString.Parse (json, ref offset);
							Parser.SkipWhitespace (json, ref offset);
							if (json [offset] != ':') {
								throw new MalformedJSONException ("Could not parse object, missing colon (\":\") after key");
							}
							offset++;
							JSONNode val = Parser.ParseInternal (json, ref offset);
							obj.Add (key.GetString (), val);
							nextElemAllowed = false;
						} else {
							throw new MalformedJSONException ("Could not parse object, found new key without a separating comma");
						}
						break;
					case ',':
						if (!nextElemAllowed) {
							nextElemAllowed = true;
							offset++;
						} else
							throw new MalformedJSONException ("Could not parse object, found a comma without a key/value pair first");
						break;
					case '}':
						offset++;
						//Log.Out ("JSON:Parsed Object: " + obj.ToString ());
						return obj;
				}
			}
		}

	}
}


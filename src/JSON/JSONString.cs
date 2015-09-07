using System;
using System.Text;

namespace AllocsFixes.JSON
{
	public class JSONString : JSONNode
	{
		private string value;

		public JSONString (string value)
		{
			this.value = value;
		}

		public string GetString ()
		{
			return value;
		}

		public override string ToString (bool prettyPrint = false, int currentLevel = 0)
		{
			if (value == null || value.Length == 0) {
				return "\"\"";
			}

			int len = value.Length;

			StringBuilder sb = new StringBuilder (len + 4);
			String t;

			foreach (char c in value) {
				switch (c) {
					case '\\':
					case '"':
					case '/':
						sb.Append ('\\');
						sb.Append (c);
						break;
					case '\b':
						sb.Append ("\\b");
						break;
					case '\t':
						sb.Append ("\\t");
						break;
					case '\n':
						sb.Append ("\\n");
						break;
					case '\f':
						sb.Append ("\\f");
						break;
					case '\r':
						sb.Append ("\\r");
						break;
					default:
						if (c < ' ') {
							t = "000" + String.Format ("X", c);
							sb.Append ("\\u" + t.Substring (t.Length - 4));
						} else {
							sb.Append (c);
						}
						break;
				}
			}

			return string.Format ("\"{0}\"", sb.ToString ());
		}

		public static JSONString Parse (string json, ref int offset)
		{
			//Log.Out ("ParseString enter (" + offset + ")");
			StringBuilder sb = new StringBuilder ();
			offset++;
			while (offset < json.Length) {
				switch (json [offset]) {
					case '\\':
						offset++;
						switch (json [offset]) {
							case '\\':
							case '"':
							case '/':
								sb.Append (json [offset]);
								break;
							case 'b':
								sb.Append ('\b');
								break;
							case 't':
								sb.Append ('\t');
								break;
							case 'n':
								sb.Append ('\n');
								break;
							case 'f':
								sb.Append ('\f');
								break;
							case 'r':
								sb.Append ('\r');
								break;
							default:
								sb.Append (json [offset]);
								break;
						}
						offset++;
						break;
					case '"':
						offset++;
						//Log.Out ("JSON:Parsed String: " + sb.ToString ());
						return new JSONString (sb.ToString ());
					default:
						sb.Append (json [offset]);
						offset++;
						break;
				}
			}
			throw new MalformedJSONException ("End of JSON reached before parsing string finished");
		}

	}
}


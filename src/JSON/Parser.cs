using System;
using System.Text;

namespace AllocsFixes.JSON
{
	public class Parser
	{

		public static JSONNode Parse (string json)
		{
			int offset = 0;
			return ParseInternal (json, ref offset);
		}

		public static JSONNode ParseInternal (string json, ref int offset)
		{
			SkipWhitespace (json, ref offset);
			//Log.Out ("ParseInternal (" + offset + "): Decide on: '" + json [offset] + "'");
			switch (json [offset]) {
				case '[':
					return JSONArray.Parse (json, ref offset);
				case '{':
					return JSONObject.Parse (json, ref offset);
				case '"':
					return JSONString.Parse (json, ref offset);
				case 't':
				case 'f':
					return JSONBoolean.Parse (json, ref offset);
				case 'n':
					return JSONNull.Parse (json, ref offset);
				default:
					return JSONNumber.Parse (json, ref offset);
			}
		}

		public static void SkipWhitespace (string json, ref int offset)
		{
			//Log.Out ("SkipWhitespace (" + offset + "): '" + json [offset] + "'");
			while (offset < json.Length) {
				switch (json [offset]) {
					case ' ':
					case '\t':
					case '\r':
					case '\n':
						offset++;
						break;
					default:
						return;
				}
			}
			throw new MalformedJSONException ("End of JSON reached before parsing finished");
		}


	}
}


using System;
using System.Text;

namespace AllocsFixes.JSON
{
	public class JSONNumber : JSONNode
	{
		private double value;

		public JSONNumber (double value)
		{
			this.value = value;
		}

		public double GetDouble ()
		{
			return value;
		}

		public int GetInt ()
		{
			return (int)Math.Round(value);
		}

		public override string ToString (bool prettyPrint = false, int currentLevel = 0)
		{
			return value.ToString (System.Globalization.CultureInfo.InvariantCulture);
		}

		public static JSONNumber Parse (string json, ref int offset)
		{
			//Log.Out ("ParseNumber enter (" + offset + ")");
			StringBuilder sbNum = new StringBuilder ();
			StringBuilder sbExp = null;
			bool hasDec = false;
			bool hasExp = false;
			while (offset < json.Length) {
				if (json [offset] >= '0' && json [offset] <= '9') {
					if (hasExp)
						sbExp.Append (json [offset]);
					else
						sbNum.Append (json [offset]);
				} else if (json [offset] == '.') {
					if (hasExp) {
						throw new MalformedJSONException ("Decimal separator in exponent");
					} else {
						if (hasDec)
							throw new MalformedJSONException ("Multiple decimal separators in number found");
						else if (sbNum.Length == 0) {
							throw new MalformedJSONException ("No leading digits before decimal separator found");
						} else {
							sbNum.Append ('.');
							hasDec = true;
						}
					}
				} else	if (json [offset] == '-') {
					if (hasExp) {
						if (sbExp.Length > 0)
							throw new MalformedJSONException ("Negative sign in exponent after digits");
						else
							sbExp.Append (json [offset]);
					} else {
						if (sbNum.Length > 0)
							throw new MalformedJSONException ("Negative sign in mantissa after digits");
						else
							sbNum.Append (json [offset]);
					}
				} else if (json [offset] == 'e' || json [offset] == 'E') {
					if (hasExp)
						throw new MalformedJSONException ("Multiple exponential markers in number found");
					else if (sbNum.Length == 0) {
						throw new MalformedJSONException ("No leading digits before exponential marker found");
					} else {
						sbExp = new StringBuilder ();
						hasExp = true;
					}
				} else if (json [offset] == '+') {
					if (hasExp) {
						if (sbExp.Length > 0)
							throw new MalformedJSONException ("Positive sign in exponent after digits");
						else
							sbExp.Append (json [offset]);
					} else {
						throw new MalformedJSONException ("Positive sign in mantissa found");
					}
				} else {
					double number;
					if (!double.TryParse (sbNum.ToString (), out number)) {
						throw new MalformedJSONException ("Mantissa is not a valid decimal (\"" + sbNum.ToString () + "\")");
					}

					if (hasExp) {
						int exp;
						if (!int.TryParse (sbExp.ToString (), out exp)) {
							throw new MalformedJSONException ("Exponent is not a valid integer (\"" + sbExp.ToString () + "\")");
						}

						number = number * Math.Pow (10, exp);
					}

					//Log.Out ("JSON:Parsed Number: " + number.ToString ());
					return new JSONNumber (number);
				}
				offset++;
			}
			throw new MalformedJSONException ("End of JSON reached before parsing number finished");
		}

	}
}


using System;
using System.Runtime.Serialization;

namespace AllocsFixes.JSON
{
	public class MalformedJSONException : ApplicationException
	{
		public MalformedJSONException ()
		{
		}

		public MalformedJSONException (string message) : base(message)
		{
		}

		public MalformedJSONException (string message, System.Exception inner) : base(message, inner)
		{
		}
 
		protected MalformedJSONException (SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}


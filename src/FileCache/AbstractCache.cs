using System;
using System.Collections.Generic;

namespace AllocsFixes.FileCache
{
	public abstract class AbstractCache
	{

		public AbstractCache ()
		{
		}

		public abstract byte[] GetFileContent (string filename);

	}
}


using System;
using System.Collections.Generic;
using System.IO;

namespace AllocsFixes.FileCache
{
	// Not caching at all, simply reading from disk on each request
	public class DirectAccess : AbstractCache
	{

		public DirectAccess ()
		{
		}

		public override byte[] GetFileContent (string filename)
		{
			try {
				if (!File.Exists (filename)) {
					return null;
				}

				return File.ReadAllBytes (filename);
			} catch (Exception e) {
				Log.Out ("Error in DirectAccess.GetFileContent: " + e);
			}
			return null;
		}

	}
}


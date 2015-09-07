using System;
using System.Collections.Generic;
using System.IO;

namespace AllocsFixes.FileCache
{
	// Caching all files, useful for completely static folders only
	public class SimpleCache : AbstractCache
	{

		private Dictionary<string, byte[]> fileCache = new Dictionary<string, byte[]> ();

		public SimpleCache ()
		{
		}

		public override byte[] GetFileContent (string filename)
		{
			try {
				lock (fileCache) {
					if (!fileCache.ContainsKey (filename)) {
						if (!File.Exists (filename)) {
							return null;
						}

						fileCache.Add (filename, File.ReadAllBytes (filename));
					}

					return fileCache [filename];
				}
			} catch (Exception e) {
				Log.Out ("Error in SimpleCache.GetFileContent: " + e);
			}
			return null;
		}

	}
}


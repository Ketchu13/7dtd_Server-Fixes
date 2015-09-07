using System;
using System.Collections.Generic;
using System.IO;

namespace AllocsFixes.FileCache
{
	// Special "cache" for map tile folder as both map rendering and webserver access files in there.
	// Only map rendering tiles are cached. Writing is done by WriteThrough.
	public class MapTileCache : AbstractCache
	{
		private struct CurrentZoomFile
		{
			public string filename;
			public byte[] data;
		}

		private CurrentZoomFile[] cache;

		public MapTileCache ()
		{
		}

		public void SetZoomCount (int count)
		{
			cache = new CurrentZoomFile[count];
		}

		public byte[] LoadTile (int zoomlevel, string filename)
		{
			try {
				lock (cache) {
					if (cache [zoomlevel].filename == null || !cache [zoomlevel].filename.Equals (filename)) {
						cache [zoomlevel].filename = filename;

						if (!File.Exists (filename)) {
							cache [zoomlevel].data = null;
							return null;
						}

						cache [zoomlevel].data = File.ReadAllBytes (filename);
					}
					return cache [zoomlevel].data;
				}
			} catch (Exception e) {
				Log.Out ("Error in MapTileCache.LoadTile: " + e);
			}
			return null;
		}

		public void SaveTile (int zoomlevel, byte[] content)
		{
			try {
				lock (cache) {
					if (cache [zoomlevel].filename != null) {
						cache [zoomlevel].data = content;
						File.WriteAllBytes (cache [zoomlevel].filename, content);
					}
				}
			} catch (Exception e) {
				Log.Out ("Error in MapTileCache.SaveTile: " + e);
			}
		}

		public override byte[] GetFileContent (string filename)
		{
			try {
				lock (cache) {
					foreach (CurrentZoomFile czf in cache) {
						if (czf.filename != null && czf.filename.Equals (filename))
							return czf.data;
					}

					if (!File.Exists (filename)) {
						return null;
					}
					return File.ReadAllBytes (filename);
				}
			} catch (Exception e) {
				Log.Out ("Error in MapTileCache.GetFileContent: " + e);
			}
			return null;
		}

	}
}


using System;
using System.Collections.Generic;
using System.Threading;

namespace AllocsFixes
{
	public class BlockingQueue<T>
	{
		private bool closing = false;
		private Queue<T> queue = new Queue<T> ();

		public void Enqueue (T item)
		{
			lock (queue) {
				queue.Enqueue (item);
				Monitor.PulseAll (queue);
			}
		}

		public T Dequeue ()
		{
			lock (queue) {
				while (queue.Count == 0) {
					if (closing) {
						return default(T);
					}
					Monitor.Wait (queue);
				}
				return queue.Dequeue ();
			}
		}

		public void Close ()
		{
			lock (queue) {
				closing = true;
				Monitor.PulseAll (queue);
			}
		}

	}
}


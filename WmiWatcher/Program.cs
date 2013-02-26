using System;
using System.Management;
public class RemoteConnect
{
	public static void Main()
	{
		const string wmiNamespace = @"root\Imhotep\StarCommerce";
		const string eventName = "SessionStatusEvent";

		string query = string.Format("SELECT * FROM {0}", eventName);

		// Event options
		// blockSize = 1, so wait for 1 event to return
		EventWatcherOptions options = new EventWatcherOptions(null, TimeSpan.MaxValue, 1);

		// Initialize an event watcher and subscribe to events 
		// that match this query
		ManagementEventWatcher watcher =
			new ManagementEventWatcher(
			new ManagementScope(wmiNamespace),
			new EventQuery(query), options);

		Console.WriteLine(
			"Open an application (notepad.exe) to trigger an event.");

		while (true)
		{
			ManagementBaseObject e = watcher.WaitForNextEvent();

			int x = e.Properties.Count;
			Console.Write(x.ToString());

			PropertyDataCollection.PropertyDataEnumerator etor = e.Properties.GetEnumerator();
			while (etor.MoveNext())
			{
				Console.WriteLine(etor.Current.Name + " : " + etor.Current.Value);
			}
		}

	}
}
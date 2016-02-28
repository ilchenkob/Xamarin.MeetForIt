using System;
using LocationMonitor.Models;

namespace LocationMonitor.Services
{
	public interface ILocationService
	{
		void Run();

		void Stop();

		event Action<Location> LocationUpdated;
	}
}


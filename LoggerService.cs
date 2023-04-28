using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
	public sealed class LoggerService
	{
		private static LoggerService? _instance;
		private const string PATH = @"D:\projects\dotnet\EntityFramework\ExLog.txt";

		private LoggerService()
		{
			File.WriteAllText(PATH, string.Empty);
		}
		public static LoggerService GetLogger()
		{
			_instance ??= new LoggerService();
			return _instance;
		}
		internal async Task WriteLogAsync(string message)
		{
			using StreamWriter sw = new(PATH, true);
			await sw.WriteLineAsync(message);
		}

	}
}

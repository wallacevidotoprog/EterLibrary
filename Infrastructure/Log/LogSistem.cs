using System.Text;
using System.Text.Json;

namespace EterLibrary.Infrastructure.Log
{
	public static class LogSistem
	{
		public static string dir = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "LOG";
		public static string fileLog = Path.Combine(dir, "LOGSISTEM_DB.log");
		public static void InitLogger()
		{
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			LogWrite($"# => LOG ETERPHARMAPRO  {DateTime.Now} ");

		}

		public static void LogWrite(string msg)
		{
			if (!File.Exists(fileLog))
			{
				using (TextWriter tw = new StreamWriter(fileLog, false, Encoding.Default))
				{
					tw.Close();
				}
			}
			using (StreamWriter sw = File.AppendText(fileLog))
			{
				sw.WriteLine(msg);
			}
		}
		public static void LogWriteLog(TypeLog typeLog, object title, object msgData, object data)
		{

			InitLogger();

			if (data != null)
			{
				try
				{
					string entityJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
					data = entityJson;
				}
				catch (Exception)
				{


				}
			}


			string msg = $"=>\t{typeLog.ToString().PadRight(7)}" +
				$"\n" +
				$"=> {title}" +
				$"\n" +
				$"=> {msgData}" +
				$"\n" +
				$"=> DADOS:  {data}\n\n";
			LogWrite(msg);
		}

		public static List<string[]> LogGet()
		{
			List<string[]> tempLog = null;
			if (File.Exists(fileLog))
			{
				tempLog = new List<string[]>();
				using (StreamReader sr = new StreamReader(fileLog))
				{
					while (!sr.EndOfStream)
					{
						string temp = sr.ReadLine();
						if (temp != "" && temp != null)
						{
							tempLog.Add(temp.Split('-').Select(x => x.Trim().Replace("\t", null)).ToArray());
						}
					}
					sr.Close();
				}
			}

			return tempLog;
		}
	}

	public enum TypeLog
	{
		NONE, INSERT, UPDATE, DELETE, ERROR, VIEW
	}
}

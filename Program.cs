using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace daily
{
	class Program
	{
		private static Timer timer;
		private static DateTime target1;
		private static DateTime target2;
		private static string lastCaption = "";

		static void Main(string[] args)
		{
			try
			{
				Console.CursorVisible = false;
				DateTime now = DateTime.Now;
				target1 = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0);
				target2 = new DateTime(now.Year, now.Month, now.Day, 17, 30, 0);
				using (timer = new Timer(100))
				{
					timer.Elapsed += new ElapsedEventHandler(OnTimer);
					timer.AutoReset = false;
					timer.Enabled = true;
					Console.ReadLine();
					timer.Enabled = false;
					timer.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				Console.CursorVisible = true;
			}
		}

		static void OnTimer(object sender, ElapsedEventArgs args)
		{
			try
			{
				DateTime now = DateTime.Now;
				if (string.Compare(Console.Title, now.ToString("hh:mm tt")) != 0)
					Console.Title = now.ToString("hh:mm:ss tt").ToLower();
				DateTime t0000 = new DateTime(now.Year, now.Month, now.Day);
				DateTime t0830 = t0000.AddMinutes(510);
				DateTime t1200 = t0000.AddMinutes(720);
				DateTime t1300 = t0000.AddMinutes(780);
				DateTime t1730 = t0000.AddMinutes(1050);
				DateTime t2400 = t0000.AddMinutes(1440);
				DateTime t = now;
				StringBuilder caption = new StringBuilder();
				while (t.CompareTo(t2400) < 0)
				{
					if (t.DayOfWeek == DayOfWeek.Saturday) caption.Append("-");
					else if (t.DayOfWeek == DayOfWeek.Sunday) caption.Append("-");
					else if (t.CompareTo(now) < 0) caption.Append(' ');
					else if (t.CompareTo(t0830) < 0) caption.Append("-");
					else if (t.CompareTo(t1200) < 0) caption.Append("M");
					else if (t.CompareTo(t1300) < 0) caption.Append("L");
					else if (t.CompareTo(t1730) < 0) caption.Append("A");
					t = t.AddMinutes(1);
				}
				if (caption.ToString() != lastCaption)
				{
					Console.SetCursorPosition(0, 0);
					Console.Write(caption.ToString() + " ");
					lastCaption = caption.ToString();
				}
				timer.Enabled = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}

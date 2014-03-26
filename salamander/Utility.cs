using System;
using System.Diagnostics;

namespace salamander
{
	public class Utility
	{
		public static string multiMarkdownPath;

		public static string MultiMarkdownPath {
			get {
				return multiMarkdownPath;
			}
			set {
				multiMarkdownPath = value;
			}
		}

		public static string GetMarkdownHeaderValue(string headerLine)
		{
			return headerLine.Substring (headerLine.IndexOf (":") + 1).Trim ();
		}
		
		public static string MarkdownToHtml (string Markdown)
		{
			Process p = new Process();
			ProcessStartInfo psi = new ProcessStartInfo();
			
			psi.FileName = multiMarkdownPath;
			psi.RedirectStandardInput = true;
			psi.RedirectStandardOutput = true;
			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			
			p.StartInfo = psi;
			p.Start ();
			
			p.StandardInput.Write (Markdown);
			p.StandardInput.Close ();
			
			string html = p.StandardOutput.ReadToEnd ();
			
			p.WaitForExit ();
			p.Close ();
			
			return html;
		}

	}
}


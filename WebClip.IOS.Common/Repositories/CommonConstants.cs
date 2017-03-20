using System;
using System.IO;

namespace WebClip.IOS.Common
{
	public class CommonConstants
	{
			public static string DBName = "WeClip.db";
			public static string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DBName);

	}
}

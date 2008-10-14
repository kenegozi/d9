using System.Text;

namespace D9.QuerySpecBuilder.Helpers
{
	public static class IntExtensions
	{
		public static string Times(this int times, char input)
		{
			return new string(input, times);
		}

		public static string Times(this int times, string input)
		{
			StringBuilder builder = new StringBuilder(input.Length * times);
			for (int i = 0; i < times; i++)
			{
				builder.Append(input);
			}
			return builder.ToString();
		}
	}
}


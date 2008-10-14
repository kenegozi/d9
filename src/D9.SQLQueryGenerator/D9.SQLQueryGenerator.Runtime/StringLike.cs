namespace D9.SQLQueryGenerator.Runtime
{
	public class StringLike
	{
		public static implicit operator string(StringLike stringLike)
		{
			return stringLike.ToString();
		}
	}
}

namespace D9.SQLQueryGenerator.Runtime.Model
{
	/// <summary>
	/// Represents a SQL parameter
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Parameter<T> : IOperateable<T>
	{
		private readonly string name;

		/// <summary>
		/// Creates a new named parameter 
		/// </summary>
		/// <param name="name"></param>
		public Parameter(string name)
		{
			this.name = name;
		}

		public override string ToString()
		{
			return "@" + name;
		}

		/// <summary>
		/// SQL string represented by the current Parameter
		/// </summary>
		/// <param name="parameter">The parameter</param>
		/// <returns>SQL fragment</returns>
		public static implicit operator string(Parameter<T> parameter)
		{
			return parameter.ToString();
		}
	}
}
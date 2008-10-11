namespace D9.NHibernate.Tests.UserTypes.DescribedEnumStringType
{
	/// <summary>
	/// 
	/// </summary>
	public enum Colours
	{
		Yellow,

		[System.ComponentModel.Description("B")]
		Blue,

		Green
	}
}
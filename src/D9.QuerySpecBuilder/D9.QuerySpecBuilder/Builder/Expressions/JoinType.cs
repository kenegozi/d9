namespace D9.QuerySpecBuilder.Builder.Expressions
{
	/// <summary>
	/// Models types of possible joins
	/// </summary>
    public class JoinType
    {
        private static readonly JoinType inner = new InnerJoin();
        private static readonly JoinType left = new LeftJoin();
        private static readonly JoinType right = new RightJoin();

		/// <summary>
		/// INNER JOIN
		/// </summary>
        public static JoinType Inner
        {
            get
            {
                return inner;
            }
        }

		/// <summary>
		/// OUTER LEFT JOIN
		/// </summary>
        public static JoinType Left
        {
            get
            {
                return left;
            }
        }

		/// <summary>
		/// OUTER RIGHT JOIN
		/// </summary>
        public static JoinType Right
        {
            get
            {
                return right;
            }
        }

		/// <summary>
		/// Representing an INNER JOIN
		/// </summary>
        public class InnerJoin : JoinType
        {
            public override string ToString()
            {
                return "INNER JOIN\t";
            }
        }

		/// <summary>
		/// Representing an OUTER LEFT JOIN
		/// </summary>
		public class LeftJoin : JoinType
        {
            public override string ToString()
            {
                return "LEFT JOIN\t";
            }
        }

		/// <summary>
		/// Representing an OUTER RIGHT JOIN
		/// </summary>
        public class RightJoin : JoinType
        {
            public override string ToString()
            {
                return "RIGHT JOIN\t";
            }
        }
    }
}


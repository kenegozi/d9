#region License

// Copyright (c) 2008 Ken Egozi (ken@kenegozi.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of the D9 project nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion License

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


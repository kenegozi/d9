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

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// Provide access to the file system
	/// </summary>
	public interface IFileSystemAdapter
	{
		/// <summary>
		/// Gets script files (*.js) from a given directory
		/// </summary>
		/// <param name="dir">The given directory</param>
		/// <returns>A string array of filenames</returns>
		string[] GetScriptFiles(string dir);

		/// <summary>
		/// Gets style files (*.css) from a given directory
		/// </summary>
		/// <param name="dir">The given directory</param>
		/// <returns>A string array of filenames</returns>
		string[] GetStyleFiles(string dir);

		/// <summary>
		/// Gets image files (*.png <c>etc</c>.) from a given directory
		/// </summary>
		/// <param name="dir">The given directory</param>
		/// <returns>A string array of filenames</returns>
		string[] GetImageFiles(string dir);

		/// <summary>
		/// Gets a list of subdirectories of a given directory
		/// </summary>
		/// <param name="dir">The given directory</param>
		/// <returns>A string array of directory names</returns>
		string[] GetDirectories(string dir);

		/// <summary>
		/// Extract the directory name off a given directory path
		/// </summary>
		/// <param name="dirPath">The directory path</param>
		/// <returns>The directory's name</returns>
		string GetDirectoryName(string dirPath);
	}
}
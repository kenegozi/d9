#region License

// Copyright (c) 2008-2010 Ken Egozi (ken@kenegozi.com)
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

import System
import System.Collections.Generic
import System.IO
import System.Text
import System.Xml.XPath
import System.Xml.Xsl

class Transform:

	Xsl as string

	Input as string
	
	InputDir as string

	OutputPath = "out.xml"
	
	def Execute():
		stylesheet = XslTransform()
		stylesheet.Load(Xsl)
		
		buffer = StringBuilder()
		
		if InputDir != null:
			for file in GetFiles(InputDir):
				Handle(file, buffer, stylesheet)
		else:
			Handle(Input, buffer, stylesheet)

		File.WriteAllText(OutputPath, buffer.ToString())
	
	def Handle(file as string, buffer as StringBuilder, stylesheet as XslTransform):
		using reader = StreamReader(file):
			buffer.AppendLine(GetMarkupFrom(reader, stylesheet))
		
	def GetMarkupFrom(reader as StreamReader, stylesheet as XslTransform):
		xmlDoc = XPathDocument(reader)
		writer = StringWriter()
		stylesheet.Transform(xmlDoc, null, writer, null)
		return writer.ToString()

	def GetFiles(dir as string) as IEnumerable of string:
		exts = ["*.xml"]
		for ext in exts:
			for file in Directory.GetFiles(dir, ext):
				yield file 
				
	def Init():
		for arg in Environment.GetCommandLineArgs():
			parts = arg.Split(('=',), 2, StringSplitOptions.RemoveEmptyEntries)
			continue if parts.Length < 2
			if parts[0] == "out":
				OutputPath = parts[1]
			elif parts[0] == "in":
				Input = parts[1]
			elif parts[0] == "xsl":
				Xsl = parts[1]
			elif parts[0] == "dir":
				InputDir = parts[1]
		
		if Input == null and InputDir == null:
			print "you must either supply in=INPUTFILE or dir=INPUT_DIR"
			Environment.Exit(-1) 

		if Xsl == null:
			print "you must supply xsl=STYLESHEET"
			Environment.Exit(-2) 
			
		return self


Transform().Init().Execute()	

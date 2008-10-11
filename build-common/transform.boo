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

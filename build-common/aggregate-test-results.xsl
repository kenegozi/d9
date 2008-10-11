<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxsl='urn:schemas-microsoft-com:xslt'
	xmlns:csharp='urn:CustomScript'>
	
<xsl:output method="xml" omit-xml-declaration="yes" encoding="UTF-8" indent="yes"/>
	
<msxsl:script implements-prefix='csharp' language='C#'>
	<![CDATA[
	public string GetFixtureNameFrom(string name)
	{
		return name.Substring(0, name.LastIndexOf("."));
			name.Substring(name.LastIndexOf(".") + 1);
	}
	public string GetTestNameFrom(string name)
	{
		return name.Substring(name.LastIndexOf(".") + 1);
	}
	]]>
</msxsl:script>

<xsl:key name="byfixture" match="test-results//test-case" use='csharp:GetFixtureNameFrom(@name)'/>

	<xsl:template match="/">
		<suite>
<!-- -->
			<xsl:for-each select="test-results//test-case[generate-id() = generate-id(key('byfixture', csharp:GetFixtureNameFrom(@name))[1])]">

<!-- -->
<!--
			<xsl:for-each select="test-results//test-case">
-->
				<xsl:sort select="@name" order="ascending" data-type="text" />
				<xsl:variable name="fixture" select="csharp:GetFixtureNameFrom(@name)" />
					
				<fixture name="{$fixture}">
<!-- 
					<xsl:copy-of select="key('byfixture', csharp:GetFixtureNameFrom(@name))" />
-->
					<xsl:for-each select="key('byfixture', csharp:GetFixtureNameFrom(@name))">
						<xsl:element name="test-case">
							<xsl:attribute name="name">
								<xsl:value-of select="csharp:GetTestNameFrom(@name)" />
							</xsl:attribute>
							<xsl:choose>
								<xsl:when test="@executed = 'False'">
									<xsl:attribute name="result">ignored</xsl:attribute>
								</xsl:when>
								<xsl:when test="@executed = 'True' and @success = 'False'">
									<xsl:attribute name="result">failed</xsl:attribute>
								</xsl:when>
								<xsl:when test="@executed = 'True' and @success = 'True'">
									<xsl:attribute name="result">passed</xsl:attribute>
								</xsl:when>
							</xsl:choose>
							<xsl:attribute name="time">
								<xsl:value-of select="@time" />
							</xsl:attribute>
							<xsl:attribute name="asserts">
								<xsl:value-of select="@asserts" />
							</xsl:attribute>
						</xsl:element>
					</xsl:for-each>
				</fixture>
			</xsl:for-each>
		</suite>
	</xsl:template>

</xsl:stylesheet>
<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxsl='urn:schemas-microsoft-com:xslt'
	xmlns:csharp='urn:CustomScript'>
	
<xsl:output method="html"/>
	
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


	<xsl:template match="fixture">
		
		<xsl:variable name="passed"	select="count(test-case[@result = 'passed'])" />
		<xsl:variable name="failed"	select="count(test-case[@result = 'failed'])" />
		<xsl:variable name="ignored"	select="count(test-case[@result = 'ignored'])" />
		<xsl:variable name="executed"	select="count(test-case[@result != 'ignored'])" />
	
		<xsl:variable name="ok"			select="$failed = 0" />
	
		<div class="fixture">
			<h4><xsl:value-of select="@name" /></h4>
			<xsl:element name="p">
				<xsl:choose>
					<xsl:when test="$ok">
						<xsl:attribute name="class">summary ok</xsl:attribute>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="class">summary not-ok</xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>
				(
				<span title="executed">	<xsl:value-of select="$executed" /></span> /
				<span title="passed">	<xsl:value-of select="$passed" /></span> /
				<span title="failed">	<xsl:value-of select="$failed" /></span> /
				<span title="ignored">	<xsl:value-of select="$ignored" /></span>  )
			</xsl:element>
			
			<xsl:apply-templates select="test-case" />
		</div>
	</xsl:template>

	<xsl:template match="test-case">
		<xsl:element name="div">
			<xsl:attribute name="class">test-case test-case-<xsl:value-of select="@result" /></xsl:attribute>
			<xsl:value-of select="@name" />
		</xsl:element>
	</xsl:template>


</xsl:stylesheet>
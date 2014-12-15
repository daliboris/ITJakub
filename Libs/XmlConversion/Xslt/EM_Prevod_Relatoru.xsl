<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xd="http://www.oxygenxml.com/ns/doc/xsl"
	exclude-result-prefixes="xd"
	version="1.0">
	<xd:doc scope="stylesheet">
		<xd:desc>
			<xd:p><xd:b>Created on:</xd:b> Jan 4, 2011</xd:p>
			<xd:p><xd:b>Author:</xd:b> boris</xd:p>
			<xd:p></xd:p>
		</xd:desc>
	</xd:doc>
	
	<xsl:include href="Kopirovani_prvku.xsl"/>

	<xsl:template match="/">
		<xsl:comment> EM_Prevod_relatoru </xsl:comment>
		<xsl:apply-templates />
	</xsl:template>
	

	<xsl:template match="note">
		<xsl:variable name="predchazi">
			<xsl:value-of select="name(preceding-sibling::*[position() = 1])"/>
		</xsl:variable>
		<xsl:variable name="predchazitype">
			<xsl:value-of select="(preceding-sibling::*[position() = 1])/@type"/>
		</xsl:variable>
		<xsl:variable name="predchaziid">
			<xsl:value-of select="(preceding-sibling::*[position() = 2])/@xml:id"/>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$predchazi = 'anchor' and $predchazitype = 'end'">
				<xsl:element name="note">
					<xsl:attribute name="from"><xsl:value-of select="$predchaziid"/></xsl:attribute>
					<xsl:apply-templates />
				</xsl:element>
			</xsl:when>
			<xsl:otherwise>
				<xsl:copy>
					<xsl:apply-templates select="@*" />
					<xsl:apply-templates />
				</xsl:copy>
			</xsl:otherwise>
		</xsl:choose>
		
	</xsl:template>
	
	<xsl:template match="add[@type='contemporaneous' and @place='inline' and @hand]">
		<xsl:choose>
			<xsl:when test="name(preceding-sibling::*[position() = 1]) = 'anchor'">
				<xsl:element name="app">
					<xsl:attribute name="from">
						<xsl:text>#</xsl:text><xsl:value-of select="preceding-sibling::anchor[@type='start'][1]/@xml:id"/>
					</xsl:attribute>
					<xsl:element name="rdg">
						<xsl:attribute name="hand"><xsl:value-of select="@hand"/></xsl:attribute>
						<xsl:apply-templates />
					</xsl:element>
				</xsl:element>
			</xsl:when>
			<!-- pro případy, že jde o přípisek bez vztahu k předchozímu textu; nejspíš převést na add -->
			<xsl:otherwise>
				<xsl:copy>
					<xsl:apply-templates select="@*" />
				<xsl:apply-templates />
				</xsl:copy>
			</xsl:otherwise>
			
		</xsl:choose>
	</xsl:template>
	
	<xsl:template match="anchor[@type='end']"/>
	
</xsl:stylesheet>
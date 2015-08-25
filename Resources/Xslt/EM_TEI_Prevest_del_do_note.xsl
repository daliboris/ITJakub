<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xd="http://www.oxygenxml.com/ns/doc/xsl"
	xmlns:tei="http://www.tei-c.org/ns/1.0"
	exclude-result-prefixes="xd tei"
	version="1.0">
	<xd:doc scope="stylesheet">
		<xd:desc>
			<xd:p><xd:b>Created on:</xd:b> Dec 10, 2014</xd:p>
			<xd:p><xd:b>Author:</xd:b> Boris</xd:p>
			<xd:p>Přiřadí jenotlivám významonosným prvkům jedinečné ID spočívacící v identifikaci předchozích prvků.</xd:p>
		</xd:desc>
	</xd:doc>
	
	<xsl:include href="Kopirovani_prvku.xsl"/>
	
	
	<xsl:output omit-xml-declaration="no" indent="yes"/>
	<xsl:strip-space elements="*"/>
	
	<xsl:template match="/">
		<xsl:comment> EM_TEI_Prevest_del_do_note </xsl:comment>
		<xsl:apply-templates />
	</xsl:template>
	
	<xsl:template match="tei:del[not(parent::*[1]/self::tei:note)]">
		<xsl:element name="note" namespace="http://www.tei-c.org/ns/1.0">
			<xsl:copy-of select="."/>
		</xsl:element>
	</xsl:template>

	
</xsl:stylesheet>
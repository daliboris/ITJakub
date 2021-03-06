<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xd="http://www.oxygenxml.com/ns/doc/xsl"
	exclude-result-prefixes="xd"
	version="1.0">
  
  <xsl:strip-space elements="*"/>
	<!--<xsl:preserve-space elements="text"/>-->
	<xsl:output indent="no" />
  
	<xd:doc scope="stylesheet">
		<xd:desc>
			<xd:p><xd:b>Created on:</xd:b> Dec 1, 2010</xd:p>
			<xd:p><xd:b>Author:</xd:b> boris</xd:p>
			<xd:p>Přesune element foliace před značku odstavce, nadpisu atp., pokud jde o první značku</xd:p>
		</xd:desc>
	</xd:doc>
  
	<xsl:include href="Kopirovani_prvku.xsl"/>
  
  <xd:doc>
    <xd:desc>
      <xd:p>Platí pro všechny elementy, jejichž prvním podřízeným elementem je <xd:b>foliace</xd:b>.</xd:p>
    </xd:desc>
  </xd:doc>
	
	<xsl:template match="/">
		<xsl:comment> Presunout_foliaci_pred_odstavec </xsl:comment>
		<xsl:apply-templates />
	</xsl:template>
	
	<xsl:template match="body" priority="10">
		<xsl:copy>
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>
	
	<!--<xsl:template match="l[name(*[1]) = ('foliace' or 'pb')]">
    <xsl:copy-of select="child::*[1]"/>
    <xsl:element name="{local-name()}">
      <xsl:apply-templates select="child::*[position() &gt; 1]" />
    </xsl:element>
  </xsl:template>-->
	
	<xsl:template match="*[*[1]/self::pb] | *[*[1]/self::foliace]">
		<xsl:copy-of select="child::*[1]"/>
		<xsl:choose>
			<!-- po foliaci následuje poznámka, aniž je mezi nimi textový uzel -->
			<xsl:when test="node()[2][self::note]">
				<xsl:copy-of select="node()[2][self::*]"/>
				<xsl:element name="{local-name()}">
					<xsl:copy-of select="@*" />
					<xsl:apply-templates select="node()[position() &gt; 2]" />
				</xsl:element>
			</xsl:when>
			<xsl:otherwise>
				<xsl:element name="{local-name()}">
					<xsl:copy-of select="@*" />
					<xsl:apply-templates select="node()[position() &gt; 1]" />
				</xsl:element>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
</xsl:stylesheet>

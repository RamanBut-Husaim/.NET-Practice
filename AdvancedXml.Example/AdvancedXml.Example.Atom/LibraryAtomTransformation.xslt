<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:lib="http://library.by/catalog"
    xmlns:at="http://www.w3.org/2005/Atom"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="msxsl">

  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/lib:catalog">
    <xsl:element name="feed">
      <xsl:element name="title">
        <xsl:text>Books</xsl:text>
      </xsl:element>
      <xsl:element name="subtitle">
        <xsl:text>New books</xsl:text>
      </xsl:element>
      <xsl:element name="link">
        <xsl:attribute name="href">
          <xsl:text>http://library.by/catalog/feed</xsl:text>
        </xsl:attribute>
        <xsl:attribute name="rel">
          <xsl:text>self</xsl:text>
        </xsl:attribute>
      </xsl:element>
      <xsl:element name="link">
        <xsl:attribute name="href">
          <xsl:text>http://library.by/catalog</xsl:text>
        </xsl:attribute>
      </xsl:element>
      <xsl:element name="id">
        <xsl:text>urn:uuid:D72CA876-F523-4557-8B12-A07A2495D0DC</xsl:text>
      </xsl:element>
      <xsl:element name="updated">
        <xsl:text>2016-01-01</xsl:text>
      </xsl:element>
    </xsl:element>
    <xsl:apply-templates/>
  </xsl:template>

  <xsl:template match="text() | @*"/>
  <!--<xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>-->
</xsl:stylesheet>

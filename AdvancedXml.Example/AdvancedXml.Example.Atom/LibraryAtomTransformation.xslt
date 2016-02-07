<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:lib="http://library.by/catalog"
    xmlns:at="http://www.w3.org/2005/Atom"
    xmlns:user="urn:custom-scripts"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="msxsl">

  <msxsl:script implements-prefix='user' language='CSharp'>
    <![CDATA[
    public string uuid() {
      return System.Guid.NewGuid().ToString();
    }]]>
  </msxsl:script>

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
        <xsl:value-of select="concat('urn:uuid:', user:uuid())"/>
      </xsl:element>
      <xsl:element name="updated">
        <xsl:text>2016-01-01</xsl:text>
      </xsl:element>
      <xsl:apply-templates/>
    </xsl:element>
  </xsl:template>

  <xsl:template match="lib:book">
    <xsl:element name="entry">
      <xsl:element name="title">
        <xsl:value-of select="concat(lib:title, ' by ', lib:author)"></xsl:value-of>
      </xsl:element>
      <xsl:element name="link">
        <xsl:attribute name="href">
          <xsl:variable name="isbn-value" select="lib:isbn"/>
          <xsl:variable name="genre-value" select="lib:genre"/>
          <xsl:if test="$isbn-value != '' and $genre-value = 'Computer'">
            <xsl:value-of select="concat('http://my.safaribooksonline.com/', $isbn-value)"></xsl:value-of>
          </xsl:if>
      </xsl:attribute>
      </xsl:element>
      <xsl:element name="id">
        <xsl:value-of select="concat('urn:uuid:', user:uuid())"/>
      </xsl:element>
      <xsl:element name="updated">
        <xsl:value-of select="lib:registration_date"></xsl:value-of>
      </xsl:element>
      <xsl:element name="summary">
        <xsl:value-of select="lib:description"/>
      </xsl:element>
    </xsl:element>
  </xsl:template>

  <xsl:template match="text() | @*"/>
</xsl:stylesheet>

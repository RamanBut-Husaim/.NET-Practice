<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:lib="http://library.by/catalog"
    xmlns:at="http://www.w3.org/2005/Atom"
    xmlns:user="urn:custom-scripts"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="msxsl">

  <xsl:output method="html" indent="yes"/>

  <xsl:variable name="genres">
    <genre>Computer</genre>
    <genre>Fantasy</genre>
    <genre>Romance</genre>
    <genre>Horror</genre>
    <genre>Science Fiction</genre>
  </xsl:variable>

  <xsl:variable name="header">
    <tr>
      <th>Author</th>
      <th>Title</th>
      <th>Publish Date</th>
      <th>Registration Date</th>
    </tr>
  </xsl:variable>

  <xsl:template match="/lib:catalog">
    <xsl:variable name="books" select="/lib:catalog/lib:book"/>
    <html>
      <body>
        <h1>Текущие фонды по жанрам</h1>
        <xsl:for-each select="msxsl:node-set($genres)/genre">
          <xsl:variable name="selected-genre" select="."/>

          <table style="width:100%; margin-bottom:20px">
            <caption>
                <h3>
                  <xsl:value-of select="$selected-genre"/>
                </h3>
            </caption>
            <xsl:copy-of select="$header"/>
            <xsl:call-template name="book-content-rendered">
              <xsl:with-param name="books-for-genre" select="$books[lib:genre = $selected-genre]"/>
            </xsl:call-template>
          </table>
        </xsl:for-each>

        <div>
          <h4>Grand Total: <xsl:value-of select="count($books)"/></h4>
        </div>
      </body>
    </html>
  </xsl:template>

  <xsl:template name="book-content-rendered">
    <xsl:param name="books-for-genre"/>
    <xsl:for-each select="$books-for-genre">
      <tr>
        <td>
          <xsl:value-of select="lib:author"/>
        </td>
        <td>
          <xsl:value-of select="lib:title"/>
        </td>
        <td>
          <xsl:value-of select="lib:publish_date"/>
        </td>
        <td>
          <xsl:value-of select="lib:registration_date"/>
        </td>
      </tr>
    </xsl:for-each>
    <tr>
      <td>
        <b>Total:</b>
      </td>
      <td colspan="3">
        <xsl:value-of select="count($books-for-genre)"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="text() | @*"/>
</xsl:stylesheet>

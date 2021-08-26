<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions"  xmlns:oos="http://zakupki.gov.ru/oos/types/1"
xmlns:b="http://zakupki.gov.ru/oos/export/1">
<xsl:template match="/">
<xsl:for-each select="//b:nsiOKVED">select <xsl:value-of select="oos:id"/> as Id, '<xsl:value-of select="oos:code"/>' as Code, '<xsl:value-of select="oos:name"/>' as Name, <xsl:if test="string-length(oos:parentId)!=0"><xsl:value-of select="oos:parentId"/></xsl:if><xsl:if test="string-length(oos:parentId)=0">null</xsl:if> ParentId, 0 as LastUpdateTick union all
</xsl:for-each>
</xsl:template>	
</xsl:stylesheet>

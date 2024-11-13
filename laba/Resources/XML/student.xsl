<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" encoding="UTF-8" />
	<xsl:param name="keyword" select="''" />

	<xsl:template match="/">
		<html>
			<head>
				<title>Інформація про студентів</title>
				<style>
					table {
					width: 100%;
					border-collapse: collapse;
					}
					th, td {
					border: 1px solid black;
					padding: 8px;
					text-align: left;
					}
					th {
					background-color: #f2f2f2;
					}
				</style>
			</head>
			<body>
				<h2>Інформація про студентів</h2>
				<table>
					<tr>
						<th>ID</th>
						<th>Ім'я</th>
						<th>Факультет</th>
						<th>Рівень освіти</th>
						<th>Курс</th>
						<th>Предмети та оцінки</th>
						<th>Рейтинг (середнє)</th>
					</tr>
					<xsl:for-each select="//Студент[
                        contains(Ім_я, $keyword) or 
                        contains(@Факультет, $keyword) or 
                        contains(@РівеньОсвіти, $keyword) or 
                        contains(@Курс, $keyword) or 
                        Предмет[contains(@Назва, $keyword)]
                    ]">
						<tr>
							<td>
								<xsl:value-of select="@ID" />
							</td>
							<td>
								<xsl:value-of select="Ім_я" />
							</td>
							<td>
								<xsl:value-of select="@Факультет" />
							</td>
							<td>
								<xsl:value-of select="@РівеньОсвіти" />
							</td>
							<td>
								<xsl:value-of select="@Курс" />
							</td>
							<td>
								<xsl:for-each select="Предмет">
									<p>
										<xsl:value-of select="@Назва" /> - <xsl:value-of select="@Оцінка" />
									</p>
								</xsl:for-each>
							</td>
							<td>
								<xsl:variable name="sum" select="sum(Предмет/@Оцінка)" />
								<xsl:variable name="count" select="count(Предмет)" />
								<xsl:value-of select="$sum div $count" />
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>

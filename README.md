SoftwareKobo.CnblogsAPI
=======================

#博客园 API#
##如何使用？##
##1.从 Nuget 下载：##
https://www.nuget.org/packages/SoftwareKobo.CnblogsAPI/
<br/>
##2.编写代码：##
<br/>
###2.1.博客相关：###
<table>
<tr>
<td>
最新博客文章
</td>
<td>
<pre><code>
articles = await BlogService.RecentAsync(1, 15);
</code></pre>
</td>
</tr>
</table>
<br/>
###2.2.新闻相关：###
<table>
<tr>
<td>
最新新闻
</td>
<td>
<pre><code>
news = await NewsService.RecentAsync(1, 15);
</code></pre>
</td>
</tr>
</table>

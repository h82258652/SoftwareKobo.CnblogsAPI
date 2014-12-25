SoftwareKobo.CnblogsAPI
=======================

#博客园 API#
##如何使用？##
##1.从 Nuget 下载：##
https://www.nuget.org/packages/SoftwareKobo.CnblogsAPI/
<br/>
##2.编写代码：##
###2.1.博客相关：###
<table>
  <tr>
    <td>最新博客文章</td>
    <td>
      <pre><code>articles = await BlogService.RecentAsync(1, 15);</code></pre>
    </td>
  </tr>
</table>
###2.2.新闻相关：###
<table>
  <tr>
    <td>最新新闻</td>
    <td>
      <pre><code>news = await NewsService.RecentAsync(1, 15);</code></pre>
    </td>
  </tr>
</table>
###2.3.评论相关：###
<table>
  <tr>
    <td>登录</td>
    <td>
      <pre><code>cookie = await UserService.LoginAsync(username, password);</code></pre>
    </td>
  </tr>
  <tr>
    <td>发送新闻评论</td>
    <td>
      <pre><code>response = await UserService.SendNewsCommentAsync(cookie, newsId, "评论内容，至少 3 个字符", replyId);</code></pre>
    </td>
  </tr>
</table>

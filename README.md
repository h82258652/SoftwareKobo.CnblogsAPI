SoftwareKobo.CnblogsAPI
=======================

#博客园 API#
##如何使用？##
##1.从 Nuget 下载：##
https://www.nuget.org/packages/SoftwareKobo.CnblogsAPI/
<br/>
##2.编写代码：##
###2.1.博客相关：###
<table><tbody>
  <tr>
    <td>最新博客文章</td>
    <td>articles = await BlogService.RecentAsync(1, 15);</td>
  </tr>
</tbody></table>
###2.2.新闻相关：###
<table><tbody>
  <tr>
    <td>最新新闻</td>
    <td>news = await NewsService.RecentAsync(1, 15);</td>
  </tr>
</tbody></table>
###2.3.评论相关：###
<table><tbody>
  <tr>
    <td>登录</td>
    <td>cookie = await UserService.LoginAsync(username, password);</td>
  </tr>
  <tr>
    <td>发送新闻评论</td>
    <td>response = await UserService.SendNewsCommentAsync(cookie, newsId, "评论内容，至少 3 个字符", replyId);</td>
  </tr>
</tbody></table>

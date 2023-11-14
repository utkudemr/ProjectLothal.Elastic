

using Nest;

namespace ProjectLothal.ElasticSearch.Domain.Models;

public class Blog
{
    [Text(Name = "title")]
    public string Title { get; set; }
    [Text(Name = "content")]
    public string Content { get; set; }
    [Text(Name = "user_id")]
    public string UserId { get; set; }
    [Text(Name = "tags")]
    public string[] Tags { get; set; }
    [Text(Name = "created")]
    public DateTime Created { get; set; }
}

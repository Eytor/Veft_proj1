using System;
using System.Collections.Generic;
using TechnicalRadiation.Models.Entities;

namespace TechnicalRadiation.Repositories.Data
{
  public class DataProvider
  {
    public static List<NewsItem> NewsItems = new List<NewsItem>
    {
      new NewsItem
      {
          Id = 1,
          Title = "news",
          ShortDescription = "",
          LongDescription ="",
          PublishDate = DateTime.Now,
          ModifiedBy = "TechnicalRadiationAdmin",
          CreatedDate = DateTime.Now,
          ModifiedDate = DateTime.Now
      }

    };
  }
}
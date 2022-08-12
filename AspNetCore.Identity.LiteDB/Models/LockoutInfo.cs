using System;
using System.Diagnostics.CodeAnalysis;
using LiteDB;

namespace AspNetCore.Identity.LiteDB.Models
{
   [SuppressMessage("ReSharper", "UnusedMember.Global")]
   public class LockoutInfo
   {
      public DateTimeOffset? EndDate { get; internal set; }
      public bool Enabled { get; internal set; }
      public int AccessFailedCount { get; internal set; }

      [BsonIgnore]
      public bool AllPropertiesAreSetToDefaults =>
         EndDate == null &&
         Enabled == false &&
         AccessFailedCount == 0;
   }
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Golden
    {
        public int Id  { get; internal set; }
        public string Name  { get; internal set; }
    }
}

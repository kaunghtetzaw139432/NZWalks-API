using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain; // Domain Models ရှိတဲ့ နေရာကို reference လုပ်တာပါ

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        // ၁။ Constructor: ဒီနေရာကနေတစ်ဆင့် connection string တွေကို program.cs ကနေ လှမ်းပို့ပေးမှာပါ
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        // ၂။ DbSet: ဒါတွေက Database ထဲမှာ Table တွေအဖြစ် ရောက်သွားမယ့်အရာတွေပါ
        // Java မှာတုန်းက Table တစ်ခုချင်းစီအတွက် Repository ဆောက်သလိုမျိုးပေါ့
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
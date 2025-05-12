using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteQuanLyBanHang.Startup))]
namespace WebsiteQuanLyBanHang
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

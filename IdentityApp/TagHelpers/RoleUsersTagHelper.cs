using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "asp-for-users")]
    public class RoleUsersTagHelper : TagHelper
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleUsersTagHelper(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("asp-for-users")]
        public string RoleId { get; set; } = null!;
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role != null)
            {
                var userNames = new List<string>();
                var users = await _userManager.Users.ToListAsync(); // Tüm kullanıcıları önceden çekin
                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userNames.Add(user.UserName ?? "");
                    }
                }
                output.Content.SetHtmlContent(userNames.Count == 0 ? "Kullanıcı Yok" : setHtml(userNames));
            }
        }

        private string setHtml(List<string> userNames)
        {
            var html = "<ul>";
            foreach (var item in userNames)
            {
                html += "<ul>" + item + "</ul>";
            }
            html += "</ul>";

            return html;
        }
    }
}

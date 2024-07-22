using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != null)
            {
                return RedirectToPage("/EnglishPremierLeaguePage/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var response = await Common.SendRequestWithBody(new { Email, Password }, "api/Login");
            if (response.IsSuccessStatusCode)
            {
                var accessToken = JsonNode.Parse(await response.Content.ReadAsStringAsync())["token"].ToString();
                HttpContext.Session.SetString("accessToken", accessToken);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken);
                var tokenS = jsonToken as JwtSecurityToken;
                var role = tokenS.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
                HttpContext.Session.SetString("Role", role);
                return RedirectToPage("/EnglishPremierLeaguePage/Index");
            }
            ViewData["ErrorMessage"] = "You do not have permission to do this function!";
            return Page();
        }
    }
}

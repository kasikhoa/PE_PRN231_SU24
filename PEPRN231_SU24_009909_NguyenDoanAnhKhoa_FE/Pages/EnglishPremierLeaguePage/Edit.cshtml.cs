using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE.Models;
using Repository.Models;

namespace PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE.Pages.EnglishPremierLeaguePage
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public FootballPlayer FootballPlayer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "1") return Forbid();
            var getURL = $"{Common.BaseURL}/odata/FootballPlayer?$expand=FootballClub&$filter=FootballPlayerId Eq '{id}'";
            var response = await Common.SendGetRequest(getURL, HttpContext.Session.GetString("accessToken"));
            var resJson = JsonNode.Parse(await response.Content.ReadAsStringAsync());
            var items = JsonSerializer.Deserialize<List<FootballPlayer>>(resJson["value"]) ?? new List<FootballPlayer>();

            if (!items.Any())
            {
                return NotFound();
            }
            FootballPlayer = items.FirstOrDefault();
            var getCate = $"{Common.BaseURL}/api/FootballClub";
            var cates = await (await Common.SendGetRequest(getCate, HttpContext.Session.GetString("accessToken"))).Content.ReadFromJsonAsync<List<FootballClub>>();
            ViewData["FootballClubId"] = new SelectList(cates, "FootballClubId", "ClubName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var updateURL = $"{Common.BaseURL}/api/FootballPlayer";
            var response = await Common.SendRequestWithBody(FootballPlayer, updateURL, HttpContext.Session.GetString("accessToken"), "Put");
            return RedirectToPage("./Index");
        }

    }
}

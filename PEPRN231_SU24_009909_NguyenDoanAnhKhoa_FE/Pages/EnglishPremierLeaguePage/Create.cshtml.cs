using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE.Models;
using Repository.Models;

namespace PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE.Pages.EnglishPremierLeaguePage
{
    public class CreateModel : PageModel
    {

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "1") return Forbid();
            var getCate = $"{Common.BaseURL}/api/FootballClub";
            var cates = await (await Common.SendGetRequest(getCate, HttpContext.Session.GetString("accessToken"))).Content.ReadFromJsonAsync<List<FootballClub>>();
            ViewData["FootballClubId"] = new SelectList(cates, "FootballClubId", "ClubName");
            return Page();
        }

        [BindProperty]
        public FootballPlayer FootballPlayer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var createURL = $"{Common.BaseURL}/api/FootballPlayer";
            var response = await Common.SendRequestWithBody(FootballPlayer, createURL, HttpContext.Session.GetString("accessToken"), "Post");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}

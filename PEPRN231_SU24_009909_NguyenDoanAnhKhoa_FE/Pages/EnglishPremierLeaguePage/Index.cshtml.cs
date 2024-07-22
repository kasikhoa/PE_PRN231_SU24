using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE.Models;
using Repository.Models;

namespace PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE.Pages.EnglishPremierLeaguePage
{
    public class IndexModel : PageModel
    {
        public IList<FootballPlayer> FootballPlayer { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchByAchivement { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchByNomination { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "1" && role != "2") return Forbid();
            var getURL = $"{Common.BaseURL}/odata/FootballPlayer?$expand=FootballClub{GetQuery()}";
            var response = await Common.SendGetRequest(getURL, HttpContext.Session.GetString("accessToken"));
            var resJson = JsonNode.Parse(await response.Content.ReadAsStringAsync());
            FootballPlayer = JsonSerializer.Deserialize<List<FootballPlayer>>(resJson["value"]) ?? new List<FootballPlayer>();
            return Page();
        }

        private string GetQuery()
        {
            var query = "&$filter=";
            if (string.IsNullOrEmpty(SearchByAchivement) && string.IsNullOrEmpty(SearchByNomination)) return string.Empty;
            if (!string.IsNullOrEmpty(SearchByAchivement)) query = string.Concat(query, "contains(Achievements,'", SearchByAchivement, "')");
            //if (SearchByNomination.HasValue)
            //{
            //    if (query != "&$filter=")
            //    {
            //        query = string.Concat(query, " Or PublishYear Eq ", SearchByNomination);
            //    }
            //    else
            //    {
            //        query = string.Concat(query, "PublishYear Eq ", SearchByNomination);
            //    }
            //}
            if (!string.IsNullOrEmpty(SearchByNomination)) query = string.Concat(query, "contains(Nomination,'", SearchByNomination, "')");
            return query;
        }
    }
}

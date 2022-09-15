using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using shared;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration config;
        private readonly HttpClient http;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config, HttpClient http)
        {
            _logger = logger;
            this.config = config;
            this.http = http;
        }

        public IEnumerable<Ballot>? Ballots { get; private set; }

        public async void OnGet()
        {
            string apiHost  = config["apiAddress"];
            string url = $"{apiHost}/api/elections/cities";
            Ballots = await http.GetFromJsonAsync<IEnumerable<Ballot>>("/api/elections/cities");
        }
    }
}
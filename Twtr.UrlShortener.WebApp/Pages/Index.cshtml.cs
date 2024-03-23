using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twtr.UrlService;

namespace UrlShortenerCF.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUrlShortener urlShortener;

        [BindProperty(SupportsGet = true)]
        public string? FullUrl { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ShortenedUrl { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IUrlShortener urlShortener)
        {
            _logger = logger;
            this.urlShortener = urlShortener;
        }

        public async void OnGet(Guid? urlId = null)
        {
            if (urlId.HasValue)
            {
                ShortenedUrl = await urlShortener.GetUrl(urlId.Value);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Guid? urlId = Guid.Empty;

            if (FullUrl is not null)
            {
                urlId = await urlShortener.ShortenUrlRequest(FullUrl);
            }
            return RedirectToAction(nameof(OnGet), new { urlId });
        }
    }
}

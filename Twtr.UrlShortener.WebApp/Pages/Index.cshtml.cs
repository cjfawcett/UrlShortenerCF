using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twtr.UrlService;

namespace UrlShortenerCF.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUrlShortener urlShortener;

        [BindProperty]
        public string? FullUrl { get; set; }

        [BindProperty]
        public string? ShortenedUrl { get; set; }

        [BindProperty]
        public Guid? UrlID { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IUrlShortener urlShortener)
        {
            _logger = logger;
            this.urlShortener = urlShortener;
        }

        public async Task OnGet(Guid? urlId)
        {
            if (urlId.HasValue)
            {
                ShortenedUrl = await urlShortener.GetShortUrl(urlId.Value);
                TempData["shortenedUrl"] = ShortenedUrl;
            }
        }

        public async Task<IActionResult> OnPostShortenAsync()
        {
            if (FullUrl is not null)
            {
                UrlID = await urlShortener.ShortenUrlRequest(FullUrl);
            }
            return RedirectToAction(nameof(OnGet), new { UrlID });
        }

        public async Task<IActionResult> OnPostNavigateAsync()
        {
            ShortenedUrl = TempData["shortenedUrl"]?.ToString();
            var urlKey = ShortenedUrl?.Substring(ShortenedUrl.LastIndexOf('/') + 1);
            var originalUrl = await urlShortener.GetFullUrl(urlKey);
            return Redirect(originalUrl);
        }
    }
}

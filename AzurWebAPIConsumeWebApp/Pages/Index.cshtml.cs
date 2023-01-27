using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;

namespace AzurWebAPIConsumeWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        public string content;

        public IndexModel(ILogger<IndexModel> logger, ITokenAcquisition tokenAcquisition)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;

        }

        public async Task OnGet()
        {
            string[] scope = new string[] { "api://2135be57-58dd-4460-90a3-96ebd4c4c4a2/products" };
            string accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scope);


            string apiURL = "https://azureapianish.azure-api.net/api/Products";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage responseMessage = await client.GetAsync(apiURL);
            content = await responseMessage.Content.ReadAsStringAsync();



        }
    }
}
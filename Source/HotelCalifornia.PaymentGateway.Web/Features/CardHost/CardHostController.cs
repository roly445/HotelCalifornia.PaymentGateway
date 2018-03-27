using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using HotelCalifornia.PaymentGateway.Domain.IntegrationAggregate;
using HotelCalifornia.PaymentGateway.Domain.PaymentSessionAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

namespace HotelCalifornia.PaymentGateway.Web.Features.CardHost
{
    public class CardHostController : Controller
    {
        private readonly IFileProvider _fileProvider;
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IPaymentSessionRepository _paymentSessionRepository;

        public CardHostController(IIntegrationRepository integrationRepository,
            IPaymentSessionRepository paymentSessionRepository, IFileProvider fileProvider, IMemoryCache memoryCache)
        {
            this._integrationRepository = integrationRepository;
            this._paymentSessionRepository = paymentSessionRepository;
            this._fileProvider = fileProvider;
            this._memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("~/card-host")]
        public IActionResult CardHost(Guid identifyingToken)
        {
            var paymentSession = this._paymentSessionRepository.GetByUniqueReference(identifyingToken);
            var integration = this._integrationRepository.GetById(paymentSession.IntegrationId);

            var fileInfo = this._fileProvider.GetFileInfo("wwwroot/cardhost.js");
            var cardHostScript = System.IO.File.ReadAllText(fileInfo.PhysicalPath);

            var newContent = new StringBuilder(cardHostScript)
                .Replace("%%__cardPaymentEnabled__%%", integration.CardEnabled.ToString())
                .Replace("%%__schemes__%%", $"{string.Join("', '", integration.Schemes)}")
                .Replace("%%__cardPostUrl__%%", this.Url.Action("CardPayment", "CardPayment"))
                .Replace("%%__currency__%%", integration.Currency)
                .Replace("%%__identifyingToken__%%", identifyingToken.ToString())
                .Replace("%%__methods__%%", $"{string.Join("', '", integration.Methods)}")
                .ToString();

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(newContent);
            writer.Flush();
            stream.Position = 0;

            var model = new CodeHostViewModel();
            model.IdentifyingToken = identifyingToken;

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(stream);
                model.SriHash = Base64UrlEncoder.Encode(hashedBytes);
                
            }

            this._memoryCache.Set($"cardhost-{identifyingToken}", newContent, DateTimeOffset.UtcNow.AddMinutes(2));

            return this.View(model);
        }

        [HttpGet]
        [Route("cardhost/{identifyingToken}/script.js")]
        public IActionResult Script(Guid identifyingToken)
        {
            var script = this._memoryCache.Get<string>($"cardhost-{identifyingToken}");
            return this.Content(script, "application/javascript");
        }
    }
}
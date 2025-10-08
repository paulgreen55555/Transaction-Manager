using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using TransactionManager.Api.Exceptions;
using TransactionManager.Api.Models;
using TransactionManager.Api.Services;

namespace TransactionManager.Tests
{
    public class CurrencyRateServiceTests
    {
        [Fact]
        public async Task GetExchangeRateAsync_ReturnsCorrectRate()
        {
            var expectedRate = 75.50m;
            var transactionDate = new DateOnly(2025, 10, 6);
            var currencyCode = "Canada-Dollar";

            var responseContent = JsonSerializer.Serialize(new CurrencyData
            {
                Data = new List<CurrencyRate>
                {
                    new CurrencyRate
                    {
                        ExchangeRate = expectedRate,
                        RecordDate = new DateTime(2025, 09, 01)
                    }
                }
            });

            var httpHandlerMock = new Mock<HttpMessageHandler>();

            httpHandlerMock.
                Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(responseContent),
               });

            var httpClient = new HttpClient(httpHandlerMock.Object);

            var service = new CurrencyRateService(httpClient);

            var result = await service.GetExchangeRateAsync(transactionDate, currencyCode);

            Assert.Equal(expectedRate, result);
        }

        [Fact]
        public async Task GetExchangeRateAsync_ThrowsHttpRequestException()
        {
            var transactionDate = new DateOnly(2025, 10, 6);
            var currencyCode = "Canada-Dollar";
            var expectedMessage = "Request to currency api failed with status code InternalServerError";

            var httpHandlerMock = new Mock<HttpMessageHandler>();

            httpHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.InternalServerError
               });

            var httpClient = new HttpClient(httpHandlerMock.Object);

            var service = new CurrencyRateService(httpClient);

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() =>
                service.GetExchangeRateAsync(transactionDate, currencyCode));

            Assert.Contains(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task GetExchangeRateAsync_ThrowsNotFoundException()
        {
            var transactionDate = new DateOnly(2025, 10, 6);
            var currencyCode = "Canada-Dollar";
            var expectedMessage = "No data for Canada-Dollar currency";

            var responseContent = JsonSerializer.Serialize(new CurrencyData());


            var httpHandlerMock = new Mock<HttpMessageHandler>();

            httpHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(responseContent),
               });

            var httpClient = new HttpClient(httpHandlerMock.Object);

            var service = new CurrencyRateService(httpClient);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                service.GetExchangeRateAsync(transactionDate, currencyCode));

            Assert.Contains(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task GetCurrenciesAsync_ReturnsCurrenciesList()
        {
            var expectedCurrenciesList = new List<string>() { "Canada-Dollar", "Mexico-Peso" };

            var responseContent = JsonSerializer.Serialize(new CountryCurrencyData
            {
                Data = new List<CountryCurrency>
                {
                    new CountryCurrency
                    {
                       CountryCurrencyDesc = "Canada-Dollar"
                    },
                    new CountryCurrency
                    {
                       CountryCurrencyDesc = "Mexico-Peso"
                    }
                }
            });

            var httpHandlerMock = new Mock<HttpMessageHandler>();

            httpHandlerMock.
                Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(responseContent),
               });

            var httpClient = new HttpClient(httpHandlerMock.Object);

            var service = new CurrencyRateService(httpClient);

            var result = await service.GetCurrenciesAsync();

            Assert.Equal(expectedCurrenciesList, result);
        }

        [Fact]
        public async Task GetCurrenciesAsync_ThrowsHttpRequestException()
        {
            var expectedMessage = "Request to currency api failed with status code InternalServerError";

            var httpHandlerMock = new Mock<HttpMessageHandler>();

            httpHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.InternalServerError
               });

            var httpClient = new HttpClient(httpHandlerMock.Object);

            var service = new CurrencyRateService(httpClient);

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() =>
                service.GetCurrenciesAsync());

            Assert.Contains(expectedMessage, exception.Message);
        }

    }
}

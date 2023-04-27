using IrishNFT.Models;
using Newtonsoft.Json;
using System.Text;

namespace IrishNFT.Services
{
    public class CartItemService : ICartItemService
    {
        private const string Key = "OrderServiceBaseUrl";
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CartItemService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configuration[Key]);
        }

        public async Task<List<CartItem>> GetCartItemsByUsername(string userName)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"/api/cartitem");

                var response = await _httpClient.SendAsync(request);
                var responseResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<dynamic>(responseResult);

                    throw new AppException(error?.responseDescription, (int)response.StatusCode);
                }
                var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(responseResult);
                return cartItems?.Where(f => f.Username == userName).ToList() ?? new List<CartItem>();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, "An error occurred!.");
            }
        }

        public async Task AddCartItem(int productId, string username)
        {
            try
            {
                var requestBody = JsonConvert.SerializeObject(new { quantity = 1, dateCreated = DateTime.Now, productId, username  });
               
                var request = new HttpRequestMessage(HttpMethod.Post, $"/api/cartitem")
                {
                    Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);
                var responseResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<dynamic>(responseResult);

                    throw new AppException(error?.responseDescription, (int)response.StatusCode);
                }
                
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, "An error occurred!.");
            }
        }

        public async Task DeleteCartItemId(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/cartitem/{id}");

                var response = await _httpClient.SendAsync(request);
                var responseResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<dynamic>(responseResult);

                    throw new AppException(error?.responseDescription, (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, "An error occurred!.");
            }
        }

        public async Task<CartItem> GetyCartItemById(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"/api/cartitem/{id}");

                var response = await _httpClient.SendAsync(request);
                var responseResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<dynamic>(responseResult);

                    throw new AppException(error?.responseDescription, (int)response.StatusCode);
                }
                var cartItem = JsonConvert.DeserializeObject<CartItem>(responseResult);
                return cartItem ?? new CartItem();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, "An error occurred!.");
            }
        }

    }
}

using IrishNFT.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace IrishNFT.Services
{
    public class ProductService : IProductService
    {
        private const string Key = "ProductServiceBaseUrl";
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configuration[Key]);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"/api/products");

                var response = await _httpClient.SendAsync(request);
                var responseResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<dynamic>(responseResult);

                    throw new AppException(error?.responseDescription, (int)response.StatusCode);
                }
                var products = JsonConvert.DeserializeObject<List<Product>>(responseResult);
                return products ?? new List<Product>();
            }
            catch(Exception ex)
            {
                throw new AppException(ex.Message, "An error occurred!.");
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"/api/products/{id}");

                var response = await _httpClient.SendAsync(request);
                var responseResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<dynamic>(responseResult);

                    throw new AppException(error?.responseDescription, (int)response.StatusCode);
                }
                var product = JsonConvert.DeserializeObject<Product>(responseResult);
                return product ?? new Product();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, "An error occurred!.");
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"/api/categories");

                var response = await _httpClient.SendAsync(request);
                var responseResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<dynamic>(responseResult);

                    throw new AppException(error?.responseDescription, (int)response.StatusCode);
                }
                var categories = JsonConvert.DeserializeObject<List<Category>>(responseResult);
                return categories ?? new List<Category>();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, "An error occurred!.");
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"/api/categories/id");

                var response = await _httpClient.SendAsync(request);
                var responseResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<dynamic>(responseResult);

                    throw new AppException(error?.responseDescription, (int)response.StatusCode);
                }
                var category = JsonConvert.DeserializeObject<Category>(responseResult);
                return category ?? new Category();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, "An error occurred!.");
            }
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            var products = await GetAllProducts();
            products = products.Where(p => p.CategoryID == categoryId).Select(s => s).ToList();
            return products;

        }

        public async Task<bool> IsProductAvailable(int productId)
        {
            var product = await GetProductById(productId);

            if (product.Status == (int)Status.Available)
                return true;
            return false;
        }

        public async Task SetProductStatus(int productId, Status status)
        {
            var product = await GetProductById(productId);

            product.Status = (int)status;

            try
            {
                var requestBody = JsonConvert.SerializeObject(product);

                var request = new HttpRequestMessage(HttpMethod.Put, $"/api/products/{productId}")
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
    }
}

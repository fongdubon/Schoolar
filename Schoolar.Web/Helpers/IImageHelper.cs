namespace Schoolar.Web.Helpers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string nameFile, string folder);
    }
}

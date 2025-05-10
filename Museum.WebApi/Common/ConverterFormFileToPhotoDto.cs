using Museum.Application.SQRS.MuseumEvents.Common;

namespace Museum.WebApi.Common
{
    public static class ConverterFormFileToPhotoDto
    {
        public static async Task<List<PhotoUploadDto>> ConvertRangeAsync(ICollection<IFormFile> photos)
        {
            var result = new List<PhotoUploadDto>();
            foreach (var photo in photos)
            {
                result.Add(await ConvertAsync(photo));
            }
            return result;
        }


        public static async Task<PhotoUploadDto> ConvertAsync(IFormFile photo)
        {
            var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return new PhotoUploadDto
            {
                Content = memoryStream,
                Name = photo.FileName,
            };
        }
    }
}

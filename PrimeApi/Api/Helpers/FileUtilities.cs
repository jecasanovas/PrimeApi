namespace PrimeApi.Api.Helpers
{
    public static class FileUtilities
    {

       public static async Task<string>  CreateFile(IFormFile formFile)
        {
            var filePath = String.Empty;
            if (formFile.Length > 0)
            {
                filePath = $"../files/{formFile.FileName}";
                using (var inputStream = new FileStream(filePath, FileMode.Create))
                {
                    // read file to stream
                    await formFile.CopyToAsync(inputStream);
                    // stream to byte array
                    byte[] array = new byte[inputStream.Length];
                    inputStream.Seek(0, SeekOrigin.Begin);
                    inputStream.Read(array, 0, array.Length);
                    // get file name
                    string fName = formFile.FileName;
                }
            }
            return filePath;
        }
    }
}

using BLL.Dtos;
using CsvHelper;
using CsvHelper.Configuration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Globalization;
using System.Text;

namespace PrimeApi
{

    public static class Reports
    {
        public static void CreateReport<T>(IEnumerable<T> data, string fileName, bool csvOnly)
        {
            if (csvOnly)
            {
                CreateCsv<T>(data, fileName);

            } else
            {
                fileName = fileName.Replace("xls", "csv");
                CreateCsv<T>(data, fileName.Replace("xls","csv"));
                CSVtoExcel(fileName);
            }
        }

        private static void CreateCsv<T>(IEnumerable<T> data,string filename)
        {
            var conf = new CsvConfiguration(CultureInfo.CreateSpecificCulture("en"));
            if (!Directory.Exists(@"../files"))
                Directory.CreateDirectory(@"../files");

            using (var writer = new StreamWriter($"../files/{filename}"))
            using (var csv = new CsvWriter(writer, conf))
             {

                csv.WriteHeader<T>();
                csv.NextRecord();
                foreach (var record in data)
                {
                    csv.WriteRecord(record);
                    csv.NextRecord();
                }
            }
        }
        public static void CSVtoExcel(string fileName)
        {
            fileName = $"../files/{fileName}";
            string newFileName = fileName.Replace(".csv", ".xls");
            string text = File.ReadAllText(fileName, Encoding.UTF8);
            var lines = text.Split("\r\n");
            int columnCounter = 0;

            foreach (string s in lines)
            {
                string[] ss = s.Trim().Split(Convert.ToChar("|"));
                columnCounter = ss.Length;
                break;
            }
            HSSFWorkbook workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Data");
            var rowIndex = 0;
            var rowExcel = sheet.CreateRow(rowIndex);
            
            foreach (string s in lines)
            {
                rowExcel = sheet.CreateRow(rowIndex);

                string[] ss = s.Trim().Split(Convert.ToChar("|"));

                for (int i = 0; i < columnCounter; i++)
                {
                    string data = !String.IsNullOrEmpty("s") && i < ss.Length ? ss[i] : "";
                    rowExcel.CreateCell(i).SetCellType(CellType.String);
                    rowExcel.CreateCell(i).SetCellValue(data.Replace("\"","").Trim());
                }
                rowIndex++;
            }
            for (var i = 0; i < sheet.GetRow(0).LastCellNum; i++)

                using (FileStream file = new FileStream(newFileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(file);
                    file.Close();
                }
        }

        public static IEnumerable<CourseDetailDto> ReadCsv(string absolutePath, int id)
        {
            var records = new List<CourseDetailDto>();
            var emptyresult = new List<CourseDetailDto>();
            using (var reader = new StreamReader(absolutePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = new CourseDetailDto
                        {
                            LessonId = csv.GetField<int>(0),
                            Description = $"{csv.GetField<string>(0)}. {csv.GetField(1)}",
                            CourseId = id,
                            Id = 0
                        };
                        records.Add(record);
                    }
                }
            }
            return records;
        }
    }
}
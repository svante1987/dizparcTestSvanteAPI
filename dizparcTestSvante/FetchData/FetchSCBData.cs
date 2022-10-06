using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;

namespace dizparcTestSvante.FetchData
{
    public class FetchSCBData
    {
        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-ö0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
        public static void fetchData()
        {
        EncodingProvider ppp = CodePagesEncodingProvider.Instance;
        Encoding.RegisterProvider(ppp);
            using (var client = new HttpClient())
            {
                var path = Environment.CurrentDirectory;
                var jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(Environment.CurrentDirectory + @"\Json\data.json"));
                var url = "https://api.scb.se/OV0104/v1/doris/sv/ssd/START/BE/BE0101/BE0101H/FoddaK";
                var endpoint = new Uri(url);

                var newPostJson = JsonConvert.SerializeObject(jsonFile);

                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");

                var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;

                StreamWriter sw = new StreamWriter("test_file.txt");
                sw.WriteLine(result);
                sw.Close();
                saveData();
            }
        }

        public static void saveData()
        {
            using (SqlConnection conn = new SqlConnection(@"Server=LAPTOP-7I73AQN6;Database=SvanteDb;Trusted_Connection=True;TrustServerCertificate=True"))
            {
                var lineNr = 0;
                conn.Open();
                using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\test_file.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (lineNr != 0 && lineNr != 581)
                        {
                            RemoveSpecialCharacters(line);
                            var values = line.Split(',');
                            var sql = "INSERT INTO SvanteDb.dbo.Born VALUES ('" + values[0] + "','" + values[1] + "'," + values[2] + "," + values[3] + "," + values[4] + "," + values[5] + "," + values[6] + ")";
                            var cmd = new SqlCommand();
                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = conn;
                            cmd.ExecuteNonQuery();
                        }
                        lineNr++;
                    }
                }
                conn.Close();
            }
        }
    }
}

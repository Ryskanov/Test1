using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Test1
{
    class Record
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }

        public override string ToString()
        {
            return userId.ToString() + " " + id.ToString() + " " + title + " " + body;
        }


    }

    class MyData
    {
        private string siteName;        // Ссылка на сайт
        private WebClient webClient;    // webClient
        public List<Record> book;       // Список публикаций
        public List<int> userId;        // Список пользователей


        // Конструктор
        public MyData(string pSiteName)
        {
            siteName = pSiteName;
            webClient = new WebClient();
            userId = new List<int>();
        }

        // Конструктор
        public MyData()
        {
            siteName = "";
            webClient = new WebClient();
        }

        // Установить ссылку на сайт
        public void SetSiteName(string pSiteName)
        {
            siteName = pSiteName;
        }

        // Прочитать сайт Get запросом
        public int Read()
        {
            int error = 0;
            try
            {
                string result = webClient.DownloadString(siteName);
                book = JsonSerializer.Deserialize<List<Record>>(result);
                foreach (Record element in book)
                {
                    if (userId.IndexOf(element.userId) == -1)
                    {
                        userId.Add(element.userId);
                    }

                }
            }
            catch
            {
                error = 1;
            }
            return error;
        }

    }
}

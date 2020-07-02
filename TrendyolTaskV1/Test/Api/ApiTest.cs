using NUnit.Framework;
using TechTalk.SpecFlow;
using RestSharp;
using System.Net;
using TrendyolTaskV1.Entity;
using Newtonsoft.Json;

namespace TrendyolTaskV1.Test
{
    [TestFixture]
    [Binding, Scope(Feature = "ApiTest")]

    public class ApiTest
    {
        private Book book;
        [StepDefinition(@"Apinin boş olup olmadığı kontrol edilir")]
        public void CheckWhetherApiIsEmptyOrNot()
        {

            var client = new RestClient("apiAddress");            
            var request = new RestRequest("/api/books/", Method.GET);
            var response = client.Execute(request);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.StatusCode, "HTTP 500 Internal Server Error hatası alınmıştır! ");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Response hata almıştır! " + response.ErrorMessage);
            Books responseBook = JsonConvert.DeserializeObject<Books>(response.Content);
            Assert.AreEqual(0, responseBook.BookList.Count, "Api response dolu gelmiştir! ");            
        }

        [StepDefinition(@"Apide author girilmeden kitap eklenmeye çalışılır")]
        public void CheckWhetherAuthorIsRequiredOrNot()
        {
            var client = new RestClient("apiAddress");
            var request = new RestRequest("/api/books/", Method.PUT);
            request.AddParameter("title", "kitapİsmi");
            var response = client.Execute(request);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.StatusCode, "HTTP 500 Internal Server Error hatası alınmıştır! ");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Response hata almamıştır! ");
            ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            Assert.AreEqual("Field 'author' is required", error.Error, "Hata mesajı yanlış! ");
        }

        [StepDefinition(@"Apide title girilmeden kitap eklenmeye çalışılır")]
        public void CheckWhetherTitleIsRequiredOrNot()
        {
            var client = new RestClient("apiAddress");
            var request = new RestRequest("/api/books/", Method.PUT);
            request.AddParameter("author", "yazarİsmmi");
            var response = client.Execute(request);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.StatusCode, "HTTP 500 Internal Server Error hatası alınmıştır! ");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Response hata almamıştır! ");
            ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            Assert.AreEqual("Field 'title' is required", error.Error, "Hata mesajı yanlış! ");
        }

        [StepDefinition(@"Apide author boş girilerek kitap eklenmeye çalışılır")]
        public void CheckWhetherAuthorCanNotBeEmpty()
        {
            var client = new RestClient("apiAddress");
            var request = new RestRequest("/api/books/", Method.PUT);
            request.AddParameter("title", "kitapİsmi");
            request.AddParameter("author", "");
            var response = client.Execute(request);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.StatusCode, "HTTP 500 Internal Server Error hatası alınmıştır! ");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Response hata almamıştır! ");
            ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            Assert.AreEqual("Field 'author' cannot be empty", error.Error, "Hata mesajı yanlış! ");
        }

        [StepDefinition(@"Apide title boş girilerek kitap eklenmeye çalışılır")]
        public void CheckWhetherTitleCanNotBeEmpty()
        {
            var client = new RestClient("apiAddress");
            var request = new RestRequest("/api/books/", Method.PUT);
            request.AddParameter("title", "");
            request.AddParameter("author", "Yazarİsmi");
            var response = client.Execute(request);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.StatusCode, "HTTP 500 Internal Server Error hatası alınmıştır! ");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Response hata almamıştır! ");
            ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            Assert.AreEqual("Field 'title' cannot be empty", error.Error, "Hata mesajı yanlış! ");
        }

        [StepDefinition(@"Apide Id girilerek kitap eklenmeye çalışılır")]
        public void CheckWhetherIdCanNotBeSent()
        {
            var client = new RestClient("apiAddress");
            var request = new RestRequest("/api/books/", Method.PUT);
            request.AddParameter("title", "kitapİsmi");
            request.AddParameter("author", "Yazarİsmi");
            request.AddParameter("id", "1");
            var response = client.Execute(request);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.StatusCode, "HTTP 500 Internal Server Error hatası alınmıştır! ");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Response hata almamıştır! ");           
        }

        [StepDefinition(@"Apide '(.*)' yazarının '(.*)' kitabı eklenir")]
        public void AddBookInApi(string author, string title)
        {
            var client = new RestClient("apiAddress");
            var request = new RestRequest("/api/books/", Method.PUT);
            request.AddParameter("title", title);
            request.AddParameter("author", author);            
            var response = client.Execute(request);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.StatusCode, "HTTP 500 Internal Server Error hatası alınmıştır! ");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Response hata almıştır! ");
            book = JsonConvert.DeserializeObject<Book>(response.Content);
        }

        [StepDefinition(@"Apiye oluşturulan kitap idsi ile GetRequest atıldığında oluşturulan kitabın döndüğü görülür")]
        public void CheckWhetherBookWithIdExists()
        {
            var client = new RestClient("apiAddress");
            var request = new RestRequest("/api/books/" + book.Id, Method.GET);            
            var response = client.Execute(request);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.StatusCode, "HTTP 500 Internal Server Error hatası alınmıştır! ");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Response hata almıştır! ");
            Book createdBook = JsonConvert.DeserializeObject<Book>(response.Content);
            Assert.AreEqual(book.Author, createdBook.Author, "Yazarlar farklıdır! ");
            Assert.AreEqual(book.Title, createdBook.Title, "Kitap adı farklıdır! ");
            Assert.AreEqual(book.Id, createdBook.Id, "Kitap idleri farklıdır! ");
        }

        [StepDefinition(@"Apide '(.*)' yazarının '(.*)' kitabının tekrar eklenemediği görülür")]
        public void AddBookInApiWithSameAuthorAndTitle(string author, string title)
        {
            var client = new RestClient("apiAddress");
            var request = new RestRequest("/api/books/", Method.PUT);
            request.AddParameter("title", title);
            request.AddParameter("author", author);
            var response = client.Execute(request);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.StatusCode, "HTTP 500 Internal Server Error hatası alınmıştır! ");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Response hata almamıştır! ");
            ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            Assert.AreEqual("Another book with similar title and author already exists.", error.Error, "Hata mesajı yanlış gelmiştir! ");
        }
    }
}

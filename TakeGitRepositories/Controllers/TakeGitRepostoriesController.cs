using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Take.Blip.Client;
using Lime.Protocol;
using Lime.Messaging.Contents;
using Newtonsoft.Json.Linq;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TakeGitRepositories.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TakeGitRepostoriesController : ControllerBase
    {

        [HttpGet]
        public async Task<string> Get()
        {
            var result = await PopulateObjectForCarousel();
            return result;
        }

        private static async Task<string> ProcessRepositories()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/takenet/repos");
            var response =  stringTask;
            response.Wait();
         
            return response.Result;
        }

        private async Task<string> PopulateObjectForCarousel()
        {
            List<Rootobject> firstFiveRepos = await FirstFiveRepository();
            Carousel carousel = new Carousel();

            carousel.itemType = "application/vnd.lime.document-select+json";
            carousel.items = new List<Header>();
            

            firstFiveRepos.ForEach(repos =>
            {
                HeaderBody headerBody = new HeaderBody();
                Header header = new Header();
                header.header = new HeaderBody();

                headerBody.value = new Value();

                headerBody.type = "application/vnd.lime.media-link+json";

                headerBody.value.title = repos.full_name;
                headerBody.value.text = repos.description;
                headerBody.value.type = "image/jpeg";
                headerBody.value.uri = repos.owner.avatar_url;

                header.header = headerBody;
                carousel.items.Add(header);
            });

           

            string result = JsonConvert.SerializeObject(carousel);

            return result;
        }

        private async Task<List<Rootobject>> FirstFiveRepository()
        {
            List<Rootobject> allRepos = new List<Rootobject>();
            var allReposJson =  await ProcessRepositories();
            JsonConvert.PopulateObject(allReposJson, allRepos);
            var firstFiveRepos = allRepos.Where(x => x.language == "C#").OrderBy(x => x.created_at).Take(5).ToList();

          

            return firstFiveRepos;
        }

        [Obsolete]
        private async Task<string> PopulateObjectForCarouselUsingString()
        {
            List<Rootobject> firstFiveRepos = await FirstFiveRepository();

            string carousel = "{"
                 + "'itemType': 'application/vnd.lime.document-select+json',"
                 + "'items': [";


            firstFiveRepos.ForEach(repos =>
            {

                var jsonResult = @"
                            {
                            header:
                            {
                            type:'application/vnd.lime.media-link+json',
                            value:
                            {
                            title:'" + repos.full_name + "', " +
                            "text:'" + repos.description + "'," +
                            "type:'image/jpeg'," +
                            "uri:'" + repos.owner.avatar_url + "'" +
                            "}" +
                            "}," +
                            "},";





                carousel += jsonResult;
            });

            carousel += "]}";

            return carousel;
        }




    }
}

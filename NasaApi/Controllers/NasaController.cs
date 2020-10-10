using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NasaApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NasaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NasaController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public NasaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // Read dates from file 

        [HttpGet]
        [Route("poolImages")]
        public ActionResult poolImages()
        {
            string path = @"dates.txt";

            //Read file from path 
            string[] fileInfo = System.IO.File.ReadAllLines(path);

            foreach (string info in fileInfo)
            {
                DateTime outDate = new DateTime();
                DateTime.TryParse(info, out outDate);

                if (outDate != DateTime.MinValue)
                    GetNasaPhotos(outDate.ToString("yyyy-MM-dd"));
            }

            return Ok();
        }      

        // Get Photos with specific date
        public async void GetNasaPhotos(string earth_date)
        {
            if (earth_date == null)
                return;

            string key = _configuration.GetSection("API_Settings").GetSection("MarsRover_Api_Key").Value;
            string url = _configuration.GetSection("API_Settings").GetSection("MarsRover_Api_URL").Value;
            string finalURL = url + "?api_key=" + key + "&earth_date=" + earth_date;
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(finalURL);
            string apiResponse = await response.Content.ReadAsStringAsync();
            MarsRoverResponse result = JsonConvert.DeserializeObject<MarsRoverResponse>(apiResponse);

            if (result == null)
                return;

            SaveImages(earth_date, result.photos);
        }

        //SaveImages to local
        public void SaveImages(string earth_date, Collection<MarsRoverPhotos> photoUrls)
        {
            foreach (MarsRoverPhotos photos in photoUrls)
                using (WebClient cl = new WebClient())
                {
                    string st = earth_date+ '_' + $"{Guid.NewGuid().ToString()}.png";
                    cl.DownloadFileAsync(new Uri(photos.img_src), $"NasaImages\\{st}");
                }
        }

       

    }
}

using api_test.Context;
using api_test.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace api_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonajesController : ControllerBase
    {
        private readonly pruebaContext dbContext;
        public PersonajesController(pruebaContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("ingresar")]
        public  ActionResult<int> ingresar(CharacterDB character)
        {

            var rs = dbContext.CharacterDB.Where(c => c.Id.Equals(character.Id));
            if (rs != null)
            {
                var eDatos = dbContext.Set<CharacterDB>();
                eDatos.Add(character);
                dbContext.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }

        }

        [HttpGet]
        [Route("consultar")]
        public ResponseIntegration<List<Character>> consultar()
        {
            Character character;
            List<Character> lstCharacterV = new List<Character>();
            List<Character> lstCharacterM = new List<Character>();


            var resultado = new ResponseIntegration<List<Character>>();


            var url = $"https://rickandmortyapi.com/api/character?species=Human";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                        {
                       
                            return resultado;

                        }
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();


                            JObject data = JObject.Parse(responseBody);
                            JArray lstResult = (JArray)data["results"];
                            foreach (var item in lstResult)
                            {


                                if (item["status"].ToString().Equals("Alive"))
                                {
                                    character = new Character();
                                    character.id = item["id"].ToString();
                                    character.name = item["name"].ToString();
                                    character.image = item["image"].ToString();
                                    character.status = item["status"].ToString();
                                    character.species = item["species"].ToString();

                                    lstCharacterV.Add(character);

                                }
                                else if (item["status"].ToString().Equals("Dead"))
                                {
                                    character = new Character();
                                    character.id = item["id"].ToString();
                                    character.name = item["name"].ToString();
                                    character.image = item["image"].ToString();
                                    character.status = item["status"].ToString();
                                    character.species = item["species"].ToString();

                                    lstCharacterM.Add(character);
                                }

                               
                               
                            }


                        }
                    }
                }
            }
            catch (WebException ex){}

            resultado.cantidadV = lstCharacterV.Count;
            resultado.cantidadM = lstCharacterM.Count;
            resultado.listaVivos = lstCharacterV;
            resultado.listaMuertos = lstCharacterM;

            return resultado;
        }
    }
}

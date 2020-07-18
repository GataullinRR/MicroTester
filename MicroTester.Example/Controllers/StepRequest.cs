using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MicroTester.Example.Controllers
{
    public class StepRequest
    {
        [Required]
        public string ClientId { get; set; }

        public StepRequest()
        {

        }

        [JsonConstructor]
        public StepRequest(string clientId)
        {
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
        }
    }
}

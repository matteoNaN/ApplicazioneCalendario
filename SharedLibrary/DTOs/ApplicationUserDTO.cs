using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        
        public string GetName()
        {
            return Name.Substring(0,Name.IndexOf("@"));
        }
    }



}

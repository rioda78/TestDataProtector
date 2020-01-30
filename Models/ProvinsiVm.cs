using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDataProtector.Models
{
    public class ProvinsiVm
    {
        public Provinsi Provinsi { get; set; }
        public string EncryptedId { get; set; }
    }
}

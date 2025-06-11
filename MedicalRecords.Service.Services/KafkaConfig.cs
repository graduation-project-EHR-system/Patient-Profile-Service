using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Services
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string AutoOffsetReset { get; set; } = string.Empty;
        public List<string> Topics { get; set; } = new();
    }
}

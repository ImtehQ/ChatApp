using ChatApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Models
{
    public class Message
    {
        public int Id {  get; set; }
        public string Content {  get; set; }
        public byte[] BlobContent {  get; set; }
        public GroupTypeEnum type {  get; set; }
        public int TypeId {  get; set; }
    }
}

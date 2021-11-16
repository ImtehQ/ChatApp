using ChatApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Models
{
    public class Message
    {
        public int MessageId {  get; set; }
        public string Content {  get; set; }
        public byte[] BlobContent {  get; set; } //lotus
        [ForeignKey("UserId")]
        public User SenderId { get; set; }
        public bool Received { get; set; }
        public bool Read { get; set; }
    }
}

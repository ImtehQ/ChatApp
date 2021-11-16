using ChatApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Models
{
    public class Group 
    {
        public int GroupId {  get; set; }
        public GroupTypeEnum type { get; set; }
        public string Name {  get; set; }
        public int MaxUsers {  get; set; }
        public int VisibilityType {  get; set; }
        public int Password {  get; set; }

        public GroupUsers Users {  get; set; }
        
        public Message[] messageIds { get; set; }
    }
}

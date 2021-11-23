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
        public GroupVisibilityEnum VisibilityType {  get; set; }
        public string Password {  get; set; }
    }
}

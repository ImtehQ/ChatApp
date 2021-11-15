using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Models
{
    public class Group 
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public int MaxUsers {  get; set; }
        public int VisibilityType {  get; set; }
        public int Password {  get; set; }
        public User[] Moderators {  get; set; }
        public int Admin { get; set; }

    }
}

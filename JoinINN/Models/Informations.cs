using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoinINN.Models
{
    public class Informations
    {
        public Informations(int groups, int visitors, int admins, int croatia, int split, int zagreb)
        {
            this.Groups = groups;
            this.Visitors = visitors;
            this.Admins = admins;
            this.Croatia = croatia;
            this.Split = split;
            this.Zagreb = zagreb;
        }

        public int Groups { get; set; }
        public int Visitors { get; set; }
        public int Admins { get; set; }
        public int Croatia { get; set; }
        public int Split { get; set; }
        public int Zagreb { get; set; }
    }
}
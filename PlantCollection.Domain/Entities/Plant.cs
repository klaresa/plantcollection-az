using System;
using System.Collections.Generic;
using System.Text;

namespace PlantCollection.Domain.Model.Entities
{
    public class Plant
    {
        public Guid Id { get; set; }
        public string BinomialName { get; set; }
        public string Species { get; set; }
        public string Genus { get; set; }
        public string ImageUri { get; set; }
        public int PageViews { get; set; }
    }
}

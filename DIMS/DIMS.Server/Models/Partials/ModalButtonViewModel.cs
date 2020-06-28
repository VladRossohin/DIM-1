using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIMS.Server.Models.Partials
{
    public class ModalButtonViewModel
    {
        public string Value { get; set; }
        public string ButtonClass { get; set; }
        public bool IsItDetails { get; set; }
        public int? Id { get; set; }
    }
}
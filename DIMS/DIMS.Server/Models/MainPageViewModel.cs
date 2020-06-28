using System.Collections.Generic;

namespace DIMS.Server.Models
{
    public class MainPageViewModel
    {
        public List<ActionLinkViewModel> ActionLinks { get; set; } = new List<ActionLinkViewModel>();
        public string CoverHead { get; set; }
    }
}
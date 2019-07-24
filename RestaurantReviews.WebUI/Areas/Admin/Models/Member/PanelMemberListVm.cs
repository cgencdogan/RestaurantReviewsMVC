using System.Collections.Generic;

namespace RestaurantReviews.WebUI.Areas.Admin.Models {
    public class PanelMemberListVm {
        public List<PanelMemberVm> MemberList { get; set; }
        public string SearchWord { get; set; }
        public int PageNumber { get; set; }
        public int ShownCount { get; set; }
        public double MaxPage { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
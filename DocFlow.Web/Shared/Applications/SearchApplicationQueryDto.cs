using System.ComponentModel.DataAnnotations;

namespace DocFlow.Web.Shared.Applications
{
  public class SearchApplicationQueryDto
    {
        [RegularExpression(@"\w{8}(\-\w{4}){4}\w{8}", ErrorMessage = "Wrong application id.")]
        public string ApplicationId { get; set; }

        [RegularExpression(@"\w{8}(\-\w{4}){4}\w{8}", ErrorMessage = "Please select correct status")]
        public string StatusId { get; set; }
    }
}
